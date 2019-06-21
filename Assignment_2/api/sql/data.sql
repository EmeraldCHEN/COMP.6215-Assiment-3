/*  

MAINTAINED BY JEFF Kranenburg

Uncomment LINE 6 - 13 to recreate the database

*/



USE Master

Go



If EXISTS(select * from sys.databases where name = 'DB_6215_19_S1')

DROP DATABASE DB_6215_19_S1



CREATE DATABASE DB_6215_19_S1

Go



USE DB_6215_19_S1



DROP TABLE IF EXISTS "users";

DROP TABLE IF EXISTS "movies";

DROP TABLE IF EXISTS "wishlist";



SET QUOTED_IDENTIFIER OFF

SET ANSI_NULLS ON 

Go



CREATE TABLE users (

  ID int NOT NULL PRIMARY KEY IDENTITY(1, 1),

  Fname varchar(100) NOT NULL,

  Lname varchar(255) NOT NULL,

  Uname varchar(255) NOT NULL,

  Passwrd varchar(255) NOT NULL,

  Email varchar(255) NOT NULL

)



CREATE TABLE movies (

  ID int NOT NULL PRIMARY KEY IDENTITY(1, 1),

  Title varchar(100) NOT NULL,

  Summary varchar(500) NOT NULL,

  Picture varchar(255) NOT NULL,

  Genre varchar(255) NOT NULL,

  Rating float NOT NULL

)



CREATE TABLE wishlist (

  ID int NOT NULL PRIMARY KEY IDENTITY(1, 1),

  Pid int NOT NULL,

  Mid int NOT NULL

)



INSERT INTO users (Fname, Lname, Uname, Passwrd, Email) VALUES ("Steve", "Rogers", "SRogers", "America1", "steve.rogers@avengers.com"), ("Tony", "Stark", "TStark", "Iron1", "tony.stark@avengers.com");



INSERT INTO movies (Title, Summary, Picture, Genre, Rating) VALUES ("Avengers: End Game", "After the devastating events of Avengers: Infinity War (2018), the universe is in ruins. With the help of remaining allies, the Avengers assemble once more in order to undo Thanos' actions and restore order to the universe.", "MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzI2NzQ2NzM@._V1_SY1000_CR0,0,674,1000_AL_", "Action" ,8.8)



INSERT INTO movies (Title, Summary, Picture, Genre, Rating) VALUES ("The Godfather", "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", "MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_SY1000_CR0,0,704,1000_AL_", "Crime Drama", 9.2)



INSERT INTO movies (Title, Summary, Picture, Genre, Rating) VALUES ("The Mask", "Bank clerk Stanley Ipkiss is transformed into a manic superhero when he wears a mysterious mask.", "MV5BOWExYjI5MzktNTRhNi00Nzg2LThkZmQtYWVkYjRlYWI2MDQ4XkEyXkFqcGdeQXVyNTAyODkwOQ@@._V1_SY1000_CR0,0,672,1000_AL_",  "Comedy", 6.8)



INSERT INTO movies (Title, Summary, Picture, Genre, Rating) VALUES ("Your Name", "Two strangers find themselves linked in a bizarre way. When a connection forms, will distance be the only thing to keep them apart?", "MV5BODRmZDVmNzUtZDA4ZC00NjhkLWI2M2UtN2M0ZDIzNDcxYThjL2ltYWdlXkEyXkFqcGdeQXVyNTk0MzMzODA@._V1_SY1000_SX675_AL_",  "Anime", 8.4)



-- https://m.media-amazon.com/images/M/MV5BODRmZDVmNzUtZDA4ZC00NjhkLWI2M2UtN2M0ZDIzNDcxYThjL2ltYWdlXkEyXkFqcGdeQXVyNTk0MzMzODA@._V1_SY1000_SX675_AL_.jpg

-- SELECT * FROM users;
-- SELECT * FROM movies;

-- SELECT * FROM wishlist;