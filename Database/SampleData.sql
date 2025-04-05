INSERT INTO Exercises (Name, Description, MuscleGroup, DifficultyLevel) VALUES
('Bench Press', 'Standard bench press exercise', 'Chest', 'Intermediate'),
('Squats', 'Bodyweight or weighted squats', 'Legs', 'Beginner'),
('Deadlift', 'Standard deadlift exercise', 'Back', 'Advanced');

INSERT INTO Workouts (ExerciseId, DatePerformed, Sets, Reps, Weight, DurationMinutes, Notes) VALUES
(1, '2024-03-20 10:00:00', 3, 10, 135.00, 15, 'Good form, felt strong'),
(2, '2024-03-20 10:30:00', 4, 15, 0.00, 20, 'Bodyweight only'),
(3, '2024-03-21 09:00:00', 3, 8, 225.00, 20, 'Heavy set'); 