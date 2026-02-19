# AniBento

## ! IMPORTANT !
The project is **currently configured for development** and will drop the database on every launch

### Full Stack Demo Application For Media Management
- User defined media, I wanted to stay away from calling IMDB or AniDB endpoints to make it as much of my own business logic as possible
- ASP.NET Core Web Api back end server
- ASP.NET Identiy authentication 
- Fully dockerized with docker compose
- Spins up a container of a database and a container of the server its self
- Clean code architecture (to the best of my ability)

--> Planned: NextJS React front-end

## Current Features
### ASP.NET Identity Core authentication and authorization
Fully secure identity database schema with built in password hashing, claims management

### ASP.NET EntityFrameworkCore
Model-based Object Relational Mapping to easily develop typesafe database code in ASP.NET

### /api/Media Endpoint
Manages canonical medias in the database, and currently accepts user inputs to create your own input
Supports:
  - Get
  - Post
  - Put (TODO: Change to PATCH)
  - Delete
  - Pagination
  - ASP.NET Identity cookie authorization on destructive methods

### /api/Auth Endpoint
Manages user account creation, login, and claims service 

### /api/Library Endpoint
Manages medias that are scoped to a user library, sort of like how spotify has a gazillion songs but will aggregate the ones you've chosen to + on your profiel

### /api/Users controller
Main routing for front end UI, viewing user profiles, user collections, etc

### /api/Collections 
Endpoints for collection CRUD 

### /api/Genres
Endpoint for front end UI consumption 

## Starting
With docker desktop: `docker compose up` -> Starts AniBento.Api Server Container + Starts AniBentoDb Postgres Instance Container

## Swagger endpoint
`http://localhost:5163/swagger`
