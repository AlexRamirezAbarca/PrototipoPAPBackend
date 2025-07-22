/*Script de tablas auxiliares*/

CREATE TABLE EjesObjetivosPN (
    eje_objetivo_pn_id INT IDENTITY(1,1) PRIMARY KEY,
    eje_pn_id INT NOT NULL,
    obj_pn_id INT NOT NULL,
    estado VARCHAR(1) NOT NULL DEFAULT 'A',
    fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),
    fecha_modificacion DATETIME NULL,
    FOREIGN KEY (eje_pn_id) REFERENCES EjesPlanNacionalDesarrollo(eje_pn_id) ON DELETE CASCADE,
    FOREIGN KEY (obj_pn_id) REFERENCES ObjetivosPlanNacionalDesarrollo(obj_pn_id) ON DELETE CASCADE
);



CREATE TABLE ObjetivosPoliticasPN (
    objetivo_politica_pn_id INT IDENTITY(1,1) PRIMARY KEY,
    obj_pn_id INT NOT NULL,
    politica_pn_id INT NOT NULL,
    estado VARCHAR(1) NOT NULL DEFAULT 'A',
    fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),
    fecha_modificacion DATETIME NULL,

    CONSTRAINT FK_Objetivo FOREIGN KEY (obj_pn_id) 
        REFERENCES ObjetivosPlanNacionalDesarrollo(obj_pn_id)
        ON DELETE CASCADE,

    CONSTRAINT FK_Politica FOREIGN KEY (politica_pn_id) 
        REFERENCES PoliticasPlanNacionalDesarrollo(politica_pn_id)
        ON DELETE CASCADE
);

CREATE TABLE objetivos_metas_pn (
    objetivo_meta_pn_id INT IDENTITY(1,1) PRIMARY KEY,
    obj_pn_id INT NOT NULL,
    meta_pn_id INT NOT NULL,
    estado VARCHAR(1) NOT NULL DEFAULT 'A',
    fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),
    fecha_modificacion DATETIME NULL,
    FOREIGN KEY (obj_pn_id) REFERENCES ObjetivosPlanNacionalDesarrollo(obj_pn_id) ON DELETE CASCADE,
    FOREIGN KEY (meta_pn_id) REFERENCES MetasPlanNacionalDesarrollo(meta_pn_id) ON DELETE CASCADE
);


CREATE TABLE ProgramasInstitucionalesPresupuestarios (
    prog_inst_pre_id INT IDENTITY(1,1) PRIMARY KEY,
    programa_inst_id INT NOT NULL,
    programa_pre_id INT NOT NULL,
    
    -- Auditoría
    estado CHAR(1) NOT NULL DEFAULT 'A',
    creado_por VARCHAR(100) NULL,
    fecha_creacion DATETIME NOT NULL DEFAULT GETDATE(),
    modificado_por VARCHAR(100) NULL,
    fecha_modificacion DATETIME NULL,

    -- Restricción de duplicados
    CONSTRAINT UQ_ProgramasInstPre UNIQUE (programa_inst_id, programa_pre_id),

    -- Foreign Keys
    CONSTRAINT FK_ProgramasInst FOREIGN KEY (programa_inst_id)
        REFERENCES ProgramasInstitucionales(programa_inst_id),
    
    CONSTRAINT FK_ProgramasPre FOREIGN KEY (programa_pre_id)
        REFERENCES ProgramasPresupuestarios(programa_pre_id)
);

CREATE TABLE ProgramasPresupuestariosProductos (
    prog_pre_prod_id INT IDENTITY(1,1) PRIMARY KEY,
    programa_pre_id INT NOT NULL,
    producto_inst_id INT NOT NULL,
    estado CHAR(1) DEFAULT 'A',
    creado_por NVARCHAR(100),
    fecha_creacion DATETIME DEFAULT GETDATE(),
    modificado_por NVARCHAR(100),
    fecha_modificacion DATETIME,

    CONSTRAINT FK_ProgramaPresupuestario FOREIGN KEY (programa_pre_id) REFERENCES ProgramasPresupuestarios(programa_pre_id),
    CONSTRAINT FK_ProductoInstitucional FOREIGN KEY (producto_inst_id) REFERENCES ProductosInstitucionales(producto_inst_id),

    CONSTRAINT UQ_ProgramaProducto UNIQUE (programa_pre_id, producto_inst_id)
);
