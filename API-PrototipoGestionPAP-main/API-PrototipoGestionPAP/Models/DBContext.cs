using API_PrototipoGestionPAP.Models.Mantenedores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace API_PrototipoGestionPAP.Models;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acciones> Acciones { get; set; }

    public virtual DbSet<Actividades> Actividades { get; set; }

    public virtual DbSet<Carreras> Carreras { get; set; }

    public virtual DbSet<EjecucionesMensuales> EjecucionesMensuales { get; set; }

    public virtual DbSet<EjesPlanNacionalDesarrollo> EjesPlanNacionalDesarrollo { get; set; }

    public virtual DbSet<Facultades> Facultades { get; set; }

    public virtual DbSet<Indicadores> Indicadores { get; set; }

    public virtual DbSet<MetasPlanNacionalDesarrollo> MetasPlanNacionalDesarrollo { get; set; }

    public virtual DbSet<ObjetivosEstrategicosInstitucionales> ObjetivosEstrategicosInstitucionales { get; set; }

    public virtual DbSet<ObjetivosOperativos> ObjetivosOperativos { get; set; }

    public virtual DbSet<ObjetivosPlanNacionalDesarrollo> ObjetivosPlanNacionalDesarrollo { get; set; }

    public virtual DbSet<ObrasTareas> ObrasTareas { get; set; }

    public virtual DbSet<Permisos> Permisos { get; set; }

    public virtual DbSet<Personas> Personas { get; set; }

    public virtual DbSet<PlanificacionesAnuales> PlanificacionesAnuales { get; set; }

    public virtual DbSet<PoliticasPlanNacionalDesarrollo> PoliticasPlanNacionalDesarrollo { get; set; }

    public virtual DbSet<ProductosInstitucionales> ProductosInstitucionales { get; set; }

    public virtual DbSet<ProgramasInstitucionales> ProgramasInstitucionales { get; set; }

    public virtual DbSet<ProgramasNacionales> ProgramasNacionales { get; set; }

    public virtual DbSet<ProgramasPresupuestarios> ProgramasPresupuestarios { get; set; }

    public virtual DbSet<RegistroSesiones> RegistroSesiones { get; set; }

    public virtual DbSet<RolPermisos> RolPermisos { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<UnidadesEjecutoras> UnidadesEjecutoras { get; set; }

    public virtual DbSet<UnidadesResponsables> UnidadesResponsables { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    //Mantenedores
    public virtual DbSet<EjeObjetivoPN> EjesObjetivosPN { get; set; }
    public virtual DbSet<ObjetivoPoliticaPN> ObjetivosPoliticasPN { get; set; }
    public virtual DbSet<ObjetivoMetaPN> ObjetivosMetasPN { get; set; }

    //Fin Mantenedores
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acciones>(entity =>
        {
            entity.HasKey(e => e.AccionId).HasName("PK__Acciones__48B9911CE8CFCF0D");

            entity.HasIndex(e => e.Nombre, "UQ__Acciones__72AFBCC60077D3FF").IsUnique();

            entity.Property(e => e.AccionId).HasColumnName("accion_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Actividades>(entity =>
        {
            entity.HasKey(e => e.ActividadId).HasName("PK__Activida__3AC1AB444F7B4A09");

            entity.Property(e => e.ActividadId).HasColumnName("actividad_id");
            entity.Property(e => e.AccionId).HasColumnName("accion_id");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.EjePnId).HasColumnName("eje_pn_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FacultadId).HasColumnName("facultad_id");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.Fuente1Monto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("fuente1_monto");
            entity.Property(e => e.Fuente2Monto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("fuente2_monto");
            entity.Property(e => e.Fuente3Monto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("fuente3_monto");
            entity.Property(e => e.Impacto)
                .HasMaxLength(50)
                .HasColumnName("impacto");
            entity.Property(e => e.IndicadorId).HasColumnName("indicador_id");
            entity.Property(e => e.ItemPresupuestario)
                .HasMaxLength(50)
                .HasColumnName("item_presupuestario");
            entity.Property(e => e.MetaPnId).HasColumnName("meta_pn_id");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.ObjEstrId).HasColumnName("obj_estr_id");
            entity.Property(e => e.ObjPnId).HasColumnName("obj_pn_id");
            entity.Property(e => e.ObjetivoOperativoId).HasColumnName("objetivo_operativo_id");
            entity.Property(e => e.PlanificacionId).HasColumnName("planificacion_id");
            entity.Property(e => e.PoliticaPnId).HasColumnName("politica_pn_id");
            entity.Property(e => e.ProductoInstId).HasColumnName("producto_inst_id");
            entity.Property(e => e.ProgramaInstId).HasColumnName("programa_inst_id");
            entity.Property(e => e.ProgramaNacId).HasColumnName("programa_nac_id");
            entity.Property(e => e.ProgramaPreId).HasColumnName("programa_pre_id");
            entity.Property(e => e.RecursosActividad)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("recursos_actividad");
            entity.Property(e => e.RecursosRestantes)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("recursos_restantes");
            entity.Property(e => e.Riesgo)
                .HasMaxLength(250)
                .HasColumnName("riesgo");
            entity.Property(e => e.UnidadEjecutoraId).HasColumnName("unidad_ejecutora_id");
            entity.Property(e => e.UnidadRespId).HasColumnName("unidad_resp_id");

            entity.HasOne(d => d.Accion).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.AccionId)
                .HasConstraintName("FK__Actividad__accio__5E8A0973");

            entity.HasOne(d => d.Carrera).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__Actividad__carre__540C7B00");

            entity.HasOne(d => d.EjePn).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.EjePnId)
                .HasConstraintName("FK__Actividad__eje_p__57DD0BE4");

            entity.HasOne(d => d.Facultad).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.FacultadId)
                .HasConstraintName("FK__Actividad__facul__55009F39");

            entity.HasOne(d => d.Indicador).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.IndicadorId)
                .HasConstraintName("FK__Actividad__indic__55F4C372");

            entity.HasOne(d => d.MetaPn).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.MetaPnId)
                .HasConstraintName("FK__Actividad__meta___5AB9788F");

            entity.HasOne(d => d.ObjEstr).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.ObjEstrId)
                .HasConstraintName("FK__Actividad__obj_e__5224328E");

            entity.HasOne(d => d.ObjPn).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.ObjPnId)
                .HasConstraintName("FK__Actividad__obj_p__58D1301D");

            entity.HasOne(d => d.ObjetivoOperativo).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.ObjetivoOperativoId)
                .HasConstraintName("FK__Actividad__objet__5F7E2DAC");

            entity.HasOne(d => d.Planificacion).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.PlanificacionId)
                .HasConstraintName("FK__Actividad__plani__503BEA1C");

            entity.HasOne(d => d.PoliticaPn).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.PoliticaPnId)
                .HasConstraintName("FK__Actividad__polit__59C55456");

            entity.HasOne(d => d.ProductoInst).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.ProductoInstId)
                .HasConstraintName("FK__Actividad__produ__5D95E53A");

            entity.HasOne(d => d.ProgramaInst).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.ProgramaInstId)
                .HasConstraintName("FK__Actividad__progr__5CA1C101");

            entity.HasOne(d => d.ProgramaNac).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.ProgramaNacId)
                .HasConstraintName("FK__Actividad__progr__5BAD9CC8");

            entity.HasOne(d => d.ProgramaPre).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.ProgramaPreId)
                .HasConstraintName("FK__Actividad__progr__56E8E7AB");

            entity.HasOne(d => d.UnidadEjecutora).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.UnidadEjecutoraId)
                .HasConstraintName("FK__Actividad__unida__531856C7");

            entity.HasOne(d => d.UnidadResp).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.UnidadRespId)
                .HasConstraintName("FK__Actividad__unida__51300E55");
        });

        modelBuilder.Entity<Carreras>(entity =>
        {
            entity.HasKey(e => e.CarreraId).HasName("PK__Carreras__1F1EC70065DB2CF3");

            entity.HasIndex(e => e.Nombre, "UQ__Carreras__72AFBCC6728F4898").IsUnique();

            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<EjecucionesMensuales>(entity =>
        {
            entity.HasKey(e => e.EjecucionId).HasName("PK__Ejecucio__4A1D8F2B3D52E3D9");

            entity.Property(e => e.EjecucionId).HasColumnName("ejecucion_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.Mes).HasColumnName("mes");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.ObraTareaId).HasColumnName("obra_tarea_id");
            entity.Property(e => e.PorcentajeEjecucion)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("porcentaje_ejecucion");

            entity.HasOne(d => d.ObraTarea).WithMany(p => p.EjecucionesMensuales)
                .HasForeignKey(d => d.ObraTareaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ejecucion__obra___6BE40491");
        });

        modelBuilder.Entity<EjesPlanNacionalDesarrollo>(entity =>
        {
            entity.HasKey(e => e.EjePnId).HasName("PK__EjesPlan__C3E7C4D53DF14B4E");

            entity.HasIndex(e => e.Nombre, "UQ__EjesPlan__72AFBCC68A0F399D").IsUnique();

            entity.Property(e => e.EjePnId).HasColumnName("eje_pn_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Facultades>(entity =>
        {
            entity.HasKey(e => e.FacultadId).HasName("PK__Facultad__6407F1AE7823640E");

            entity.HasIndex(e => e.Nombre, "UQ__Facultad__72AFBCC63739F58A").IsUnique();

            entity.Property(e => e.FacultadId).HasColumnName("facultad_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Indicadores>(entity =>
        {
            entity.HasKey(e => e.IndicadorId).HasName("PK__Indicado__77769B2532837036");

            entity.Property(e => e.IndicadorId).HasColumnName("indicador_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.MetaIndicador)
                .HasMaxLength(100)
                .HasColumnName("meta_indicador");
            entity.Property(e => e.MetodoCalculo)
                .HasMaxLength(500)
                .HasColumnName("metodo_calculo");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.NombreIndicador)
                .HasMaxLength(250)
                .HasColumnName("nombre_indicador");
        });

        modelBuilder.Entity<MetasPlanNacionalDesarrollo>(entity =>
        {
            entity.HasKey(e => e.MetaPnId).HasName("PK__MetasPla__21C9C6DEB8B3D6A6");

            entity.HasIndex(e => e.Nombre, "UQ__MetasPla__72AFBCC64510FF9A").IsUnique();

            entity.Property(e => e.MetaPnId).HasColumnName("meta_pn_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<ObjetivosEstrategicosInstitucionales>(entity =>
        {
            entity.HasKey(e => e.ObjEstrId).HasName("PK__Objetivo__618D0EADBFE46456");

            entity.Property(e => e.ObjEstrId).HasColumnName("obj_estr_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<ObjetivosOperativos>(entity =>
        {
            entity.HasKey(e => e.ObjetivoOperativoId).HasName("PK__Objetivo__3339EFB26431AF5F");

            entity.HasIndex(e => e.Nombre, "UQ__Objetivo__72AFBCC66D39DEEA").IsUnique();

            entity.Property(e => e.ObjetivoOperativoId).HasColumnName("objetivo_operativo_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<ObjetivosPlanNacionalDesarrollo>(entity =>
        {
            entity.HasKey(e => e.ObjPnId).HasName("PK__Objetivo__8DA2CE8B246EC88A");

            entity.HasIndex(e => e.Nombre, "UQ__Objetivo__72AFBCC6415B0F81").IsUnique();

            entity.Property(e => e.ObjPnId).HasColumnName("obj_pn_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<ObrasTareas>(entity =>
        {
            entity.HasKey(e => e.ObraTareaId).HasName("PK__ObrasTar__2D27D22E0719EDCB");

            entity.Property(e => e.ObraTareaId).HasColumnName("obra_tarea_id");
            entity.Property(e => e.ActividadId).HasColumnName("actividad_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaDesde).HasColumnName("fecha_desde");
            entity.Property(e => e.FechaHasta).HasColumnName("fecha_hasta");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.ObraTarea)
                .HasMaxLength(500)
                .HasColumnName("obra_tarea");

            entity.HasOne(d => d.Actividad).WithMany(p => p.ObrasTareas)
                .HasForeignKey(d => d.ActividadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ObrasTare__activ__6442E2C9");
        });

        modelBuilder.Entity<Permisos>(entity =>
        {
            entity.HasKey(e => e.PermisoId).HasName("PK__Permisos__60B569CD98285F1D");

            entity.HasIndex(e => e.Codigo, "UQ__Permisos__40F9A2060F78F3A0").IsUnique();

            entity.Property(e => e.PermisoId).HasColumnName("permiso_id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(100)
                .HasColumnName("codigo");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
        });

        modelBuilder.Entity<Personas>(entity =>
        {
            entity.HasKey(e => e.PersonaId).HasName("PK__Personas__189F813ABA7BD748");

            entity.HasIndex(e => e.Cedula, "UQ__Personas__415B7BE5EE82CE6A").IsUnique();

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Personas__5B8A0682902A2107").IsUnique();

            entity.Property(e => e.PersonaId).HasColumnName("persona_id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .HasColumnName("cedula");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(255)
                .HasColumnName("correo_electronico");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<PlanificacionesAnuales>(entity =>
        {
            entity.HasKey(e => e.PlanificacionId).HasName("PK__Planific__9F2C7782A75B0302");

            entity.HasIndex(e => e.Anio, "UQ__Planific__61B22F46FA4BF5B5").IsUnique();

            entity.Property(e => e.PlanificacionId).HasColumnName("planificacion_id");
            entity.Property(e => e.Anio).HasColumnName("anio");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
        });

        modelBuilder.Entity<PoliticasPlanNacionalDesarrollo>(entity =>
        {
            entity.HasKey(e => e.PoliticaPnId).HasName("PK__Politica__2C74500E812E6B6E");

            entity.HasIndex(e => e.Nombre, "UQ__Politica__72AFBCC6890C554E").IsUnique();

            entity.Property(e => e.PoliticaPnId).HasColumnName("politica_pn_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<ProductosInstitucionales>(entity =>
        {
            entity.HasKey(e => e.ProductoInstId).HasName("PK__Producto__DCAF56EA7B3C92B3");

            entity.HasIndex(e => e.Nombre, "UQ__Producto__72AFBCC676992E31").IsUnique();

            entity.Property(e => e.ProductoInstId).HasColumnName("producto_inst_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<ProgramasInstitucionales>(entity =>
        {
            entity.HasKey(e => e.ProgramaInstId).HasName("PK__Programa__CE4161D2F0E3F069");

            entity.HasIndex(e => e.Nombre, "UQ__Programa__72AFBCC6D7694422").IsUnique();

            entity.Property(e => e.ProgramaInstId).HasColumnName("programa_inst_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<ProgramasNacionales>(entity =>
        {
            entity.HasKey(e => e.ProgramaNacId).HasName("PK__Programa__6CE2521E3C61B8DE");

            entity.HasIndex(e => e.Nombre, "UQ__Programa__72AFBCC649F622B0").IsUnique();

            entity.Property(e => e.ProgramaNacId).HasColumnName("programa_nac_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<ProgramasPresupuestarios>(entity =>
        {
            entity.HasKey(e => e.ProgramaPreId).HasName("PK__Programa__33DB40D277768C50");

            entity.Property(e => e.ProgramaPreId).HasColumnName("programa_pre_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<RegistroSesiones>(entity =>
        {
            entity.HasKey(e => e.RegistroSesionId).HasName("PK__Registro__ABE2E015398431EE");

            entity.Property(e => e.RegistroSesionId).HasColumnName("registro_sesion_id");
            entity.Property(e => e.DireccionIp)
                .HasMaxLength(45)
                .HasColumnName("direccion_ip");
            entity.Property(e => e.Dispositivo)
                .HasMaxLength(255)
                .HasColumnName("dispositivo");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.MensajeError)
                .HasMaxLength(500)
                .HasColumnName("mensaje_error");
            entity.Property(e => e.Navegador)
                .HasMaxLength(255)
                .HasColumnName("navegador");
            entity.Property(e => e.ResultadoSesion)
                .HasMaxLength(10)
                .HasDefaultValue("Exitoso")
                .HasColumnName("resultado_sesion");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.RegistroSesiones)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RegistroS__usuar__7A672E12");
        });

        modelBuilder.Entity<RolPermisos>(entity =>
        {
            entity.HasKey(e => e.RolPermisoId).HasName("PK__RolPermi__D26ED78C28A8967B");

            entity.Property(e => e.RolPermisoId).HasColumnName("rol_permiso_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.PermisoId).HasColumnName("permiso_id");
            entity.Property(e => e.RolId).HasColumnName("rol_id");

            entity.HasOne(d => d.Permiso).WithMany(p => p.RolPermisos)
                .HasForeignKey(d => d.PermisoId)
                .HasConstraintName("FK__RolPermis__permi__75A278F5");

            entity.HasOne(d => d.Rol).WithMany(p => p.RolPermisos)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__RolPermis__rol_i__74AE54BC");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__CF32E443E5548B2C");

            entity.HasIndex(e => e.Nombre, "UQ__Roles__72AFBCC6951B6093").IsUnique();

            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<UnidadesEjecutoras>(entity =>
        {
            entity.HasKey(e => e.UnidadEjecutoraId).HasName("PK__Unidades__2C826F0683BCE1C2");

            entity.Property(e => e.UnidadEjecutoraId).HasColumnName("unidad_ejecutora_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<UnidadesResponsables>(entity =>
        {
            entity.HasKey(e => e.UnidadRespId).HasName("PK__Unidades__3F12033C67A01B4D");

            entity.Property(e => e.UnidadRespId).HasColumnName("unidad_resp_id");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.Nombre)
                .HasMaxLength(250)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2ED7D2AFA4054007");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__D4D22D74097033DD").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .HasColumnName("contraseña");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_modificacion");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.PersonaId).HasColumnName("persona_id");
            entity.Property(e => e.RolId).HasColumnName("rol_id");

            entity.HasOne(d => d.Persona).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.PersonaId)
                .HasConstraintName("FK__Usuarios__person__6A30C649");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Usuarios__rol_id__6B24EA82");
        });

        //Mantenedores

        modelBuilder.Entity<EjeObjetivoPN>(entity =>
        {
            entity.HasKey(e => e.EjeObjetivoPnId);

            entity.ToTable("EjesObjetivosPN");

            entity.Property(e => e.EjeObjetivoPnId).HasColumnName("eje_objetivo_pn_id");
            entity.Property(e => e.EjePnId).HasColumnName("eje_pn_id");
            entity.Property(e => e.ObjPnId).HasColumnName("obj_pn_id");
            entity.Property(e => e.Estado).HasMaxLength(1).IsUnicode(false).HasDefaultValue("A").IsFixedLength().HasColumnName("estado");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("datetime").HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion").HasColumnType("datetime");

            entity.HasOne(d => d.Eje)
                .WithMany()
                .HasForeignKey(d => d.EjePnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EjesObjetivosPN_Eje");

            entity.HasOne(d => d.Objetivo)
                .WithMany()
                .HasForeignKey(d => d.ObjPnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EjesObjetivosPN_Objetivo");

            entity.HasIndex(e => new { e.EjePnId, e.ObjPnId }).IsUnique(); // para evitar duplicados
        });

        modelBuilder.Entity<ObjetivoPoliticaPN>(entity =>
        {
            entity.HasKey(e => e.ObjetivoPoliticaPnId);
            entity.ToTable("ObjetivosPoliticasPN");

            entity.Property(e => e.ObjetivoPoliticaPnId).HasColumnName("objetivo_politica_pn_id");
            entity.Property(e => e.ObjPnId).HasColumnName("obj_pn_id");
            entity.Property(e => e.PoliticaPnId).HasColumnName("politica_pn_id");
            entity.Property(e => e.Estado).HasMaxLength(1).IsUnicode(false).HasDefaultValue("A").IsFixedLength().HasColumnName("estado");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("datetime").HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion").HasColumnType("datetime");

            entity.HasOne(d => d.Objetivo)
                .WithMany()
                .HasForeignKey(d => d.ObjPnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ObjetivosPoliticasPN_Objetivo");

            entity.HasOne(d => d.Politica)
                .WithMany()
                .HasForeignKey(d => d.PoliticaPnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ObjetivosPoliticasPN_Politica");

            entity.HasIndex(e => new { e.ObjPnId, e.PoliticaPnId }).IsUnique();
        });

        modelBuilder.Entity<ObjetivoMetaPN>(entity =>
        {
            entity.HasKey(e => e.ObjetivoMetaPnId);
            entity.ToTable("ObjetivosMetasPN");

            entity.Property(e => e.ObjetivoMetaPnId).HasColumnName("objetivo_meta_pn_id");
            entity.Property(e => e.ObjPnId).HasColumnName("obj_pn_id");
            entity.Property(e => e.MetaPnId).HasColumnName("meta_pn_id");
            entity.Property(e => e.Estado).HasMaxLength(1).IsUnicode(false).HasDefaultValue("A").IsFixedLength().HasColumnName("estado");
            entity.Property(e => e.CreadoPor).HasColumnName("creado_por");
            entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion").HasColumnType("datetime").HasDefaultValueSql("(getdate())");
            entity.Property(e => e.ModificadoPor).HasColumnName("modificado_por");
            entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion").HasColumnType("datetime");

            entity.HasOne(d => d.Objetivo)
                .WithMany()
                .HasForeignKey(d => d.ObjPnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ObjetivosMetasPN_Objetivo");

            entity.HasOne(d => d.Meta)
                .WithMany()
                .HasForeignKey(d => d.MetaPnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ObjetivosMetasPN_Meta");

            entity.HasIndex(e => new { e.ObjPnId, e.MetaPnId }).IsUnique();
        });




        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
