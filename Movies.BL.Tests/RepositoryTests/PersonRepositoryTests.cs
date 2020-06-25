using System;
using System.Collections.Generic;
using System.Linq;
using Movies.BL.Interfaces;
using Movies.BL.Mappers;
using Movies.BL.Models;
using Movies.BL.Repositories;
using Movies.DAL.Entities;
using Xunit;

namespace Movies.BL.Tests.RepositoryTests
{
    public class PersonRepositoryTests : TestsBase
    {
        private readonly IRepository<PersonEntity, PersonListModel, PersonDetailModel> repository;

        public PersonRepositoryTests() : base()
        {
            repository = new PersonRepository(dbContextFactory);
        }

        [Fact]
        public void GetALL_List_NotEmpty()
        {
            var models = repository.GetAll();

            Assert.NotEmpty(models);
        }

        [Fact]
        public void GetById_Detail_Matches()
        {
            var viggo = DAL.Seeds.Persons.Mortensen;
            var lotr = DAL.Seeds.Movies.LordOfTheRings;
            var lotr2 = DAL.Seeds.Movies.LordOfTheRingsDuplicate;

            var viggoPlays = DAL.Seeds.PersonPlaysMovie.MortensenPlaysLordOfTheRings;
            var viggoPlays2 = DAL.Seeds.PersonPlaysMovie.MortensenPlaysLordOfTheRings2;

            viggoPlays.Actor = viggo;
            viggoPlays.Movie = lotr;
            viggoPlays2.Actor = viggo;
            viggoPlays2.Movie = lotr2;

            var model = new PersonDetailModel()
            {
                Id = viggo.Id,
                FirstName = viggo.FirstName,
                LastName = viggo.LastName,
                Age = viggo.Age,
                Photo = viggo.Photo,
                PlayedMovies = new List<ActorMovieModel>()
               {
                    ActorMovieMapper.MapToModel(viggoPlays),
                    ActorMovieMapper.MapToModel(viggoPlays2),
               }
            };

            var returnedModel = repository.GetById(viggo.Id);

            Assert.Equal(model, returnedModel, PersonDetailModel.PersonDetailComparer);
        }

        [Fact]
        public void DeleteById_Model_NotPersisted()
        {
            var mortenson = DAL.Seeds.Persons.Mortensen;

            repository.Delete(mortenson.Id);

            var returnedModel = repository.GetById(mortenson.Id);

            Assert.Null(returnedModel);

        }

        [Fact]
        public void DeleteByModel_Model_NotPersisted()
        {
            var viggo = DAL.Seeds.Persons.MortensenDuplicate;

            var model = new PersonDetailModel()
            {
                Id = viggo.Id,
                FirstName = viggo.FirstName,
                LastName = viggo.LastName,
                Age = viggo.Age,
                Photo = viggo.Photo
            };

            repository.Delete(model);

            var returnedModel = repository.GetById(viggo.Id);

            Assert.Null(returnedModel);

        }

        [Fact]
        public void Insert_Model_DidntThrow()
        {

            MovieListModel PlayedMovieList = new MovieListModel()
            {
                CzechName = "Žádný",
                OriginalName = "None",
                Description = "Nic",
                Country = "Neexistující"
            };

            MovieListModel PlayedMovieList2 = new MovieListModel()
            {
                CzechName = "Temný rytíø",
                OriginalName = "The Dark Knight",
                Description =
                            "Akèní dobrodružná fantasy v režii Christophera Nolana navazuje na film Batman zaèíná a Christian Bale si v ní zopakoval roli Batmana/Bruce Waynea v nekonèícím boji se zloèinem. Wayne v kostýmu netopýøího muže jde spolu s poruèíkem Jamesem Gordonem (Gary Oldman) z Gothamské policie po králi zloèincù Jokerovi známého podle symbolu - hrací karty s žolíkem - kterou zanechává na místech èinu. Vtipálek s proøízlou tváøí (Heath Ledger) má tentokrát v plánu Batmanovi dokázat, že slova Gotham a mír nikdy nebudou stát v jedné vìtì vedle sebe. Na uvolnìné místo po bývalém králi místní mafie usedá Sal Maroni (Eric Roberts) a pøebírá vládu nad vìtšinou gangù v Gothamu. Na scénì se rovnìž objevuje nový státní návladní Harvey Dent (Aaron Eckhart), který by rozdanými kartami (i s žolíkem v balíèku) mohl poøádnì zamíchat...",
                Country = "USA / Velká Británie"
            };



            var model = new PersonDetailModel()
            {
                FirstName = "Christopher",
                LastName = "Nolan",
                Age = 49,
                Photo = "photo.png",
                PlayedMovies = new[] {
                    new ActorMovieModel(){
                    Movie = PlayedMovieList
                    },
                    new ActorMovieModel(){
                    Movie = PlayedMovieList2
                    }
                }
            };

            var returnedModel = repository.InsertOrUpdate(model);

            Assert.NotNull(returnedModel);

        }

        [Fact]
        public void Insert_Model_Persisted()
        {
            var personEntity = DAL.Seeds.Persons.NoNameActor;
            var model = PersonMapper.MapToDetail(personEntity);

            var returnedModel = repository.InsertOrUpdate(model);
            var insertedModel = repository.GetById(returnedModel.Id);

            Assert.NotEqual(Guid.Empty, insertedModel.Id);

        }

        [Fact]
        public void Update_Model_DidntThrow()
        {
            var viggo = DAL.Seeds.Persons.Mortensen;
            var lotr = DAL.Seeds.Movies.LordOfTheRings;
            var lotr2 = DAL.Seeds.Movies.LordOfTheRingsDuplicate;

            var model = new PersonDetailModel()
            {
                Id = viggo.Id,
                FirstName = viggo.FirstName,
                LastName = viggo.LastName,
                Age = 12,
                Photo = viggo.Photo,
                PlayedMovies = new[]
                {
                    new ActorMovieModel(){
                        Movie = new MovieListModel(){
                        Id = lotr.Id,
                        CzechName = lotr.CzechName,
                        OriginalName = lotr.OriginalName,
                        Description = lotr.Description,
                        Country = lotr.Country
                        }
                    },
                    new ActorMovieModel()
                    {
                         Movie = new MovieListModel()
                         {
                            Id = lotr2.Id,
                            CzechName = lotr2.CzechName,
                            OriginalName = lotr2.OriginalName,
                            Description = lotr2.Description,
                            Country = lotr2.Country
                         }
                    }

                }
            };

            var returnedModel = repository.InsertOrUpdate(model);

            Assert.NotNull(returnedModel);
        }

        [Fact]
        public void Update_Model_Edited()
        {
            var newAge = 12;

            var viggo = DAL.Seeds.Persons.Mortensen;
            var lotr = DAL.Seeds.Movies.LordOfTheRings;
            var lotr2 = DAL.Seeds.Movies.LordOfTheRingsDuplicate;

            var viggoPlays = DAL.Seeds.PersonPlaysMovie.MortensenPlaysLordOfTheRings;
            var viggoPlays2 = DAL.Seeds.PersonPlaysMovie.MortensenPlaysLordOfTheRings2;

            var newViggo = viggo;
            newViggo.Age = newAge;

            viggoPlays.Actor = newViggo;
            viggoPlays.Movie = lotr;
            viggoPlays2.Actor = newViggo;
            viggoPlays2.Movie = lotr2;

            var model = new PersonDetailModel()
            {
                Id = viggo.Id,
                FirstName = viggo.FirstName,
                LastName = viggo.LastName,
                Age = newAge,
                Photo = viggo.Photo,
                PlayedMovies = new List<ActorMovieModel>()
                {
                    ActorMovieMapper.MapToModel(viggoPlays),
                    ActorMovieMapper.MapToModel(viggoPlays2)
                }
            };

            var returnedModel = repository.InsertOrUpdate(model);
            var editedModel = repository.GetById(returnedModel.Id);

            Assert.Equal(model, editedModel, PersonDetailModel.PersonDetailComparer);
        }
    }
}