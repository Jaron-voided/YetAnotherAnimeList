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

## Running Tests

From the root of the project:

```bash
# Run all tests
dotnet test

# Run a specific test project
dotnet test Tests/AnimeList.Tests.Unit
dotnet test Tests/AnimeList.Tests.Integration
```

## AI Usage

AI assistance was used in this project in accordance with course policy. The following files were written with AI help under direct instruction:

- `AnimeList.Web/src/features/anime/card/AnimeCard.module.css` — CSS styling for the anime card component
- `AnimeList.Web/src/features/anime/list/AnimeList.module.css` — CSS grid layout for the anime list

Both files are commented accordingly and were reviewed and approved before use. All other code was written by the student.

## Reflection

**What did you learn from this project?**

This project taught me how to architect and structure a larger application from the ground up. Working with a significant volume of data highlighted how important it is to think carefully about efficiency — both in how data is stored and how it's retrieved. It also reinforced how critical upfront planning is: taking the time to map out the structure before writing code kept the project far more organized and maintainable than it would have been otherwise.

**What did you learn from this course?**

The course gave me a much clearer picture of what day-to-day software development actually looks like as a career. It reinforced the value of things that are easy to overlook — proper testing, thoughtful project structure, and clean separation of concerns. These aren't just good habits; they're what separates maintainable code from code that becomes a liability.

**If you had more time, what would you have done differently?**

I would have invested more time upfront in organizing the project structure and making better use of interfaces and inheritance to reduce duplication. On the feature side, I would have built out more API endpoints and consumed them in the frontend to give users a richer, more complete experience.
