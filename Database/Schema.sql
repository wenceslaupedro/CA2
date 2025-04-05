CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500)
);

CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Price DECIMAL(10,2) NOT NULL,
    CategoryId INT NOT NULL,
    StockQuantity INT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

CREATE TABLE Exercises (
    ExerciseId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    MuscleGroup NVARCHAR(100),
    DifficultyLevel NVARCHAR(50)
);

CREATE TABLE Workouts (
    WorkoutId INT PRIMARY KEY IDENTITY(1,1),
    ExerciseId INT NOT NULL,
    DatePerformed DATETIME NOT NULL,
    Sets INT NOT NULL,
    Reps INT NOT NULL,
    Weight DECIMAL(10,2),
    DurationMinutes INT,
    Notes NVARCHAR(500),
    FOREIGN KEY (ExerciseId) REFERENCES Exercises(ExerciseId)
);

-- Insert sample data
INSERT INTO Categories (Name, Description) VALUES
('Electronics', 'Electronic devices and accessories'),
('Clothing', 'Apparel and fashion items');

INSERT INTO Products (Name, Description, Price, CategoryId, StockQuantity) VALUES
('Laptop', 'High-performance laptop', 999.99, 1, 10),
('Smartphone', 'Latest smartphone model', 699.99, 1, 20),
('T-Shirt', 'Cotton t-shirt', 19.99, 2, 50),
('Jeans', 'Blue denim jeans', 49.99, 2, 30);

INSERT INTO Exercises (Name, Description, MuscleGroup, DifficultyLevel) VALUES
('Bench Press', 'Standard bench press exercise', 'Chest', 'Intermediate'),
('Squats', 'Bodyweight or weighted squats', 'Legs', 'Beginner'),
('Deadlift', 'Standard deadlift exercise', 'Back', 'Advanced');

INSERT INTO Workouts (ExerciseId, DatePerformed, Sets, Reps, Weight, DurationMinutes, Notes) VALUES
(1, '2024-03-20 10:00:00', 3, 10, 135.00, 15, 'Good form, felt strong'),
(2, '2024-03-20 10:30:00', 4, 15, 0.00, 20, 'Bodyweight only'),
(3, '2024-03-21 09:00:00', 3, 8, 225.00, 20, 'Heavy set'); 