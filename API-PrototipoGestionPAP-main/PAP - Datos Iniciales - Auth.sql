-- 1. Insertar Roles
INSERT INTO Roles (nombre, descripcion) 
VALUES 
('Administrador', 'Rol con todos los permisos del sistema'),
('Lector', 'Rol con permisos de solo lectura');

-- 2. Insertar Permisos para todas las entidades (23 entidades × 4 permisos)
INSERT INTO Permisos (codigo) VALUES
-- Usuarios
('Usuarios|Lectura'),('Usuarios|Escritura'),('Usuarios|Actualización'),('Usuarios|Eliminación'),
-- Personas
('Personas|Lectura'),('Personas|Escritura'),('Personas|Actualización'),('Personas|Eliminación'),
-- Roles
('Roles|Lectura'),('Roles|Escritura'),('Roles|Actualización'),('Roles|Eliminación'),
-- Reportes
('Reportes|Lectura'),('Reportes|Escritura'),('Reportes|Actualización'),('Reportes|Eliminación'),
-- Acciones
('Acciones|Lectura'),('Acciones|Escritura'),('Acciones|Actualización'),('Acciones|Eliminación'),
-- Carreras
('Carreras|Lectura'),('Carreras|Escritura'),('Carreras|Actualización'),('Carreras|Eliminación'),
-- EjesPlanNacionalDesarrollo
('EjesPlanNacionalDesarrollo|Lectura'),('EjesPlanNacionalDesarrollo|Escritura'),('EjesPlanNacionalDesarrollo|Actualización'),('EjesPlanNacionalDesarrollo|Eliminación'),
-- Facultades
('Facultades|Lectura'),('Facultades|Escritura'),('Facultades|Actualización'),('Facultades|Eliminación'),
-- MetasPlanNacionalDesarrollo
('MetasPlanNacionalDesarrollo|Lectura'),('MetasPlanNacionalDesarrollo|Escritura'),('MetasPlanNacionalDesarrollo|Actualización'),('MetasPlanNacionalDesarrollo|Eliminación'),
-- ObjetivosEstrategicosInstitucionales
('ObjetivosEstrategicosInstitucionales|Lectura'),('ObjetivosEstrategicosInstitucionales|Escritura'),('ObjetivosEstrategicosInstitucionales|Actualización'),('ObjetivosEstrategicosInstitucionales|Eliminación'),
-- ObjetivosOperativos
('ObjetivosOperativos|Lectura'),('ObjetivosOperativos|Escritura'),('ObjetivosOperativos|Actualización'),('ObjetivosOperativos|Eliminación'),
-- ObjetivosPlanNacionalDesarrollo
('ObjetivosPlanNacionalDesarrollo|Lectura'),('ObjetivosPlanNacionalDesarrollo|Escritura'),('ObjetivosPlanNacionalDesarrollo|Actualización'),('ObjetivosPlanNacionalDesarrollo|Eliminación'),
-- PoliticasPlanNacionalDesarrollo
('PoliticasPlanNacionalDesarrollo|Lectura'),('PoliticasPlanNacionalDesarrollo|Escritura'),('PoliticasPlanNacionalDesarrollo|Actualización'),('PoliticasPlanNacionalDesarrollo|Eliminación'),
-- ProductosInstitucionales
('ProductosInstitucionales|Lectura'),('ProductosInstitucionales|Escritura'),('ProductosInstitucionales|Actualización'),('ProductosInstitucionales|Eliminación'),
-- ProgramasInstitucionales
('ProgramasInstitucionales|Lectura'),('ProgramasInstitucionales|Escritura'),('ProgramasInstitucionales|Actualización'),('ProgramasInstitucionales|Eliminación'),
-- ProgramasNacionales
('ProgramasNacionales|Lectura'),('ProgramasNacionales|Escritura'),('ProgramasNacionales|Actualización'),('ProgramasNacionales|Eliminación'),
-- ProgramasPresupuestarios
('ProgramasPresupuestarios|Lectura'),('ProgramasPresupuestarios|Escritura'),('ProgramasPresupuestarios|Actualización'),('ProgramasPresupuestarios|Eliminación'),
-- UnidadesEjecutoras
('UnidadesEjecutoras|Lectura'),('UnidadesEjecutoras|Escritura'),('UnidadesEjecutoras|Actualización'),('UnidadesEjecutoras|Eliminación'),
-- UnidadesResponsables
('UnidadesResponsables|Lectura'),('UnidadesResponsables|Escritura'),('UnidadesResponsables|Actualización'),('UnidadesResponsables|Eliminación'),
-- Actividades
('Actividades|Lectura'),('Actividades|Escritura'),('Actividades|Actualización'),('Actividades|Eliminación'),
-- ObrasTareas
('ObrasTareas|Lectura'),('ObrasTareas|Escritura'),('ObrasTareas|Actualización'),('ObrasTareas|Eliminación'),
-- EjecucionesMensuales
('EjecucionesMensuales|Lectura'),('EjecucionesMensuales|Escritura'),('EjecucionesMensuales|Actualización'),('EjecucionesMensuales|Eliminación'),
-- Indicadores
('Indicadores|Lectura'),('Indicadores|Escritura'),('Indicadores|Actualización'),('Indicadores|Eliminación');

-- 3. Asignar permisos a roles
-- Administrador (todos los permisos)
INSERT INTO RolPermisos (rol_id, permiso_id)
SELECT 
    (SELECT rol_id FROM Roles WHERE nombre = 'Administrador'), 
    permiso_id 
FROM Permisos;

-- Lector (solo lectura)
INSERT INTO RolPermisos (rol_id, permiso_id)
SELECT 
    (SELECT rol_id FROM Roles WHERE nombre = 'Lector'), 
    permiso_id 
FROM Permisos 
WHERE codigo LIKE '%|Lectura';

-- 4. Insertar usuario administrador con contraseña encriptada
-- Se utiliza un hash BCrypt precomputado para la contraseña de 'admin', que es 'Admin2025'
DECLARE @PasswordHash NVARCHAR(255) = '$2a$11$Aq63rq/vlNxKH4QVxLnJWO2ZFEkwdioU0rVmwAPngPg69W0eYERHW'; -- 'Admin2025'

INSERT INTO Usuarios (rol_id, nombre_usuario, contraseña)
SELECT 
    rol_id,
    'admin',
    @PasswordHash
FROM Roles 
WHERE nombre = 'Administrador';

