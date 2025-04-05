# Workout Tracker System

A complete workout tracking system that includes:
- SQL Server database with Exercises and Workouts tables
- ASP.NET Core MVC web application for managing workouts
- RESTful API for workout analysis
- Console client to display workout statistics

## Database Schema

### Exercises Table
- ExerciseId (PK)
- Name
- Description
- MuscleGroup
- DifficultyLevel

### Workouts Table
- WorkoutId (PK)
- ExerciseId (FK)
- DatePerformed
- Sets
- Reps
- Weight
- DurationMinutes
- Notes

## API Endpoints

### Analysis
- GET /api/analysis/workout-stats
  Returns statistics about workouts including:
  - Total number of workouts
  - Number of different exercises
  - Average sets and reps
  - Total volume
  - Most performed exercise

## Setup Instructions

1. Create a SQL Server database
2. Run the Schema.sql script to create tables and sample data
3. Update connection strings in appsettings.json
4. Start the web application
5. Run the console client to view statistics

## Projects

- WorkoutTracker.Web: MVC application for managing workouts
- WorkoutTracker.API: RESTful API for workout analysis
- WorkoutTracker.Client: Console application to display statistics 