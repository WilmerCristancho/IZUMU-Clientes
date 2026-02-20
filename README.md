# IzumuClientes – Gestión de Clientes

Sistema de gestión de clientes de salud desarrollado con Clean Architecture, .NET 8, SQL Server y ASP.NET Core MVC.

## Arquitectura

La solución está compuesta por los siguientes proyectos:

```
IzumuClientes/
├── API/                            # Web API REST (endpoints)
├── Application/                    # Casos de uso, DTOs, validaciones y servicios
├── Domain/                         # Entidades, interfaces y Result Pattern
├── Infraestructure/                # Repositorios, Dapper y conexión a BD
├── Web/                            # Frontend ASP.NET Core MVC
├── Tests/                          # Pruebas unitarias con xUnit
└── Database/
    └── ScriptDB_IZUMU.sql          # Script completo de base de datos
```

## Requisitos previos

Antes de ejecutar la solución, asegúrese de tener instalado:

| **Herramienta**              | **Versión recomendada** |
|------------------------------|-------------------------|
| Visual Studio                | 2022 o superior         |
| .NET SDK                     | 8.0                     |
| SQL Server                   | 2019 o superior         |
| SQL Server Management Studio | 18 o superior           |

## Instalación de la base de datos

### Paso 1: Ejecutar el script completo

Abra **SQL Server Management Studio (SSMS)**, conéctese a su instancia y siga estos pasos:

1. Vaya a Archivo → Abrir → Archivo
2. Seleccione el archivo `Database/ScriptDB_IZUMU.sql`
3. Presione **F5** para ejecutar

Este script crea automáticamente:
- La base de datos `IZUMU`
- El usuario `izumu_user` con permisos de propietario
- Todas las tablas con sus relaciones y restricciones
- La función `fn_FormatoFecha`
- Todos los Stored Procedures
- Los datos iniciales de tipos de documento y planes

### Paso 2: Verificar autenticación mixta

> SQL Server debe estar configurado en modo de **autenticación mixta** para que el usuario `izumu_user` funcione correctamente.

Para verificarlo:
1. En SSMS, clic derecho sobre el servidor → **Properties**
2. Sección **Security**
3. Seleccione **SQL Server and Windows Authentication mode**
4. Haga clic en **OK**
5. Reinicie el servicio de SQL Server

## Configuración de la solución

### Paso 1: Clonar el repositorio

```bash
git clone https://github.com/WilmerCristancho/IZUMU-Clientes
cd IzumuClientes
```

### Paso 2: Configurar la cadena de conexión en la API

Abra `API/appsettings.json` y actualice con el nombre de su instancia de **SQL Server**:

```json
{
  "ConnectionStrings": {
    "IZUMU": "Server=.;Database=IZUMU;User Id=izumu_user;Password=@Izumu2026*;TrustServerCertificate=True;Encrypt=False;"
  }
}
```

> Reemplace `Server=.` con el nombre de su instancia si es necesario.
> Ejemplos: `Server=localhost`, `Server=MIEQUIPO\SQLEXPRESS`.

### Paso 3: Verificar la URL de la API en el proyecto Web

Abra `Web/appsettings.json` y verifique que la URL apunte correctamente a la API:

```json
{
  "ApiSettings": {
    "BaseUrl": "https://localhost:7195/"
  }
}
```

> El puerto puede variar según su configuración local. Verifíquelo en `API/Properties/launchSettings.json` y actualícelo si es necesario.

## Ejecución de la solución

### Opción 1: Desde Visual Studio (recomendada)

1. Abra `IzumuClientes.slnx` en Visual Studio 2022
2. Clic derecho sobre la solución → **Propiedades**
3. Seleccione **Varios proyectos de inicio**
4. Configure en **Iniciar**:
   - `IzumuClientes.API` → **Iniciar**
   - `Web` → **Iniciar**
5. Presione **F5**

### Opción 2: Desde la terminal

Abra dos terminales y ejecute cada proyecto por separado:

**Terminal 1 — API:**
```bash
cd API
dotnet run
```

**Terminal 2 — Web:**
```bash
cd Web
dotnet run
```

## URLs de acceso

| **Proyecto**      | **URL**                  |
|-------------------|--------------------------|
| Frontend          | `https://localhost:7134` |
| API / Swagger     | `https://localhost:7195` |

> Los puertos pueden variar según la configuración local. Verifique los archivos `launchSettings.json` de cada proyecto.

---

## Ejecución de pruebas unitarias

### Desde Visual Studio
1. Vaya al proyecto **Tests** → **Ejecutar todas las pruebas**
2. O use el atajo **Ctrl + R, A**

### Desde la terminal
```bash
cd Tests
dotnet test
```

## Tecnologías utilizadas

| **Tecnología**           | **Uso**             |
|--------------------------|---------------------|
|   .NET 8                 | Framework principal |
|   ASP.NET Core Web API   | Backend REST        |
|   ASP.NET Core MVC       | Frontend            |
|   SQL Server 2019        | Base de datos       |
|   Dapper                 | ORM ligero          |
|   FluentValidation       | Validaciones        |
|   xUnit + Moq            | Pruebas unitarias   |
|   Bootstrap 5            | Estilos UI          |
|   Clean Architecture     | Patrón de diseño    |

## Notas importantes

- El sistema implementa **Soft Delete**: los registros eliminados no se borran físicamente de la base de datos, solo se marcan con `Eliminado = 1`.
- La validación de clientes duplicados se realiza por **tipo de documento + número de identificación**.
- El campo `NombreCliente` en `tbCliente` es una **columna calculada** que se genera automáticamente concatenando nombres y apellidos.
- Los clientes deben ser **mayores de 18 años** para poder registrarse en el sistema.