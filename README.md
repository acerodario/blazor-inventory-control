# PruebaFinal - Sistema de Gestión de Productos

> Aplicación web  de gestión de inventario desarrollada con .NET Blazor, C#, SQL Server, HTML y CSS.

## Descripción

**PruebaFinal** es una plataforma de gestión de productos que permite a los usuarios administrar un inventario completo con operaciones CRUD (Crear, Leer, Actualizar, Eliminar), autenticación de usuarios y exportación de datos en múltiples formatos.

## Características Principales

### Autenticación
- Sistema de login seguro
- Gestión de sesiones de usuario
- Control de acceso a funcionalidades

### Gestión de Productos (CRUD)
- Crear nuevos productos
- Leer información de productos
- Actualizar datos de productos existentes
- Eliminar productos del sistema
- Búsqueda y filtrado de productos
- Validación de datos en tiempo real

### Exportación de Datos
Descarga de informes en múltiples formatos:
- Excel (.xlsx) - Con formato y estilos
- CSV (.csv) - Formato delimitado por comas
- PDF (.pdf) - Documentos profesionales

### Interfaz de Usuario
- Diseño moderno con Bootstrap
- UI intuitiva y fácil de usar

---

## Stack Tecnológico

### Backend
- **.NET 8** - Framework principal
- **Blazor Server** - Componentes interactivos del lado del servidor
- **C#** - Lenguaje de programación
- **Entity Framework Core** - ORM para acceso a datos

### Base de Datos
- **SQL Server** - Sistema de gestión de base de datos relacional
- Migraciones automáticas
- Stored procedures opcionales

### Frontend
- **Razor Components** - Componentes reutilizables
- **HTML5** - Estructura
- **CSS3** - Estilos
- **Bootstrap 5** - Framework CSS responsivo

### Librerías NuGet Principales

#### Base de Datos
- `Microsoft.EntityFrameworkCore` - ORM
- `Microsoft.EntityFrameworkCore.SqlServer` - Proveedor SQL Server
- `Microsoft.EntityFrameworkCore.Tools` - Herramientas para migraciones

#### Exportación de Datos
- `ClosedXML` - Generación de archivos Excel
- `iTextSharp` - Generación de archivos PDF
- `DocumentFormat.OpenXml` - Generación de archivos Word

#### Otros
- `Microsoft.AspNetCore.Session` - Gestión de sesiones
- `Microsoft.JSInterop` - Interoperabilidad JavaScript

---

## Instalación y Configuración

### Requisitos Previos
- **.NET 8 SDK** instalado ([descargar](https://dotnet.microsoft.com/download))
- **SQL Server 2019+** o **SQL Server Express** instalado
- **Visual Studio 2022** o **Visual Studio Code**

### Pasos de Instalación

#### 1. Clonar el Repositorio
```bash
git clone https://github.com/tuusuario/PruebaFinal.git
cd PruebaFinal
```

#### 2. Configurar la Cadena de Conexión
Edita `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tu-servidor;Database=PruebaFinalDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

**Ejemplos de cadenas de conexión:**
- Local (Windows Auth): `Server=(localdb)\\mssqllocaldb;Database=PruebaFinalDB;Trusted_Connection=true;`
- Local (SQL Auth): `Server=localhost;Database=PruebaFinalDB;User Id=sa;Password=tucontraseña;`

#### 3. Instalar Paquetes NuGet
```bash
dotnet restore
```

#### 4. Aplicar Migraciones
```bash
dotnet ef database update
```

#### 5. Ejecutar la Aplicación
```bash
dotnet run
```

La aplicación estará disponible en: `https://localhost:7158`

---

## Cómo Usar

### Acceso Inicial
1. Abre la aplicación en tu navegador
2. Accede a la pantalla de login
3. Ingresa tus credenciales

### Gestión de Productos

#### Agregar Producto
1. Navega a "Agregar Producto"
2. Completa el formulario con los datos del producto
3. Haz clic en "Guardar"

#### Ver Productos
1. Ve a "Productos"
2. Se mostrarán todos los productos en una tabla
3. Puedes buscar y filtrar

#### Editar Producto
1. Haz clic en el botón "Editar" del producto
2. Modifica los datos necesarios
3. Guarda los cambios

#### Eliminar Producto
1. Haz clic en "Eliminar"
2. Confirma la acción

#### Exportación de Datos
1. Ve a "Exportar Productos"
2. Selecciona el formato deseado:
   - Excel - Para análisis en hojas de cálculo
   - CSV - Para importar a otras aplicaciones
   - PDF - Para reportes profesionales
3. El archivo se descargará automáticamente

---

## Estructura del Proyecto

```
PruebaFinal/
│
├── Components/          # Componentes Razor
│   ├── App.razor
│   ├── Layout/
│   └── Pages/
│       ├── Login.razor
│       ├── AgregarProducto.razor
│       ├── EditarProducto.razor
│       ├── EliminarProducto.razor
│       ├── ExportarDatos.razor
│       └── ...
│
├── Data/               # Contexto de BD
│   └── AppDbContext.cs
│
├── Models/             # Modelos de datos
│   ├── Producto.cs
│   ├── Usuario.cs
│   └── ...
│
├── Services/           # Servicios de negocio
│   ├── IProductoService.cs
│   ├── ProductoService.cs
│   ├── IAuthService.cs
│   ├── AuthService.cs
│   ├── IExportService.cs
│   └── ExportService.cs
│
├── wwwroot/           # Archivos estáticos
│   ├── css/
│   ├── js/
│   └── bootstrap/
│
├── Program.cs         # Configuración principal
├── appsettings.json   # Configuraciones
└── PruebaFinal.csproj # Archivo del proyecto
```

---

## Configuración del Proyecto

### Program.cs - Configuraciones Clave

```csharp
// Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Base de Datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servicios
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IExportService, ExportService>();

// Sesiones
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
```

---

## Base de Datos

### Tablas Principales

#### Tabla: Productos
```sql
CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(255) NOT NULL,
    Codigo NVARCHAR(100) NOT NULL UNIQUE,
    Categoria NVARCHAR(100) NOT NULL,
    Marca NVARCHAR(100),
    PrecioCosto DECIMAL(18,2) NOT NULL,
    PrecioVenta DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    StockMinimo INT,
    Descripcion NVARCHAR(MAX),
    FechaCreacion DATETIME DEFAULT GETDATE()
);
```

#### Tabla: Usuarios
```sql
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Usuario NVARCHAR(100) NOT NULL UNIQUE,
    Contraseña NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE()
);
```

---

## Personalización

### Cambiar Colores y Estilos
Edita `wwwroot/app.css` y `wwwroot/bootstrap/` para personalizar el diseño.

### Modificar Timeouts de Sesión
En `Program.cs`, ajusta:
```csharp
options.IdleTimeout = TimeSpan.FromMinutes(30); // Cambia el valor
```

---

## Solución de Problemas

### Error: "Base de datos no encontrada"
- Verifica la cadena de conexión en `appsettings.json`
- Ejecuta: `dotnet ef database update`

### Error: "Las descargas no funcionan"
- Asegúrate de que `window.downloadFile` está en `wwwroot/index.html`
- Limpia el caché del navegador (Ctrl+Shift+Delete)

### Error: "No puedo iniciar sesión"
- Verifica que la tabla `Usuarios` existe en la BD
- Intenta recrear la BD con migraciones

---

## Notas de Desarrollo

- Blazor Server renderiza componentes en el servidor
- La directiva @rendermode InteractiveServer es requerida para eventos
- Entity Framework Core maneja automáticamente las migraciones
- Los archivos se descargan usando JavaScript interop

---

##  Licencia

Este proyecto está bajo licencia **MIT**. Siéntete libre de usarlo y modificarlo.

---

##  Autor

Desarrollado por **[Leon Dario Acero Zapata]**

---

##  Contribuciones

Las contribuciones son bienvenidas. Para cambios importantes:
1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

---

**Última actualización:** Noviembre 2025
