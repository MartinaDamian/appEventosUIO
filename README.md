# 🏛️ Quito Vibes - Sistema de Gestión Cultural (ASP.NET Core MVC)

[![.NET Version](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![Architecture](https://img.shields.io/badge/Pattern-MVC-blue?style=for-the-badge)](https://learn.microsoft.com/aspnet/core/mvc/overview)
[![Security](https://img.shields.io/badge/Auth-Cookie_%2B_MD5-red?style=for-the-badge&logo=security)](https://learn.microsoft.com/aspnet/core/security/authentication/cookie)
[![Design](https://img.shields.io/badge/Style-Municipio_de_Quito-002B49?style=for-the-badge)](https://www.quito.gob.ec/)

Aplicación web desarrollada bajo el patrón **Model-View-Controller (MVC)** para la administración de eventos de la agenda cultural del **Distrito Metropolitano de Quito**. Incluye sistema de **Autenticación (Login)** con contraseñas encriptadas en **MD5**, **protección estricta de rutas mediante atributos `[Authorize]`**, un módulo **CRUD completo** (Crear, Leer, Actualizar, Eliminar) y una interfaz con la **identidad visual oficial del Municipio de Quito**.

---

## 🎨 Identidad Visual y Paleta de Colores (Municipio de Quito)

El diseño del proyecto recrea la estética oficial de los portales web del Municipio de Quito mediante la paleta de colores requerida:

| Color | Código Hex | Uso en la Aplicación |
| :--- | :--- | :--- |
| **Azul Institucional Quito** | `#002B49` | Cabeceras principales, Navbar, Títulos de tablas y Footers |
| **Rojo Alcaldía** | `#D3122A` | Botones de acción principales, Badges destacados y detalles del banner |
| **Azul Plomo Bajo (Slate)** | `#334155` | Tarjetas de contenido, bordes de sección y acentos secundarios |
| **Plomo Claro Fondo** | `#F8FAFC` | Fondo de pantalla y contenedores |

---

## 🔐 Características de Seguridad y Autenticación

### 1. Encriptación de Contraseñas (MD5)
Las contraseñas de los usuarios no se almacenan en texto plano en la base de datos. Se utiliza el servicio `SecurityService` que implementa el algoritmo **MD5** (y SHA-256) para generar hashes criptográficos:

```csharp
public string HashPasswordMd5(string input)
{
    using (MD5 md5 = MD5.Create())
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("x2"));
        }
        return sb.ToString();
    }
}
```

### 2. Protección de Rutas (`[Authorize]`)
El controlador de la sección privada `EventosController` está estrictamente decorado con `[Authorize]`. Intentar ingresar a cualquier URL interna (ej. `/Eventos`, `/Eventos/Create`, `/Eventos/Edit/1`) sin una Cookie de sesión activa **bloquea el acceso y redirige automáticamente** a `/Account/Login?ReturnUrl=...`.

---

## 🔑 Credenciales de Prueba para Evaluación

Al iniciar la aplicación por primera vez, el sistema puebla la base de datos mediante `DbInitializer` con las siguientes credenciales predeterminadas:

| Rol | Usuario / Correo | Contraseña | Hash MD5 Almacenado |
| :--- | :--- | :--- | :--- |
| **Administrador** | `admin` | `admin123` | `0192023a7bbd73250516f069df18b500` |
| **Ciudadano** | `usuario` | `quito123` | `75a7c20ad761ffecaebe1b63e80f2d3d` |

---

## 🚀 Instrucciones para Ejecutar el Proyecto

### Requisitos Previos
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download) o **.NET 9.0 SDK** instalado en el sistema.

### Pasos de Ejecución
1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/tu-usuario/quito-vibes-mvc.git
   cd quito-vibes-mvc/QuitoVibesMvc
   ```

2. **Ejecutar la aplicación:**
   ```bash
   dotnet run
   ```

3. **Abrir en el navegador:**
   Navega a `http://localhost:5000` o la URL indicada en la consola (ej: `https://localhost:7001`).

---

## 📹 Guión Detallado para Grabar el Video (Máximo 3 Minutos)

Utiliza esta estructura paso a paso para grabar tu video demostrativo en Loom o YouTube:

```mermaid
timeline
    title Estructura del Video Demostrativo (3 Minutos)
    0:00 - 0:45 : Demostración de Seguridad & Bloqueo de URLs
    0:45 - 1:30 : Login & Explicación de Encriptación MD5
    1:30 - 2:30 : Demostración del Módulo CRUD Completo
    2:30 - 3:00 : Logout & Verificación Final
```

### ⏱️ Desglose Minuto a Minuto

#### **1. Bloqueo de Rutas no Autenticadas (0:00 - 0:45)**
* **Acción:** Abre el navegador en modo incógnito (o asegúrate de no haber iniciado sesión).
* **Acción:** Copia e intenta acceder directamente a la URL protegida: `http://localhost:5000/Eventos` o `http://localhost:5000/Eventos/Create`.
* **Lo que muestra el video:** El sistema rebota la solicitud y redirige automáticamente a `http://localhost:5000/Account/Login?ReturnUrl=/Eventos`.
* **Discurso sugerido:** *"Como se puede observar, al intentar ingresar a la sección de administración de eventos sin haber iniciado sesión, la aplicación bloquea el acceso y nos redirige a la pantalla de Login manteniendo el ReturnUrl."*

#### **2. Inicio de Sesión y Encriptación MD5 (0:45 - 1:30)**
* **Acción:** Ingresa el usuario `admin` y la contraseña `admin123`. Presiona **Ingresar al Sistema**.
* **Acción:** Muestra rápidamente el archivo `Services/SecurityService.cs` en el editor de código mostrando la función `HashPasswordMd5`.
* **Lo que muestra el video:** Inicio de sesión exitoso y la insignia del usuario `admin` en la barra superior.
* **Discurso sugerido:** *"Al autenticarnos con las credenciales admin / admin123, el sistema valida la contraseña convirtiéndola a hash MD5 mediante el SecurityService y comparándola con el registro en la base de datos SQL."*

#### **3. Demostración del CRUD Protegido (1:30 - 2:30)**
* **Acción (Read):** Muestra el listado de eventos con la estética del Municipio de Quito (Azul `#002B49`, Rojo `#D3122A` y Plomo Bajo `#334155`).
* **Acción (Create):** Haz clic en **"+ Crear Nuevo Evento"**, llena el formulario (ej: *"Desfile de las Fiestas de Quito"*) y guarda.
* **Acción (Edit):** Edita el evento recién creado o uno existente cambiando la fecha o el precio.
* **Acción (Delete):** Haz clic en **"Eliminar"** y confirma la remoción.
* **Discurso sugerido:** *"Estando autenticados, accedemos al módulo CRUD completo donde podemos listar los eventos culturales de Quito, crear nuevas actividades, modificar registros existentes y eliminar eventos finalizados."*

#### **4. Cierre de Sesión y Comprobación Final (2:30 - 3:00)**
* **Acción:** Presiona el botón **"Cerrar Sesión"** en el Navbar.
* **Acción:** Intenta presionar el botón "Atrás" del navegador o ingresar nuevamente a `/Eventos`.
* **Lo que muestra el video:** La sesión se destruye correctamente y el sistema exige autenticación nuevamente.
* **Discurso sugerido:** *"Al cerrar sesión, la Cookie de autenticación se elimina y las URLs quedan nuevamente protegidas contra accesos no autorizados."*

---

## 📂 Estructura del Código Fuente

```
QuitoVibesMvc/
├── Controllers/
│   ├── AccountController.cs       # Login, Logout y control de acceso
│   ├── EventosController.cs       # Controller CRUD [Authorize]
│   └── HomeController.cs          # Inicio del portal
├── Data/
│   ├── ApplicationDbContext.cs    # DbContext EF Core
│   └── DbInitializer.cs           # Datos de prueba e inicialización
├── Models/
│   ├── User.cs                    # Entidad de Usuario
│   ├── EventoCultural.cs          # Entidad de Evento (CRUD)
│   └── ViewModels/
│       └── LoginViewModel.cs      # Modelo del formulario de login
├── Services/
│   └── SecurityService.cs         # Implementación de Hashing MD5 / SHA256
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml           # Vista Login (Estilo Municipio)
│   │   └── AccessDenied.cshtml    # Vista Acceso Denegado
│   ├── Eventos/                   # Vistas del CRUD (Index, Create, Edit, Details, Delete)
│   └── Shared/
│       ├── _Layout.cshtml         # Layout base con cabecera y footer institucional
│       └── _LoginPartial.cshtml   # Estado de sesión de usuario
└── wwwroot/
    └── css/
        └── municipio-theme.css    # Paleta de colores Azul, Rojo y Plomo Bajo
```

---

*Desarrollado para la materia de Ingeniería Web - 7mo Semestre.*
