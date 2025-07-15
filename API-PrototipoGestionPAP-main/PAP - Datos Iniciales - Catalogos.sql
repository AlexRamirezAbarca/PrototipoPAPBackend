-- Ejes del Plan Nacional de Desarrollo (con descripción duplicada)
INSERT INTO EjesPlanNacionalDesarrollo (nombre, descripcion) 
VALUES 
('Eje Social', 'Eje Social'),
('Eje Institucional', 'Eje Institucional'),
('Eje Institucional', 'Eje Institucional');

-- Objetivos del Plan Nacional de Desarrollo
INSERT INTO ObjetivosPlanNacionalDesarrollo (nombre, descripcion) 
VALUES 
('7. Potenciar las capacidades de la ciudadanía y promover una educación innovadora, inclusiva y de calidad en todos los niveles.', 
 '7. Potenciar las capacidades de la ciudadanía y promover una educación innovadora, inclusiva y de calidad en todos los niveles.'),
 
('14. Fortalecer las capacidades del Estado con énfasis en la administración de justicia y eficiencia en los procesos de regulación y control con independencia y autonomía.', 
 '14. Fortalecer las capacidades del Estado con énfasis en la administración de justicia y eficiencia en los procesos de regulación y control con independencia y autonomía.');

-- Políticas del Plan Nacional de Desarrollo
INSERT INTO PoliticasPlanNacionalDesarrollo (nombre, descripcion) 
VALUES 
('7.4. Fortalecer el Sistema de Educación Superior bajo los principios de libertad, autonomía responsable, igualdad de oportunidades, calidad y pertinencia; promoviendo la investigación de alto impacto.', 
 '7.4. Fortalecer el Sistema de Educación Superior bajo los principios de libertad, autonomía responsable, igualdad de oportunidades, calidad y pertinencia; promoviendo la investigación de alto impacto.'),
 
('"7.3. Erradicar toda forma de discriminación, negligencia y violencia en todos los niveles del ámbito educativo, con énfasis en la violencia sexual contra la niñez y adolescencia."', 
 '"7.3. Erradicar toda forma de discriminación, negligencia y violencia en todos los niveles del ámbito educativo, con énfasis en la violencia sexual contra la niñez y adolescencia."'),
 
('14.3. Fortalecer la implementación de las buenas prácticas regulatorias que garanticen la transparencia, eficiencia y competitividad del Estado.', 
 '14.3. Fortalecer la implementación de las buenas prácticas regulatorias que garanticen la transparencia, eficiencia y competitividad del Estado.');

-- Metas del Plan Nacional de Desarrollo
INSERT INTO MetasPlanNacionalDesarrollo (nombre, descripcion) 
VALUES 
('7.4.3 Disminuir la tasa de deserción en el primer año en la educación superior del 21,84% al 19,89%.', 
 '7.4.3 Disminuir la tasa de deserción en el primer año en la educación superior del 21,84% al 19,89%.'),
 
('7.3.1 Incrementar el porcentaje de respuesta a la atención de víctimas de violencia para que cuenten con un plan de acompañamiento pasando de 67,60% a 95,00%', 
 '7.3.1 Incrementar el porcentaje de respuesta a la atención de víctimas de violencia para que cuenten con un plan de acompañamiento pasando de 67,60% a 95,00%'),
 
('7.4.1 Incrementar los artículos publicados por las universidades y escuelas politécnicas en revistas indexadas de 6.624 a 12.423.', 
 '7.4.1 Incrementar los artículos publicados por las universidades y escuelas politécnicas en revistas indexadas de 6.624 a 12.423.'),
 
('7.4.4 Incrementar el número de investigadores por cada 1.000 habitantes de la Población Económicamente Activa de 0,55 a 0,75.', 
 '7.4.4 Incrementar el número de investigadores por cada 1.000 habitantes de la Población Económicamente Activa de 0,55 a 0,75.'),
 
('7.4.2 Incrementar la tasa bruta de matrícula en educación superior terciaria del 37,34% al 50,27%.', 
 '7.4.2 Incrementar la tasa bruta de matrícula en educación superior terciaria del 37,34% al 50,27%.'),
 
('14.3.2 Aumentar el índice de percepción de calidad de los servicios públicos de 6,10 a 8,00.', 
 '14.3.2 Aumentar el índice de percepción de calidad de los servicios públicos de 6,10 a 8,00.');

-- Programas Nacionales
INSERT INTO ProgramasNacionales (nombre, descripcion) 
VALUES 
('Educación de Calidad', 'Educación de Calidad'),
('Paz, Justicia e Instituciones Sólidas', 'Paz, Justicia e Instituciones Sólidas'),
('Educación de Calidad', 'Educación de Calidad');

/******************** PROGRAMAS INSTITUCIONALES ********************/
INSERT INTO ProgramasInstitucionales (nombre, descripcion)
VALUES 
('Programa 84: Gestión de la Vinculación con la Colectividad', 'Programa 84: Gestión de la Vinculación con la Colectividad'),
('Programa 83: Gestión de la Investigación', 'Programa 83: Gestión de la Investigación'),
('Programa 82: Formación y Gestión Académica', 'Programa 82: Formación y Gestión Académica'),
('Programa 01: Administración Central', 'Programa 01: Administración Central');

/******************** PRODUCTOS INSTITUCIONALES ********************/
INSERT INTO ProductosInstitucionales (nombre, descripcion)
VALUES 
('Inclusión de Estudiantes y Graduados', 'Inclusión de Estudiantes y Graduados'),
('Proyectos de Vinculación con la Colectividad', 'Proyectos de Vinculación con la Colectividad'),
('Investigaciones', 'Investigaciones'),
('Oferta Académica', 'Oferta Académica'),
('Fortalecimiento Institucional', 'Fortalecimiento Institucional'),
('Atención Integral', 'Atención Integral');

/******************** UNIDADES RESPONSABLES ********************/
INSERT INTO UnidadesResponsables (nombre, descripcion)
VALUES 
('Vicerrectorado Académico', 'Vicerrectorado Académico'),
('Gerencia Administrativa', 'Gerencia Administrativa'),
('Rectorado', 'Rectorado');

/******************** OBJETIVOS ESTRATÉGICOS INSTITUCIONALES ********************/
INSERT INTO ObjetivosEstrategicosInstitucionales (nombre, descripcion)
VALUES 
('OEI 4. Mejorar los servicios que oferta Bienestar Estudiantil y sus procesos durante la carrera del estudiante, mediante la potencialización y automatización de los procesos competentes.', 'OEI 4. Mejorar los servicios que oferta Bienestar Estudiantil y sus procesos durante la carrera del estudiante, mediante la potencialización y automatización de los procesos competentes.'),
('OEI 3. Generar transferencia de conocimiento mediante la vinculación con pertinencia, calidad e impacto de los procesos sustantivos, en aras del desarrollo regional y los alcances de la Universidad de Guayaquil.', 'OEI 3. Generar transferencia de conocimiento mediante la vinculación con pertinencia, calidad e impacto de los procesos sustantivos, en aras del desarrollo regional y los alcances de la Universidad de Guayaquil.'),
('OEI 2. Consolidar un ecosistema de ciencia, tecnología, innovación y emprendimiento como eje transversal de los campos de conocimiento que garantice la generación, protección y transferencia de la producción científica institucional, contribuyendo al bienestar y desarrollo de la sociedad.', 'OEI 2. Consolidar un ecosistema de ciencia, tecnología, innovación y emprendimiento como eje transversal de los campos de conocimiento que garantice la generación, protección y transferencia de la producción científica institucional, contribuyendo al bienestar y desarrollo de la sociedad.'),
('OEI 1. Fortalecer el sistema educativo de la Universidad de Guayaquil a través de la óptima articulación de sus áreas: Admisión y Nivelación, Formación Académica y Gestión de Personal Académico.', 'OEI 1. Fortalecer el sistema educativo de la Universidad de Guayaquil a través de la óptima articulación de sus áreas: Admisión y Nivelación, Formación Académica y Gestión de Personal Académico.'),
('OEI 5. Fortalecer las capacidades institucionales.', 'OEI 5. Fortalecer las capacidades institucionales.'),
('OEI 5. Fortalecer las Capacidades Institucionales.', 'OEI 5. Fortalecer las Capacidades Institucionales.');

/******************** UNIDADES EJECUTORAS ********************/
INSERT INTO UnidadesEjecutoras (nombre, descripcion)
VALUES 
('Decanato de Vinculación con la Sociedad y Bienestar Estudiantil', 'Decanato de Vinculación con la Sociedad y Bienestar Estudiantil'),
('Decanato de Investigación, Posgrado e Internacionalización', 'Decanato de Investigación, Posgrado e Internacionalización'),
('Decanato de Formación Académica y Profesional', 'Decanato de Formación Académica y Profesional'),
('Dirección de Talento Humano', 'Dirección de Talento Humano'),
('Dirección de Gestión Tecnológica de la Información', 'Dirección de Gestión Tecnológica de la Información'),
('Dirección de Infraestructura y Obras Universitarias', 'Dirección de Infraestructura y Obras Universitarias'),
('Coordinación de Comunicación y Difusión de la Información', 'Coordinación de Comunicación y Difusión de la Información'),
('Dirección Financiera', 'Dirección Financiera'),
('Coordinación de Innovación y Emprendimiento', 'Coordinación de Innovación y Emprendimiento'),
('Dirección de Compras Públicas', 'Dirección de Compras Públicas'),
('Secretaría General', 'Secretaría General'),
('Procuraduría Síndica', 'Procuraduría Síndica'),
('Dirección de Administrativa', 'Dirección de Administrativa'),
('Gerencia Administrativa', 'Gerencia Administrativa'),
('Coordinación de Planificación, Acreditación y Evaluación Institucional', 'Coordinación de Planificación, Acreditación y Evaluación Institucional'),
('Dirección de Acreditación de Carreras y Programas', 'Dirección de Acreditación de Carreras y Programas'),
('Facultad de Filosofía, Letras y Ciencias de la Educación', 'Facultad de Filosofía, Letras y Ciencias de la Educación'),
('Facultad de Jurisprudencia y Ciencias Políticas', 'Facultad de Jurisprudencia y Ciencias Políticas'),
('Facultad Piloto de Odontolgía', 'Facultad Piloto de Odontolgía'),
('Facultad de Ciencias Económicas', 'Facultad de Ciencias Económicas'),
('Facultad de Arquitectura y Urbanismo', 'Facultad de Arquitectura y Urbanismo'),
('Facultad de Ciencias Administrativas', 'Facultad de Ciencias Administrativas'),
('Facultad de Educación Física, Deportes y Recreación', 'Facultad de Educación Física, Deportes y Recreación'),
('Facultad de Medicina Veterinaria y Zootecnia', 'Facultad de Medicina Veterinaria y Zootecnia'),
('Facultad de Ingeniería Química', 'Facultad de Ingeniería Química'),
('Facultad de Ciencias Psicológicas', 'Facultad de Ciencias Psicológicas'),
('Facultad de Ciencias Naturales', 'Facultad de Ciencias Naturales'),
('Facultad de Ciencias Médicas', 'Facultad de Ciencias Médicas'),
('Facultad de Ciencias Matemáticas y Físicas', 'Facultad de Ciencias Matemáticas y Físicas'),
('Facultad de Ingeniería Industrial', 'Facultad de Ingeniería Industrial'),
('Facultad de Posgrado "Dr. Antonio Parra Velasco"', 'Facultad de Posgrado "Dr. Antonio Parra Velasco"'),
('Facultad de Comunicación Social', 'Facultad de Comunicación Social'),
('Facultad de Ciencias Agrarias', 'Facultad de Ciencias Agrarias'),
('Facultad de Ciencias Químicas', 'Facultad de Ciencias Químicas');

/******************** OBJETIVOS OPERATIVOS ********************/
INSERT INTO ObjetivosOperativos (nombre, descripcion) 
VALUES 
('Fortalecer los servicios integrales y académicos para el desarrollo y formación a los estudiantes y graduados de la Universidad de Guayaquil.', 'Fortalecer los servicios integrales y académicos para el desarrollo y formación a los estudiantes y graduados de la Universidad de Guayaquil.'),
('Desarrollar programas y proyectos de vinculación con la sociedad alineados a los objetivos globales, nacionales e institucionales que contribuyan al mejoramiento de la calidad de vida de las personas.', 'Desarrollar programas y proyectos de vinculación con la sociedad alineados a los objetivos globales, nacionales e institucionales que contribuyan al mejoramiento de la calidad de vida de las personas.'),
('Articular estructuras que propicien el fortalecimiento del ecosistema de ciencia, tecnología, innovación y emprendimiento a través de la ejecución de los procesos y subprocesos de cultura científica, generación, protección, transferencia y gestión del conocimiento, contemplados en la función sustantiva de investigación desde los grupos de investigación, centros de excelencia y proyectos de investigación, desarrollo, innovación y emprendimiento.', 'Articular estructuras que propicien el fortalecimiento del ecosistema de ciencia, tecnología, innovación y emprendimiento a través de la ejecución de los procesos y subprocesos de cultura científica, generación, protección, transferencia y gestión del conocimiento, contemplados en la función sustantiva de investigación desde los grupos de investigación, centros de excelencia y proyectos de investigación, desarrollo, innovación y emprendimiento.'),
('Fortalecer una oferta formativa de los programas de posgrado y educación continúa vinculada al conocimiento generado por el ecosistema de ciencia, tecnología, innovación y emprendimiento que responda a la demanda social de la Universidad.', 'Fortalecer una oferta formativa de los programas de posgrado y educación continúa vinculada al conocimiento generado por el ecosistema de ciencia, tecnología, innovación y emprendimiento que responda a la demanda social de la Universidad.'),
('Dirección Administrativa: Incrementar la efectividad de la gestión institucional a través de la elaboración y/o aplicación de los procedimientos y normativas institucionales.', 'Dirección Administrativa: Incrementar la efectividad de la gestión institucional a través de la elaboración y/o aplicación de los procedimientos y normativas institucionales.'),
('"Incrementar la calidad del sistema formativo e inclusivo de docentes para que propicien el desarrollo de la Universidad de Guayaquil"', '"Incrementar la calidad del sistema formativo e inclusivo de docentes para que propicien el desarrollo de la Universidad de Guayaquil"'),
('Fortalecer las Capacidades Institucionales.', 'Fortalecer las Capacidades Institucionales.');

/******************** ACCIONES ********************/
INSERT INTO Acciones (nombre, descripcion) 
VALUES 
('Ofertar servicios de salud, consejería académica y protección social, a la comunidad estudiantil garantizando igualdad e inclusión.', 'Ofertar servicios de salud, consejería académica y protección social, a la comunidad estudiantil garantizando igualdad e inclusión.'),
('Contribuir al desarrollo de Programas de Posgrados con pertinencia, en relación a las áreas, campos del conocimiento y líneas de investigación, y la ejecución de programas de educación continua.', 'Contribuir al desarrollo de Programas de Posgrados con pertinencia, en relación a las áreas, campos del conocimiento y líneas de investigación, y la ejecución de programas de educación continua.'),
('Fomentar la eficacia en la gestión de los procesos tecnológicos.', 'Fomentar la eficacia en la gestión de los procesos tecnológicos.'),
('"Incentivar la integración de la comunidad de esudiantes y graduados a través de eventos académicos, culturales, laborales y deportivos."', '"Incentivar la integración de la comunidad de esudiantes y graduados a través de eventos académicos, culturales, laborales y deportivos."'),
('Fortalecer la informaciòn digital aexistente en los archivos de la facultad', 'Fortalecer la informaciòn digital aexistente en los archivos de la facultad'),
('Gestionar la adquisición de mobiliario para las areas administrativas', 'Gestionar la adquisición de mobiliario para las areas administrativas');

INSERT INTO Facultades (nombre, descripcion) 
VALUES 
('Facultad de Arquitectura y Urbanismo', 'Facultad de Arquitectura y Urbanismo'),
('Facultad de Ciencias Administrativas', 'Facultad de Ciencias Administrativas'),
('Facultad de Ciencias Agrarias', 'Facultad de Ciencias Agrarias'),
('Facultad de Ciencias Económicas', 'Facultad de Ciencias Económicas'),
('Facultad de Ciencias Matemáticas y Físicas', 'Facultad de Ciencias Matemáticas y Físicas'),
('Facultad de Ciencias Médicas', 'Facultad de Ciencias Médicas'),
('Facultad de Ciencias Psicológicas', 'Facultad de Ciencias Psicológicas'),
('Facultad de Ciencias Químicas', 'Facultad de Ciencias Químicas'),
('Facultad de Comunicación Social', 'Facultad de Comunicación Social'),
('Facultad de Educación Física, Deportes y Recreación', 'Facultad de Educación Física, Deportes y Recreación'),
('Facultad de Filosofía, Letras y Ciencias de la Educación', 'Facultad de Filosofía, Letras y Ciencias de la Educación'),
('Facultad de Ingeniería Industrial', 'Facultad de Ingeniería Industrial'),
('Facultad de Ingeniería Química', 'Facultad de Ingeniería Química'),
('Facultad de Jurisprudencia y Ciencias Sociales y Políticas', 'Facultad de Jurisprudencia y Ciencias Sociales y Políticas'),
('Facultad de Medicina Veterinaria y Zootecnia', 'Facultad de Medicina Veterinaria y Zootecnia'),
('Facultad Piloto de Odontología', 'Facultad Piloto de Odontología'),
('Facultad de Ciencias Naturales', 'Facultad de Ciencias Naturales');

INSERT INTO Carreras (nombre, descripcion) 
VALUES 
('Alimentos', 'Alimentos'),
('Arquitectura', 'Arquitectura'),
('Biología', 'Biología'),
('Bioquímica Farmacéutica', 'Bioquímica Farmacéutica'),
('Ciencia de Datos e Inteligencia Artificial', 'Ciencia de Datos e Inteligencia Artificial'),
('Geología', 'Geología'),
('Ingeniería Ambiental', 'Ingeniería Ambiental'),
('Ingeniería Civil', 'Ingeniería Civil'),
('Ingeniería Industrial', 'Ingeniería Industrial'),
('Ingeniería de la Producción', 'Ingeniería de la Producción'),
('Ingeniería Química', 'Ingeniería Química'),
('Sistemas de Información', 'Sistemas de Información'),
('Software', 'Software'),
('Tecnologías de la Información', 'Tecnologías de la Información'),
('Telemática', 'Telemática'),
('Diseño Gráfico', 'Diseño Gráfico'),
('Diseño de Interiores', 'Diseño de Interiores'),
('Agronomía', 'Agronomía'),
('Agropecuaria', 'Agropecuaria'),
('Medicina Veterinaria', 'Medicina Veterinaria'),
('Ciencias Políticas', 'Ciencias Políticas'),
('Comunicación', 'Comunicación'),
('Derecho', 'Derecho'),
('Educación Básica', 'Educación Básica'),
('Educación Inicial', 'Educación Inicial'),
('Entrenamiento Deportivo', 'Entrenamiento Deportivo'),
('Gastronomía', 'Gastronomía'),
('Pedagogía de la Informática', 'Pedagogía de la Informática'),
('Pedagogía de los Idiomas Nacionales y Extranjeros', 'Pedagogía de los Idiomas Nacionales y Extranjeros'),
('Pedagogía de la Lengua y Literatura', 'Pedagogía de la Lengua y Literatura'),
('Pedagogía de la Actividad Física y Deporte', 'Pedagogía de la Actividad Física y Deporte'),
('Pedagogía de la Química y Biología', 'Pedagogía de la Química y Biología'),
('Pedagogía de la Historia y Ciencias Sociales', 'Pedagogía de la Historia y Ciencias Sociales'),
('Pedagogía de las Artes y las Humanidades', 'Pedagogía de las Artes y las Humanidades'),
('Pedagogía de las Matemáticas y la Física', 'Pedagogía de las Matemáticas y la Física'),
('Psicología', 'Psicología'),
('Publicidad', 'Publicidad'),
('Sociología', 'Sociología'),
('Administración de Empresas', 'Administración de Empresas'),
('Comercio Exterior', 'Comercio Exterior'),
('Contabilidad y Auditoría', 'Contabilidad y Auditoría'),
('Economía', 'Economía'),
('Economía Internacional', 'Economía Internacional'),
('Finanzas', 'Finanzas'),
('Gestión de la Información Gerencial', 'Gestión de la Información Gerencial'),
('Mercadotecnia', 'Mercadotecnia'),
('Negocios Internacionales', 'Negocios Internacionales'),
('Turismo', 'Turismo'),
('Enfermería', 'Enfermería'),
('Fonoaudiología', 'Fonoaudiología'),
('Medicina', 'Medicina'),
('Nutrición y Dietética', 'Nutrición y Dietética'),
('Obstetricia', 'Obstetricia'),
('Odontología', 'Odontología'),
('Terapia Ocupacional', 'Terapia Ocupacional'),
('Terapia Respiratoria', 'Terapia Respiratoria');


INSERT INTO PlanificacionesAnuales (anio, descripcion)
VALUES (2023, N'Planificación Anual UG 2023');
INSERT INTO PlanificacionesAnuales (anio, descripcion)
VALUES (2024, N'Planificación Anual UG 2024');
INSERT INTO PlanificacionesAnuales (anio, descripcion)
VALUES (2025, N'Planificación Anual UG 2025');
INSERT INTO PlanificacionesAnuales (anio, descripcion)
VALUES (2026, N'Planificación Anual UG 2026');


INSERT INTO Indicadores (nombre_indicador, metodo_calculo, meta_indicador)
VALUES 
(N'Porcentaje de ejecución Presupuestaria.', 
 N'Monto de Presupuesto Ejecutado / Monto de Presupuesto Codificado.', 
 N'100%'),
(N'Porcentaje de Estudiantes con NEE Identificados', 
 N'Número de Estudiantes con NEE Identificados / Número de Estudiantes con NEE Matriculados', 
 N'100%');

 -- Inserts para ProgramasPresupuestarios
INSERT INTO ProgramasPresupuestarios (nombre, descripcion)
VALUES ('Programa 01: Administración Central', 'Programa presupuestario para la administración central.');
INSERT INTO ProgramasPresupuestarios (nombre, descripcion)
VALUES ('Programa 82: Formación y Gestión Académica', 'Programa presupuestario para la formación y gestión académica.');
INSERT INTO ProgramasPresupuestarios (nombre, descripcion)
VALUES ('Programa 83: Gestión de la Investigación', 'Programa presupuestario para la gestión de la investigación.');
INSERT INTO ProgramasPresupuestarios (nombre, descripcion)
VALUES ('Programa 84: Gestión de la Vinculación con la Colectividad', 'Programa presupuestario para la vinculación con la sociedad.');
