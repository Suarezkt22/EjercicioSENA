-- 1. Crear la base de datos
USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'StudentsDB')
BEGIN
    ALTER DATABASE StudentsDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE StudentsDB;
END
GO

CREATE DATABASE StudentsDB;
GO

USE StudentsDB;
GO

-- 2. Creación de tablas
CREATE TABLE Programs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Credits INT NOT NULL
);

CREATE TABLE Teachers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Courses (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Credits INT NOT NULL CHECK (Credits = 3),
    TeacherId INT NOT NULL,
    CONSTRAINT FK_Courses_Teacher FOREIGN KEY (TeacherId) REFERENCES Teachers(Id)
);

CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    ProgramId INT NULL,
    CONSTRAINT FK_Students_Programs FOREIGN KEY (ProgramId) REFERENCES Programs(Id)
);

-- Tablas de relación muchos-a-muchos
CREATE TABLE ProgramCourses (
    ProgramId INT NOT NULL,
    CourseId INT NOT NULL,
    CONSTRAINT PK_ProgramCourses PRIMARY KEY (ProgramId, CourseId),
    CONSTRAINT FK_ProgramCourses_Programs FOREIGN KEY (ProgramId) REFERENCES Programs(Id),
    CONSTRAINT FK_ProgramCourses_Courses FOREIGN KEY (CourseId) REFERENCES Courses(Id)
);

CREATE TABLE StudentCourses (
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    CONSTRAINT PK_StudentCourses PRIMARY KEY (StudentId, CourseId),
    CONSTRAINT FK_StudentCourses_Students FOREIGN KEY (StudentId) REFERENCES Students(Id),
    CONSTRAINT FK_StudentCourses_Courses FOREIGN KEY (CourseId) REFERENCES Courses(Id)
);

-- 3. Insertar 5 profesores
INSERT INTO Teachers (Name) VALUES
('Profesor García'),
('Profesora Martínez'),
('Profesor Rodríguez'),
('Profesora López'),
('Profesor Sánchez');

-- 4. Insertar 10 materias (cursos)
INSERT INTO Courses (Name, Credits, TeacherId) VALUES
('Matemáticas Avanzadas', 3, 1),
('Literatura Contemporánea', 3, 1),
('Física Cuántica', 3, 2),
('Historia del Arte', 3, 2),
('Programación Orientada a Objetos', 3, 3),
('Biología Molecular', 3, 3),
('Economía Internacional', 3, 4),
('Química Orgánica', 3, 4),
('Filosofía Moderna', 3, 5),
('Ingeniería de Software', 3, 5);

-- 5. Insertar un programa académico
INSERT INTO Programs (Name, Credits) VALUES
('Ingeniería de Sistemas', 180);

-- 6. Asociar todas las materias al programa (CORREGIDO: Usando IDs 1-10)
INSERT INTO ProgramCourses (ProgramId, CourseId) VALUES
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5),
(1, 6), (1, 7), (1, 8), (1, 9), (1, 10);


-- 9. Verificación de datos insertados
SELECT 'Programas' AS Tabla, COUNT(*) AS Registros FROM Programs
UNION ALL
SELECT 'Profesores', COUNT(*) FROM Teachers
UNION ALL
SELECT 'Materias', COUNT(*) FROM Courses
UNION ALL
SELECT 'Materias en Programa', COUNT(*) FROM ProgramCourses
UNION ALL
SELECT 'Estudiantes', COUNT(*) FROM Students
UNION ALL
SELECT 'Materias de Estudiantes', COUNT(*) FROM StudentCourses;