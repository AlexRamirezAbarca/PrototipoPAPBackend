/*
    ADVERTENCIA: Este script inserta datos ficticios de prueba en las entidades principales
    de la PAP (Actividades, Obra, Tareas, Ejecuciones, Indicadores, etc.). Solo ejecute este
    script en entornos de prueba o cuando sea necesario poblar las entidades principales con datos
    de ejemplo. ¡NO utilizar en producción!
*/
-- Obtener ID de la planificación 2024
DECLARE @planificacionId INT = 2;

-------------------------------------------
-- ACTIVIDAD 4 (Nueva)
-------------------------------------------
DECLARE @actividad4ID INT;
INSERT INTO Actividades (
    planificacion_id, unidad_resp_id, obj_estr_id, unidad_ejecutora_id,
    objetivo_operativo_id, descripcion, carrera_id, facultad_id,
    eje_pn_id, obj_pn_id, politica_pn_id, meta_pn_id, programa_nac_id,
    programa_inst_id, producto_inst_id, impacto, riesgo, accion_id,
    indicador_id, programa_pre_id, item_presupuestario,
    fuente1_monto, fuente2_monto, fuente3_monto, 
    recursos_actividad, recursos_restantes  -- Campo requerido añadido
) VALUES (
    @planificacionId,                
    1,                               
    1,                               
    5,                               
    1,                               
    'Sistema de gestión para estudiantes con NEE',
    12,                              
    11,                              
    1,                               
    1,                               
    2,                               
    2,                               
    1,                               
    1,                               
    6,                               
    'Alto',                          
    'Falta de adopción tecnológica', 
    3,                               
    2,                               
    1,                               
    '004',                           
    80000.00,                        
    50000.00,                        
    20000.00,                        
    150000.00,                       
    150000.00  -- Valor inicial = recursos_actividad (total)
);
SET @actividad4ID = SCOPE_IDENTITY();

-- Obra/Tarea asociada
DECLARE @obra4ID INT;
INSERT INTO ObrasTareas (
    actividad_id, obra_tarea, fecha_desde, fecha_hasta  -- Todos campos obligatorios
) VALUES (
    @actividad4ID,  -- Asegurar que obtenga el ID generado
    'Desarrollo plataforma digital NEE',
    '2024-02-01',
    '2024-11-30'
);
SET @obra4ID = SCOPE_IDENTITY();

-- Ejecuciones mensuales (12 meses)
INSERT INTO EjecucionesMensuales (obra_tarea_id, mes, monto, porcentaje_ejecucion)
VALUES 
(@obra4ID, 1, 12500.00, 8.33),
(@obra4ID, 2, 12500.00, 8.33),
(@obra4ID, 3, 12500.00, 8.33),
(@obra4ID, 4, 12500.00, 8.33),
(@obra4ID, 5, 12500.00, 8.33),
(@obra4ID, 6, 12500.00, 8.33),
(@obra4ID, 7, 12500.00, 8.33),
(@obra4ID, 8, 12500.00, 8.33),
(@obra4ID, 9, 12500.00, 8.33),
(@obra4ID, 10, 12500.00, 8.33),
(@obra4ID, 11, 12500.00, 8.33),
(@obra4ID, 12, 12500.00, 8.33);
