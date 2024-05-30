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
    FOREIGN KEY (Prestamo) REFERENCES Prestamos(IdPrestamoPk),
    FOREIGN KEY (Equipo) REFERENCES Equipos(IdEquipoPk)
);

CREATE TABLE Prestamos (
    IdPrestamoPk INT IDENTITY(1,1) PRIMARY KEY,
    AlumnoPrestamo VARCHAR(MAX),
    FechaPrestamo DATE, -- Changed to DATE for storing dates
    Equipo INT,
    CantidadEquipo INT,
    Observaciones VARCHAR(MAX),
    FOREIGN KEY (Equipo) REFERENCES Equipos(IdEquipoPk)
);

CREATE TABLE Equipos (
    IdEquipoPk INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(MAX),
    Cantidad INT,
    CantidadMax INT,
    Observaciones VARCHAR(MAX),
    FotoEquipo image,
);

CREATE TABLE Alumnos (
    IdAlumnoPk INT IDENTITY(1,1) PRIMARY KEY,
    Codigo INT,
    Nombres VARCHAR(MAX),
    Curso VARCHAR(200),
    Correo VARCHAR(200),
    CorreoPaternal VARCHAR(200)
);

-- Trigger para verificar la cantidad de equipos disponibles

CREATE TRIGGER trg_AfterInsertPrestamo
ON Prestamos
AFTER INSERT
AS
BEGIN
    DECLARE @EquipoId INT, @CantidadPrestada INT

    SELECT @EquipoId = i.Equipo, @CantidadPrestada = i.CantidadEquipo
    FROM inserted i

    -- Verificar que haya suficientes equipos disponibles
    IF EXISTS (SELECT 1 FROM Equipos WHERE IdEquipoPk = @EquipoId AND Cantidad >= @CantidadPrestada)
    BEGIN
        -- Actualizar la cantidad de equipos disponibles
        UPDATE Equipos
        SET Cantidad = Cantidad - @CantidadPrestada
        WHERE IdEquipoPk = @EquipoId
    END
    ELSE
    BEGIN
        -- Deshacer el insert si no hay suficientes equipos disponibles
        ROLLBACK TRANSACTION
        RAISERROR ('No hay suficientes equipos disponibles para este préstamo.', 16, 1)
    END
END

-- Trigger para verificar la cantidad de equipos devueltos

CREATE TRIGGER trg_AfterInsertDevolucion
ON Devoluciones
AFTER INSERT
AS
BEGIN
    DECLARE @EquipoId INT, @CantidadDevuelta INT

    SELECT @EquipoId = i.Equipo, @CantidadDevuelta = (SELECT CantidadEquipo FROM Prestamos WHERE IdPrestamoPk = i.Prestamo)
    FROM inserted i

    -- Verificar que la cantidad no exceda la cantidad máxima permitida
    IF EXISTS (SELECT 1 FROM Equipos WHERE IdEquipoPk = @EquipoId AND (Cantidad + @CantidadDevuelta) <= CantidadMax)
    BEGIN
        -- Actualizar la cantidad de equipos disponibles
        UPDATE Equipos
        SET Cantidad = Cantidad + @CantidadDevuelta
        WHERE IdEquipoPk = @EquipoId
    END
    ELSE
    BEGIN
        -- Deshacer el insert si la cantidad excede la cantidad máxima permitida
        ROLLBACK TRANSACTION
        RAISERROR ('La devolución excede la cantidad máxima permitida para este equipo.', 16, 1)
    END
END
