# FarmerApp API

ASP.NET Core 8 Web API for farmer user registration, login, and JWT-protected endpoints.

## Tech stack

- .NET 8
- MongoDB
- JWT authentication

## Run locally

Set these environment variables before starting the API:

- `MongoDb__ConnectionString`
- `MongoDb__DatabaseName`
- `Jwt__Key`

Then run:

```powershell
dotnet run --launch-profile http
```

## Main endpoints

- `GET /api/auth/ping`
- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/test/protected`

## Render deployment

This repo includes [render.yaml](/C:/Farmer_app_backend/FarmerApp.Api/render.yaml), so Render can create the service from the repository.

Set these environment variables in Render:

- `MongoDb__ConnectionString`
- `MongoDb__DatabaseName`
- `Jwt__Key`
