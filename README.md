# YetAnotherAnimeList

A full-stack anime list application with an ASP.NET Core 8 REST API backend and a React + TypeScript frontend.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js LTS](https://nodejs.org/)

## Data Setup

The CSV data files are not included in the repository. Download them from the link below and place all four files into the `Data/CSVs/` folder at the root of the project:

**[Download CSVs](https://drive.google.com/drive/u/2/folders/1vKGKImVxT3nvZKzPIH0fkNH1zC7qLLne)**

Expected files:
```
Data/
└── CSVs/
    ├── details.csv
    ├── stats.csv
    ├── recommendations.csv
    └── ratings.csv
```

The SQLite database is created and seeded automatically the first time the API starts — you do not need to set one up manually.

## Running the Project

### 1. Start the API

```bash
dotnet run --project AnimeList.API
```

The API will be available at `https://localhost:5001`. On first run it will create the database and load all CSV data (this may take a moment).

### 2. Start the Frontend

In a separate terminal:

```bash
cd AnimeList.Web
npm install
npm run dev
```

The app will be available at `https://localhost:3000`.

> **Note:** Your browser may warn about a self-signed certificate. This is expected for local development — click through to proceed.
