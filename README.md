# 🚀 BIT.API - Proyecto .NET 🔍

Bienvenido al proyecto **BIT.API**, una API desarrollada en **.NET 8**.

---

## 📌 1️⃣ **Requisitos Previos**

Antes de iniciar el proyecto, asegúrate de tener instalados los siguientes componentes:

### 🗹 **1.1 Instalar .NET SDK**
Este proyecto requiere **.NET 8**. Puedes instalarlo con:

- **Windows/macOS/Linux**: Descarga desde [📌 .NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Verifica la instalación con:
  ```sh
  dotnet --version
  ```

### 🗹 **1.2 Instalar Visual Studio Code + Extensiones**
1. Descarga **VS Code** desde [📌 Visual Studio Code](https://code.visualstudio.com/)
2. Instala las extensiones recomendadas:
   - 🔹 **C#** (para .NET) → `ms-dotnettools.csharp`
   - 🔹 **SonarLint** (para análisis de código) → `SonarSource.sonarlint-vscode`
   - 🔹 **REST Client** (para probar APIs) → `humao.rest-client`
   - 🔹 **Markdown Preview Enhanced** (para archivos README.md) - `shd101wyy.markdown-preview-enhanced`

### 🗹 **1.3 Instalar SonarQube o SonarCloud (Opcional)**
Si deseas ejecutar análisis de seguridad, instala:
  ```sh
  dotnet tool install --global dotnet-sonarscanner
  ```

Verifica que `SonarQube` está instalado:
  ```sh
  dotnet sonarscanner --version
  ```

---

## 📌 2️⃣ **Instalación y Configuración del Proyecto**

### 🔹 **2.1 Clonar el Repositorio**
Ejecuta:
```sh
git clone https://github.com/tu-usuario/bit-api.git
cd bit-api
```

### 🔹 **2.2 Instalar Dependencias**
Ejecuta:
```sh
dotnet restore
```

### 🔹 **2.3 Configurar el `.env` (Opcional)**
Si usas **Application Insights**, agrega tu `InstrumentationKey` en `appsettings.json`:
```json
{
    "Logging": {
        "UseApplicationInsights": true,
        "ApplicationInsights": {
            "InstrumentationKey": "TU-INSTRUMENTATION-KEY"
        }
    }
}
```

Si no usarás **Application Insights**, configúralo en `false`:
```json
"UseApplicationInsights": false
```

---

## 📌 3️⃣ **Ejecución del Proyecto**

### 🔹 **3.1 Ejecutar el Servidor**
Para iniciar la API en **modo desarrollo**, ejecuta:
```sh
dotnet run --project src/API
```

Si todo está bien, deberías ver en la consola:
```
💪 Envio de logs solo a Consola y Archivos.
```
o si **`UseApplicationInsights = true`**:
```
💪 Envio de logs a Application Insights ACTIVADO.
```

### 🔹 **3.2 Acceder a Swagger**
Abre en el navegador:
```
http://localhost:5183/swagger
```

---

## 📌 4️⃣ **Uso de SonarLint para Análisis de Seguridad**
Para analizar el código con **SonarLint** en VS Code:
1. Abre la **Paleta de Comandos** (`Ctrl + Shift + P`).
2. Escribe: `"SonarLint: Analyze all files"` y presiona `Enter`.
3. Abre la pestaña **"Problemas"** (`Ctrl + Shift + M`) y revisa las advertencias de seguridad.

---

## 📌 5️⃣ **Estructura del Proyecto (me falta colocar las ultimas carpetas)**
```
bit-api/
│-- src/
│   ├-- API/                   # Proyecto principal (Web API)
│   │   ├-- Controllers/       # Controladores de la API
│   │   ├-- Program.cs         # Configuración de la API
│   │   ├-- appsettings.json   # Configuración global
│   ├-- Application/           # Lógica de Negocio (CQRS)
│   ├-- Dominio/               # Entidades y Interfaces
│   ├-- Infrastructure/        # Persistencia y Repositorios
│-- logs/                      # Carpeta de Logs (Ignorada en Git)
│-- .gitignore                 # Archivos ignorados en Git
│-- README.md                  # Documentación del Proyecto
```

---

## 📌 6️⃣ **Comandos Útiles**
### 🔹 **Compilar el Proyecto**
```sh
dotnet build
```

### 🔹 **Ejecutar Pruebas (Si existen)**
```sh
dotnet test
```

### 🔹 **Verificar Dependencias Vulnerables**
```sh
dotnet list package --vulnerable
```

---

## 📌 7️⃣ **Configuración de Logs con Serilog**
Este proyecto usa **Serilog** para registrar logs en:
- 📌 **Consola**
- 📌 **Archivos (`logs/api-log-<fecha>.txt`)**
- 📌 **Azure Application Insights** (si está habilitado)

Si quieres ver los logs en **tiempo real**, abre la carpeta `logs/` y revisa los archivos generados.

---

## 🗒️ **Emojis**
Los Emojis utilizados para el código y el README se utilizo:


https://emojipedia.org/