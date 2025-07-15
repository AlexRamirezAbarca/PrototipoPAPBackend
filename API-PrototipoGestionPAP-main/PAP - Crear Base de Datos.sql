/*
    Script para la creación y configuración de la base de datos DBGestionPAP junto a un usuario administrador.
    Se desactiva temporalmente la política de contraseñas para entornos de desarrollo.
*/

-- 1. Crear la base de datos
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DBGestionPAP')
BEGIN
    CREATE DATABASE DBGestionPAP;
    PRINT 'Base de datos DBGestionPAP creada.';
END
GO

-- 2. Crear login con política de contraseñas desactivada
USE [master]
GO
IF NOT EXISTS (SELECT name FROM sys.sql_logins WHERE name = 'papAdmin')
BEGIN
    CREATE LOGIN papAdmin 
    WITH PASSWORD = 'PapPassword123', 
    CHECK_POLICY = OFF;  -- Solo para desarrollo ▲
    PRINT 'Login papAdmin creado.';
END
GO

-- 3. Crear usuario en la base de datos
USE DBGestionPAP;
GO
IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = 'papAdmin')
BEGIN
    CREATE USER papAdmin FOR LOGIN papAdmin;
    PRINT 'Usuario papAdmin creado en DBGestionPAP.';
END
GO

-- 4. Asignar permisos completos
ALTER ROLE db_owner ADD MEMBER papAdmin;
PRINT 'Rol db_owner asignado correctamente.';
GO