# Gim_SJA

En el Stored Procedures de SQL Server ejecute estos comando en el orden presentado.

#StoredProcedures_1

USE [GymSJA]
GO

/** Object:  StoredProcedure [dbo].[LoginGym]    Script Date: 29/5/2024 22:18:25 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[LoginGym] 
    @user varchar(max),
    @pass varchar(max)
AS
BEGIN
    SET NOCOUNT ON;

    Declare @Count int, @Ing int;

    Set @Count =(Select Count(*)from Usuarios where usuario = @user and password =@pass and Estado =1)

    if (@Count =1 )
        begin
        Select IdUsuarioPk,
        Usuario,
        Estado
        from Usuarios where usuario = @user and password =@pass and Estado =1
        end
     else
        begin
        select  0
        end


END
GO

#StoredProcedures_2

USE [GymSJA]
GO

/** Object:  StoredProcedure [dbo].[getAlumnosSelect]    Script Date: 29/5/2024 22:21:28 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
Create PROCEDURE [dbo].[getAlumnosSelect] 
AS
BEGIN
    SET NOCOUNT ON;

        Select IdAlumnoPk,
        Nombres
        from Alumnos 
    END
GO

#StoredProcedures_3

USE [GymSJA]
GO

/** Object:  StoredProcedure [dbo].[getEquiposSelect]    Script Date: 29/5/2024 22:21:43 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
Create PROCEDURE [dbo].[getEquiposSelect] 
AS
BEGIN
    SET NOCOUNT ON;

        Select IdEquipoPk,
        Nombre
        from Equipos 
    END
GO

#StoredProcedures_4

USE [GymSJA]
GO

/** Object:  StoredProcedure [dbo].[getPrestamos]    Script Date: 29/5/2024 22:22:02 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
Create PROCEDURE [dbo].[getPrestamos] 
AS
BEGIN
    SET NOCOUNT ON;

        Select P.IdPrestamoPk,
        A.Nombres AlumnoPrestamo,
        P.FechaPrestamo FechaPrestamo,
        E.Nombre Equipo,
        P.CantidadEquipo CantidadEquipo,
        p.Observaciones 
        from Prestamos P
        inner join Alumnos A on A.IdAlumnoPk = P.AlumnoPrestamo
        inner join Equipos E on E.IdEquipoPk = P.Equipo
    END
GO

#StoredProcedures_5

USE [GymSJA]
GO

/** Object:  StoredProcedure [dbo].[insertPrestamo]    Script Date: 29/5/2024 22:22:10 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[insertPrestamo] 
@alumno int,
@fecha DateTime,
@equipo int,
@cantidad int,
@observaciones varchar(max)
AS
BEGIN
    SET NOCOUNT ON;

        Begin try
        Insert into Prestamos(AlumnoPrestamo,FechaPrestamo,Equipo,CantidadEquipo,Observaciones)
        Values(@alumno,@fecha,@equipo,@cantidad,@observaciones)

        Select 0 AS Id
        end try
        begin catch
        Select 1 AS Id
        end catch
    END

GO
