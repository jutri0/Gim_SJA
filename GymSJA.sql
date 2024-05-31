CREATE TABLE Usuarios (
    IdUsuarioPk INT IDENTITY(1,1) PRIMARY KEY,
    Usuario VARCHAR(MAX),
    Password VARCHAR(255), -- Changed to VARCHAR for security purposes
    Estado INT
);

CREATE TABLE Devoluciones (
    IdDevolucionPk INT IDENTITY(1,1) PRIMARY KEY,
    AlumnoDevolucion VARCHAR(MAX),
    Prestamo INT,
    FechaDevolucion DATE, -- Changed to DATE for storing dates
    Equipo INT,
    Observaciones VARCHAR(MAX),
    EstadoDevolucion INT,

);

CREATE TABLE Prestamos (
    IdPrestamoPk INT IDENTITY(1,1) PRIMARY KEY,
    AlumnoPrestamo VARCHAR(MAX),
    FechaPrestamo DATE, -- Changed to DATE for storing dates
    Equipo INT,
    CantidadEquipo INT,
    Observaciones VARCHAR(MAX)
);

CREATE TABLE Equipos (
    IdEquipoPk INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(MAX),
    Cantidad INT,
    CantidadMax INT,
    Observaciones VARCHAR(MAX),
    FotoEquipo VARCHAR(MAX),
);

CREATE TABLE Alumnos (
    IdAlumnoPk INT IDENTITY(1,1) PRIMARY KEY,
    Codigo INT,
    Nombres VARCHAR(MAX),
    Curso VARCHAR(200),
    Correo VARCHAR(200),
    CorreoPaternal VARCHAR(200)
);
