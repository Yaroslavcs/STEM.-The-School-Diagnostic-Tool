-- Таблиця шкіл
CREATE TABLE Schools (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL
);

-- Таблиця ролей
CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) NOT NULL
);

-- Таблиця користувачів
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(200),
    RoleId INT FOREIGN KEY REFERENCES Roles(Id),
    SchoolId INT FOREIGN KEY REFERENCES Schools(Id)
);

-- Таблиця завучів
CREATE TABLE Deputies (
    Id INT PRIMARY KEY IDENTITY,
    UserId INT FOREIGN KEY REFERENCES Users(Id),
    SchoolId INT FOREIGN KEY REFERENCES Schools(Id)
);

-- Таблиця директорів
CREATE TABLE Directors (
    Id INT PRIMARY KEY IDENTITY,
    UserId INT FOREIGN KEY REFERENCES Users(Id),
    SchoolId INT FOREIGN KEY REFERENCES Schools(Id)
);

-- Таблиця предметів
CREATE TABLE Subjects (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) NOT NULL
);

-- Таблиця рекомендацій
CREATE TABLE Recommendations (
    Id INT PRIMARY KEY IDENTITY,
    Text NVARCHAR(500),
    Grade INT,
    IsViewed BIT,
    Note NVARCHAR(500),
    StudentId INT FOREIGN KEY REFERENCES Users(Id),
    TeacherId INT FOREIGN KEY REFERENCES Users(Id)
);

-- Таблиця оцінювання учнями по предметах
CREATE TABLE StudentGrades (
    Id INT PRIMARY KEY IDENTITY,
    StudentId INT FOREIGN KEY REFERENCES Users(Id),
    SubjectId INT FOREIGN KEY REFERENCES Subjects(Id),
    Grade FLOAT CHECK (Grade >= 0 AND Grade <= 12)
);

-- Заповнення ролей
INSERT INTO Roles (Name) VALUES ('Student'), ('Teacher'), ('Deputy'), ('Director'), ('Admin');

-- Заповнення предметів
INSERT INTO Subjects (Name) VALUES ('Science'), ('Technology'), ('Engineering'), ('Mathematics');

-- Отримання середнього балу
SELECT AVG(Grade) as AverageGrade FROM StudentGrades WHERE StudentId = @StudentId
