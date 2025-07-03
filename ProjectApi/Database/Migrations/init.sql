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

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(200),
    RoleId INT FOREIGN KEY REFERENCES Roles(Id),
    SchoolId INT FOREIGN KEY REFERENCES Schools(Id),
    FullName NVARCHAR(100),
    Grade INT,
    ResetToken NVARCHAR(200),
    Username NVARCHAR(100)
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

-- Таблиця відповідей
CREATE TABLE Responses (
    ResponseID INT PRIMARY KEY IDENTITY,
    StudentID INT NOT NULL,
    QuestionID INT NOT NULL,
    AnswerValue INT NOT NULL, -- 1-5 або 1-7
    CONSTRAINT FK_Responses_Student FOREIGN KEY (StudentID) REFERENCES Users(Id),
    CONSTRAINT FK_Responses_Question FOREIGN KEY (QuestionID) REFERENCES Questions(Id)
);

-- Таблиця питань
CREATE TABLE Questions (
    Id INT PRIMARY KEY IDENTITY,
    Text NVARCHAR(500) NOT NULL
);

-- Заповнення ролей
INSERT INTO Roles (Name) VALUES ('Student'), ('Teacher'), ('Deputy'), ('Director'), ('Admin');

-- Заповнення предметів
INSERT INTO Subjects (Name) VALUES ('Science'), ('Technology'), ('Engineering'), ('Mathematics');

-- Отримання середнього балу
-- SELECT AVG(Grade) as AverageGrade FROM StudentGrades WHERE StudentId = @StudentId