/*
    Script de creación de tablas principales, autenticación, catálogos,
    y las entidades principales de la PAP.
*/

USE DBGestionPAP;
GO

-- Tabla Roles: Almacena los diferentes roles que los usuarios pueden tener en el sistema.
CREATE TABLE Roles (
    rol_id INT IDENTITY (1, 1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL UNIQUE,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla Personas: Registra la información personal de las personas que interactúan con el sistema.
CREATE TABLE Personas (
    persona_id INT IDENTITY (1, 1) PRIMARY KEY,
    cedula NVARCHAR (20) NOT NULL UNIQUE,
    nombre NVARCHAR (100) NOT NULL,
    apellido NVARCHAR (100) NOT NULL,
    correo_electronico NVARCHAR (255) NOT NULL,
    telefono NVARCHAR (20),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla Usuarios: Gestiona las credenciales y roles de los usuarios del sistema.
CREATE TABLE Usuarios (
    usuario_id INT IDENTITY (1, 1) PRIMARY KEY,
    persona_id INT NULL,
    rol_id INT NULL,
    nombre_usuario NVARCHAR (50) NOT NULL UNIQUE,
    contraseña NVARCHAR (255) NOT NULL,
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL,
    FOREIGN KEY (persona_id) REFERENCES Personas (persona_id),
    FOREIGN KEY (rol_id) REFERENCES Roles (rol_id)
);
GO

-- Tabla Permisos: Define los diferentes permisos que pueden asignarse a los roles.
CREATE TABLE Permisos (
    permiso_id INT IDENTITY (1, 1) PRIMARY KEY,
    codigo NVARCHAR (100) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla RolPermisos: Asocia permisos específicos a cada rol.
CREATE TABLE RolPermisos (
    rol_permiso_id INT IDENTITY (1, 1) PRIMARY KEY,
    rol_id INT NULL,
    permiso_id INT NULL,
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL,
    FOREIGN KEY (rol_id) REFERENCES Roles (rol_id),
    FOREIGN KEY (permiso_id) REFERENCES Permisos (permiso_id)
);
GO

-- Tabla RegistroSesiones: Registra las sesiones de los usuarios para auditoría y seguridad.
CREATE TABLE RegistroSesiones (
    registro_sesion_id INT IDENTITY (1, 1) PRIMARY KEY,
    usuario_id INT NOT NULL,
    fecha_inicio DATETIME NOT NULL DEFAULT GETDATE(),
    direccion_ip NVARCHAR (45) NOT NULL,
    dispositivo NVARCHAR (255) NULL,
    navegador NVARCHAR (255) NULL,
    resultado_sesion NVARCHAR (10) NOT NULL DEFAULT 'Exitoso',
    mensaje_error NVARCHAR (500) NULL,
    FOREIGN KEY (usuario_id) REFERENCES Usuarios (usuario_id),
    CHECK (resultado_sesion IN ('Exitoso', 'Fallido'))
);
GO

-- Tabla UnidadesResponsables: Contiene las unidades responsables dentro de la organización.
CREATE TABLE UnidadesResponsables (
    unidad_resp_id INT IDENTITY (1, 1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla ObjetivosEstrategicosInstitucionales: Almacena los objetivos estratégicos de la institución.
CREATE TABLE ObjetivosEstrategicosInstitucionales (
    obj_estr_id INT IDENTITY (1, 1) PRIMARY KEY,
    nombre NVARCHAR (500) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla UnidadesEjecutoras: Define las unidades encargadas de la ejecución de actividades.
CREATE TABLE UnidadesEjecutoras (
    unidad_ejecutora_id INT IDENTITY (1, 1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla ProgramasPresupuestarios: Gestiona los programas presupuestarios asignados.
CREATE TABLE ProgramasPresupuestarios (
    programa_pre_id INT IDENTITY (1, 1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla PlanificacionesAnuales: Contiene las planificaciones anuales del sistema.
CREATE TABLE PlanificacionesAnuales (
    planificacion_id INT IDENTITY (1, 1) PRIMARY KEY,
    anio INT NOT NULL CHECK (anio >= 2000 AND anio <= 2100),
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla Indicadores: Define los indicadores utilizados para medir el desempeño.
CREATE TABLE Indicadores (
    indicador_id INT IDENTITY (1, 1) PRIMARY KEY,
    nombre_indicador NVARCHAR (250) NOT NULL,
    metodo_calculo NVARCHAR (500) NOT NULL,
    meta_indicador NVARCHAR (100) NOT NULL,
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla Carreras: Catalogo de las diferentes carreras disponibles.
CREATE TABLE Carreras (
    carrera_id INT IDENTITY (1, 1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla Facultades: Catalogo de las diferentes facultades dentro de la institución.
CREATE TABLE Facultades (
    facultad_id INT IDENTITY (1, 1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla EjesPlanNacionalDesarrollo: Almacena los ejes del Plan Nacional de Desarrollo.
CREATE TABLE EjesPlanNacionalDesarrollo (
    eje_pn_id INT IDENTITY (1,1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla ObjetivosPlanNacionalDesarrollo: Define los objetivos del Plan Nacional de Desarrollo.
CREATE TABLE ObjetivosPlanNacionalDesarrollo (
    obj_pn_id INT IDENTITY (1,1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla PoliticasPlanNacionalDesarrollo: Contiene las políticas del Plan Nacional de Desarrollo.
CREATE TABLE PoliticasPlanNacionalDesarrollo (
    politica_pn_id INT IDENTITY (1,1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla MetasPlanNacionalDesarrollo: Registra las metas establecidas en el Plan Nacional de Desarrollo.
CREATE TABLE MetasPlanNacionalDesarrollo (
    meta_pn_id INT IDENTITY (1,1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla ProgramasNacionales: Almacena los programas nacionales relacionados con el desarrollo.
CREATE TABLE ProgramasNacionales (
    programa_nac_id INT IDENTITY (1,1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla ProgramasInstitucionales: Define los programas institucionales específicos de la organización.
CREATE TABLE ProgramasInstitucionales (
    programa_inst_id INT IDENTITY (1,1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla ProductosInstitucionales: Registra los productos generados por la institución.
CREATE TABLE ProductosInstitucionales (
    producto_inst_id INT IDENTITY (1,1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla ObjetivosOperativos: Almacena los diferentes objetivos operativos.
CREATE TABLE ObjetivosOperativos (
    objetivo_operativo_id INT IDENTITY (1, 1) PRIMARY KEY,
    nombre NVARCHAR (500) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla Acciones: Define las acciones específicas que se llevarán a cabo en las actividades.
CREATE TABLE Acciones (
    accion_id INT IDENTITY (1,1) PRIMARY KEY,
    nombre NVARCHAR (250) NOT NULL,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL
);
GO

-- Tabla Actividades: Registra las actividades planificadas, vinculando múltiples entidades relacionadas.
CREATE TABLE Actividades (
    actividad_id INT IDENTITY (1, 1) PRIMARY KEY,
    planificacion_id INT NULL, 
    unidad_resp_id INT NULL,
    obj_estr_id INT NULL,
    unidad_ejecutora_id INT NULL, 
    objetivo_operativo_id INT NULL,
    descripcion NVARCHAR (500) NOT NULL,
    carrera_id INT NULL, 
    facultad_id INT NULL, 
    eje_pn_id INT NULL,
    obj_pn_id INT NULL,
    politica_pn_id INT NULL,
    meta_pn_id INT NULL,
    programa_nac_id INT NULL,
    programa_inst_id INT NULL,
    producto_inst_id INT NULL,
    impacto NVARCHAR (50) NULL,
    riesgo NVARCHAR (250) NULL,
    accion_id INT NULL,
    indicador_id INT NULL,
    programa_pre_id INT NULL,
    item_presupuestario NVARCHAR (50),
    fuente1_monto DECIMAL(18, 2) NOT NULL DEFAULT 0,
    fuente2_monto DECIMAL(18, 2) NOT NULL DEFAULT 0,
    fuente3_monto DECIMAL(18, 2) NOT NULL DEFAULT 0,
    recursos_actividad DECIMAL(18, 2) NOT NULL,
    recursos_restantes DECIMAL(18, 2) NOT NULL,
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL,
    FOREIGN KEY (planificacion_id) REFERENCES PlanificacionesAnuales (planificacion_id),
    FOREIGN KEY (unidad_resp_id) REFERENCES UnidadesResponsables (unidad_resp_id),
    FOREIGN KEY (obj_estr_id) REFERENCES ObjetivosEstrategicosInstitucionales (obj_estr_id),
    FOREIGN KEY (unidad_ejecutora_id) REFERENCES UnidadesEjecutoras (unidad_ejecutora_id),
    FOREIGN KEY (carrera_id) REFERENCES Carreras (carrera_id),
    FOREIGN KEY (facultad_id) REFERENCES Facultades (facultad_id),
    FOREIGN KEY (indicador_id) REFERENCES Indicadores (indicador_id),
    FOREIGN KEY (programa_pre_id) REFERENCES ProgramasPresupuestarios (programa_pre_id),
    FOREIGN KEY (eje_pn_id) REFERENCES EjesPlanNacionalDesarrollo (eje_pn_id),
    FOREIGN KEY (obj_pn_id) REFERENCES ObjetivosPlanNacionalDesarrollo (obj_pn_id),
    FOREIGN KEY (politica_pn_id) REFERENCES PoliticasPlanNacionalDesarrollo (politica_pn_id),
    FOREIGN KEY (meta_pn_id) REFERENCES MetasPlanNacionalDesarrollo (meta_pn_id),
    FOREIGN KEY (programa_nac_id) REFERENCES ProgramasNacionales (programa_nac_id),
    FOREIGN KEY (programa_inst_id) REFERENCES ProgramasInstitucionales (programa_inst_id),
    FOREIGN KEY (producto_inst_id) REFERENCES ProductosInstitucionales (producto_inst_id),
    FOREIGN KEY (accion_id) REFERENCES Acciones (accion_id),
    FOREIGN KEY (objetivo_operativo_id) REFERENCES ObjetivosOperativos (objetivo_operativo_id)
);
GO

-- Tabla ObrasTareas: Registra las obras o tareas asociadas a cada actividad.
CREATE TABLE ObrasTareas (
    obra_tarea_id INT IDENTITY (1, 1) PRIMARY KEY,
    actividad_id INT NOT NULL,
    obra_tarea NVARCHAR (500) NOT NULL,
    fecha_desde DATE NOT NULL,
    fecha_hasta DATE NOT NULL,
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL,
    FOREIGN KEY (actividad_id) REFERENCES Actividades (actividad_id)
);
GO

-- Tabla EjecucionesMensuales: Controla la ejecución mensual de las obras o tareas.
CREATE TABLE EjecucionesMensuales (
    ejecucion_id INT IDENTITY (1, 1) PRIMARY KEY,
    obra_tarea_id INT NOT NULL,
    mes TINYINT NOT NULL CHECK (mes >= 1 AND mes <= 12),
    monto DECIMAL(18, 2) NOT NULL DEFAULT 0,
    porcentaje_ejecucion DECIMAL(5, 2) NOT NULL DEFAULT 0,
    descripcion NVARCHAR (500),
    estado CHAR(1) NULL DEFAULT 'A',
    creado_por INT NULL,
    fecha_creacion DATETIME NULL DEFAULT GETDATE(),
    modificado_por INT NULL,
    fecha_modificacion DATETIME NULL,
    FOREIGN KEY (obra_tarea_id) REFERENCES ObrasTareas (obra_tarea_id)
);
GO
