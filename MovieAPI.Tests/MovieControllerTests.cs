using Xunit;
using Moq;
using MoviesAPI.Services.Abstractions;
using System.Reflection;
using System.IO;
using MoviesAPI.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using MoviesAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using MoviesAPI.Filters;
using System;

namespace MovieAPI.Tests
{
    public class MovieControllerTests
    {
        private readonly Mock<IMoviesAPIService> mockMovieAPIService;

        private readonly Assembly assembly = Assembly.GetExecutingAssembly();

        private readonly List<Movie> moviesList;
        public  MovieControllerTests()
        {
            mockMovieAPIService = new Mock<IMoviesAPIService>();

            using (var reader = new StreamReader(assembly.GetManifestResourceStream("moviesListResponse.json")))
            {
                var json = reader.ReadToEnd();
                moviesList = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        [Fact]
        public void GetAllMovies_SimpleTest()
        {
            // ARRANGE
            MoviesController moviesController = new MoviesController(mockMovieAPIService.Object);
             
            // SETUP
            mockMovieAPIService
                .Setup(service => service.GetMovies())
                .Returns(moviesList);

            // ACT
            var result = moviesController.GetAllMovies() as ObjectResult;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(moviesList, result.Value as List<Movie>);
        }

        [Fact]
        public void GetAllMovies_ErrorTest()
        {
            // ARRANGE
            MoviesController moviesController = new MoviesController(mockMovieAPIService.Object);

            // SETUP
            mockMovieAPIService
                .Setup(service => service.GetMovies())
                .Throws(new InvalidOperationException());

            // ACT
            Assert.Throws<InvalidOperationException>(() => moviesController.GetAllMovies());

        }

        [Fact]
        public void CreateMovie_ErrorTest()
        {
            // ARRANGE
            MoviesController moviesController = new MoviesController(mockMovieAPIService.Object);

            // SETUP
            mockMovieAPIService
                .Setup(service => service.CreateMovie(It.IsAny<Movie>()))
                .Throws(new InvalidDataException());

            // ACT
             Assert.Throws<InvalidDataException>(() => moviesController.CreateMovie(new Mock<Movie>().Object));
        }

        [Fact]
        public void CreateMovie_SimpleTest()
        {
            // ARRANGE
            MoviesController moviesController = new MoviesController(mockMovieAPIService.Object);

            // SETUP
            mockMovieAPIService
                .Setup(service => service.CreateMovie(It.IsAny<Movie>()));

            // ACT
            var result = moviesController.CreateMovie(new Movie()) as ObjectResult;

            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public void UpdateMovie_ErrorTest()
        {
            // ARRANGE
            MoviesController moviesController = new MoviesController(mockMovieAPIService.Object);

            // SETUP
            mockMovieAPIService
                .Setup(service => service.UpdateMovie(It.IsAny<Movie>()))
                .Throws(new KeyNotFoundException());

            // ACT
            Assert.Throws<KeyNotFoundException>(() => moviesController.UpdateMovie(new Mock<Movie>().Object));

        }

        [Fact]
        public void UpdateMovie_SimpleTest()
        {
            // ARRANGE
            MoviesController moviesController = new MoviesController(mockMovieAPIService.Object);

            // SETUP
            mockMovieAPIService
                .Setup(service => service.UpdateMovie(It.IsAny<Movie>()));

            // ACT
            var result = moviesController.UpdateMovie(new Movie()) as ObjectResult;

            Assert.Equal(202, result.StatusCode);
        }
    }
}