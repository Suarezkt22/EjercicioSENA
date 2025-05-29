# PruebaTecnicaInterrapidisimo [Employee Service]

Este proyecto incluye una aplicación desarrollada en **.NET** para el backend siguiendo la arquitectura de MVC y **Angular** para el frontend. A continuación, encontrarás las instrucciones necesarias para ejecutar y probar el proyecto.

---

## Requisitos Previos

1. **Software necesario**:
   - [.NET 8 SDK o superior](https://dotnet.microsoft.com/download).
   - [Node.js (LTS)](https://nodejs.org/).
   - [Angular CLI](https://angular.io/guide/setup-local):
     ```bash
     npm install -g @angular/cli
     ```
     
2. **Herramientas recomendadas**:
   - Editor de texto: [Visual Studio Code](https://code.visualstudio.com/) o [Visual Studio](https://visualstudio.microsoft.com/es/).

---

## Configuración del Proyecto

### Backend (.NET)

1. **Restaurar dependencias**:
   - Navega a la carpeta del backend:
     ```bash
     cd /Server/PruebaTecnicaInterrapidisimo
     ```
   - Ejecuta:
     ```bash
     dotnet restore
     ```

2. **Ejecutar el proyecto**:
   - Inicia el backend:
     ```bash
     dotnet run
     ```
   - El backend estará disponible en: `http://localhost:5270`.

---

### Frontend (Angular)

1. **Instalar dependencias**:
   - Navega a la carpeta del frontend:
     ```bash
     cd /UI
     ```
   - Ejecuta:
     ```bash
     npm install
     ```

2. **Ejecutar el servidor de desarrollo**:
   - Inicia el servidor Angular:
     ```bash
     ng serve
     ```
   - Accede a la aplicación en: [http://localhost:4200].

---

## Funcionalidades

- **Backend**:
  - Endpoints disponibles para las funcionalidades del proyecto.
  - Documentación de la API (Swagger) en: [http://localhost:5270/].

- **Frontend**:
  - Interfaz de usuario desarrollada en Angular.
  - Consumo de servicios REST para interacción con el backend.

---

## Pruebas

1. **Pruebas unitaria del backend**:
    - Navega a la carpeta del backend:
     ```bash
     cd /Server/PruebaTecnicaInterrapidisimo.Tests
     ```

   - Ejecuta las pruebas:
     ```bash
     dotnet test
     ```

      

2. **Pruebas unitaria del frontend**:
   - Ejecuta las pruebas:
     ```bash
     ng test
     ```
