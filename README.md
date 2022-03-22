# MoviesAPI
Created API endpoints to perform get movies, create movie and update movie.

The Database tables are created using Entity Framework (EF) Code First approach.

The Database script(https://github.com/Raghuram34/MoviesAPI/blob/master/MoviesAPI/DBScripts/script.sql) is generated using EF Core Script-Migration command.

## GET ALL MOVIES
Request GET /api/Movies/

It return the list of movies. The sample response is https://github.com/Raghuram34/MoviesAPI/blob/master/MovieAPI.Tests/MockData/moviesListResponse.json

## Create A Movie
Request POST /api/Movies

It takes a movie object which is not null or invalid object. It should send using body of the request. 

The sample model:

![image](https://user-images.githubusercontent.com/54551516/159462839-8282408f-f957-4c11-ac7e-3216a480af9b.png)

## Edit Movie/Update Movie
Request PUT /api/Movies

It takes a movies which is not null or invalid object. It should send using body of the request only.

The sample model:

![image](https://user-images.githubusercontent.com/54551516/159463492-339b3433-050f-43f6-b3ac-ad0fa47b9527.png)

