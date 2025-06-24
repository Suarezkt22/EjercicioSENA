# Prueba Técnica: Aplicación Web para Registro de Estudiantes

Este proyecto implementa una solución basada en principios de DDD (Domain-Driven Design) con CQRS (Command Query Responsibility Segregation) y Arquitectura Hexagonal. Se sigue un enfoque de **Vertical Slicing** para dividir las funcionalidades. La aplicación consta de un backend desarrollado en .NET y un frontend en Angular.

## Requisitos

- **Backend**: .NET 6+
- **Frontend**: Angular 15+
- **Base de Datos**: SQL Server
- **Node.js**: 16+
- **Gestor de Paquetes**: npm o yarn

## Características

1. **CRUD para estudiantes**: Permite registrar, actualizar y eliminar estudiantes.
2. **Sistema de Créditos**: Los estudiantes pueden inscribirse en un programa de créditos.
3. **Restricciones**:

   - Cada materia tiene 3 créditos.
   - Un estudiante solo puede seleccionar 3 materias.
   - Las materias están distribuidas entre 5 profesores, y cada uno dicta 2 materias.
   - No es posible tener clases con el mismo profesor en más de una materia.

4. **Visualización de registros**:

   - Los estudiantes pueden ver los registros de otros.
   - Pueden ver los nombres de los alumnos que comparten clase.

## Configuración de la Base de Datos

1. Ubica el script de SQL adjunto en la raíz del proyecto.
2. Usa tu gestor de base de datos favorito (SSMS, Azure Data Studio, etc.) para ejecutar el script y crear las tablas necesarias y registros necesarios.

## Instalación

### Backend Server\PruebaRaddarStudios

1. Restaura los paquetes:

   ```bash
   dotnet restore
   ```

2. Inicia el servidor:

   ```bash
   dotnet run --urls http://localhost:5270
   ```

3. Documentacion de la API

   ```bash
   http://localhost:5270/swagger
   ```

### Frontend \UI

1. Instala las dependencias:

   ```bash
   npm install
   ```

2. Inicia el servidor de desarrollo:

   ```bash
   ng serve --open
   ```

## Uso

- El backend estará disponible en `http://localhost:5270`.
- El frontend estará disponible en `http://localhost:4200`.


## Arquitectura del Proyecto

- **DDD**: Separación en capas de dominio, aplicación e infraestructura.
- **CQRS**: Comandos y consultas están claramente separados.
- **Hexagonal**: Uso de puertos y adaptadores para facilitar el testing y la extensión.
- **Vertical Slicing**: Cada funcionalidad está encapsulada y desacoplada del resto.

## Licencia

Este proyecto es únicamente para fines evaluativos.

