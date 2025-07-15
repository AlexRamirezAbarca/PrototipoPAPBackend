# API Prototipo Gestión PAP

Este repositorio contiene el prototipo de una API para la gestión de PAP. A continuación se explica detalladamente la estructura del proyecto, la configuración de la base de datos y la integración con el cliente.

## Tabla de Contenidos

- [Scripts para la Creación de la Base de Datos](#scripts-para-la-creación-de-la-base-de-datos)
    - [Orden de Ejecución](#orden-de-ejecución)
- [Configuración de la Conexión a la Base de Datos](#configuración-de-la-conexión-a-la-base-de-datos)
- [Configuración del Cliente y Conexión con la API](#configuración-del-cliente-y-conexión-con-la-api)
- [Notas Adicionales](#notas-adicionales)

## Scripts para la Creación de la Base de Datos

El repositorio contiene cinco scripts SQL que deben ejecutarse en el orden establecido para garantizar la correcta creación y configuración del entorno. Cada script tiene una función específica.

### Orden de Ejecución

| Orden | Script                                     | Descripción                                                                                          |
|-------|--------------------------------------------|------------------------------------------------------------------------------------------------------|
| 1     | **PAP - Crear Base de Datos.sql**          | Crea la base de datos `DBGestionPAP` y configura el usuario administrador.                           |
| 2     | **PAP - Crear Entidades y Catálogos.sql**   | Crea las tablas principales y establece sus relaciones y restricciones.                            |
| 3     | **PAP - Datos Iniciales - Catálogos.sql**    | Inserta los datos esenciales en los catálogos requeridos para el correcto funcionamiento.            |
| 4     | **PAP - Datos Iniciales - Auth.sql**         | Crea el usuario administrador y define los roles de acceso inicial para el sistema.                  |
| 5     | **PAP - Datos de Prueba.sql**                | Inserta datos de prueba para verificar el funcionamiento en entornos de desarrollo (no usar en producción). |

> **Advertencia:** El script **PAP - Datos de Prueba.sql** está diseñado únicamente para entornos de desarrollo. No se recomienda ejecutarlo en producción.
## Configuración de la Conexión a la Base de Datos

Para conectar la API con una instancia de SQL Express, modifique la cadena de conexión en el archivo `appsettings.json`. Use los mismos parámetros (nombre de usuario, base de datos y contraseña) que se definen en los scripts SQL.

Es importante considerar que el servidor puede variar dependiendo de la versión de SQL Server que se esté utilizando o del puerto en el que se haya decidido desplegar el servidor. Asegúrese de ajustar la dirección del servidor y el puerto de acuerdo a su entorno.

Ejemplo de configuración:

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=DBGestionPAP;User Id=papAdmin;Password=PapPassword123;Trusted_Connection=True;"
    }
}
```

## Configuración del Cliente y Conexión con la API

Para conectar un cliente (por ejemplo, una aplicación web o móvil) con la API, es necesario configurar la URL base. Esto se realiza también en el archivo `appsettings.json`.

Ejemplo de configuración:

```json
{
        "ApiSettings": {
                "BaseUrl": "https://localhost:5001"
        }
}
```

Si se requiere cambiar el puerto o actualizar la URL del API, modifique la propiedad `applicationUrl` en el archivo `launchSettings.json`. Por ejemplo:

```json
{
        "applicationUrl": "http://localhost:5278"
}
```

## Notas Adicionales

- Verifique que la instancia de SQL Express se encuentre activa y accesible desde el entorno donde se ejecuta la API.
- Asegúrese de que los encabezados y los scripts SQL se ejecuten en el orden correcto para evitar conflictos o datos incompletos.