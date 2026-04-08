# YetAnotherAnimeList

A full-stack anime list application with an ASP.NET Core 8 REST API backend and a React + TypeScript frontend.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js LTS](https://nodejs.org/)

## Data Setup

The CSV and database files are not included in the repository. Download them from the link below:

**[Download Data Files](https://drive.google.com/drive/u/2/folders/1vKGKImVxT3nvZKzPIH0fkNH1zC7qLLne)**

Place the files as follows:

**CSVs** → `Data/CSVs/` (at the root of the project)
```
Data/
└── CSVs/
    ├── details.csv
    ├── stats.csv
    ├── recommendations.csv
    └── ratings.csv
```

**Database** → `AnimeList.Persistence/Data/Database/`
```
AnimeList.Persistence/
└── Data/
    └── Database/
        └── anime.db
```

> **Note:** If you place the database file, it will be used as-is. If you omit it, the database will be created and seeded automatically from the CSVs on first run (this may take a moment).

## Running the Project

### 1. Trust the development certificate

Run this once from anywhere on your machine:

```bash
dotnet dev-certs https --trust
```

### 2. Start the API

From the root of the project:

```bash
dotnet run --project AnimeList.API
```

The API will be available at `https://localhost:5001`.

### 3. Start the Frontend

In a separate terminal, from the root of the project:

```bash
cd AnimeList.Web
npm install
npm run dev
```

The app will be available at `https://localhost:3000`.

> **Note:** Your browser may warn about a self-signed certificate. This is expected for local development — click through to proceed.
