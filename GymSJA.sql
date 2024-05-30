CREATE TABLE Usuarios (
    IdUsuarioPk INT IDENTITY(1,1) PRIMARY KEY,
    Usuario VARCHAR(MAX),
    Password VARCHAR(MAX),
    Estado INT
);

CREATE TABLE Devoluciones (
    IdDevolucionPk INT IDENTITY(1,1) PRIMARY KEY,
    AlumnoDevolucion VARCHAR(MAX),
    Prestamo int,
    FechaDevolucion datetime,
    Equipo Int,
    Observaciones VARCHAR(MAX)
);

CREATE TABLE Prestamos (
    IdPrestamoPk INT IDENTITY(1,1) PRIMARY KEY,
    AlumnoPrestamo VARCHAR(MAX),
    FechaPrestamo datetime,
    Equipo Int,
    CantidadEquipo INT,
    Observaciones VARCHAR(MAX)
);

CREATE TABLE Equipos (
    IdEquipoPk INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(MAX),
    Cantidad INT,
    CantidadMax INT,
    Observaciones VARCHAR(MAX),
    FotoEquipo VARCHAR (MAX)
);

CREATE TABLE Alumnos (
    IdAlumnoPk INT IDENTITY(1,1) PRIMARY KEY,
    Codigo INT,
    Nombres VARCHAR(MAX),
    Curso VARCHAR(200),
    Correo VARCHAR(200),
    CorreoPaternal VARCHAR(200)
);
