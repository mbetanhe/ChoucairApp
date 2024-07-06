# Chaoucair Test

Este proyecto corresponde a una prueba tecnica para la vacante de desarrollador. El proyecto cuenta esta divido en tres partes las cuales son backend, frontend y nube.
El aplicativo en su parte de back esta construido utilizando una arquitectura limpia (Clean Architecture) y teniendo como base el patro DDD. El acceso a la información se realiza por medio del patron CQRS para generar mas escalabilidad al momento de implementar nuevas funcionalidad o modificar las entidades ya existentes.

## Comenzando 🚀

_Estas instrucciones te permitirán obtener una copia del proyecto en funcionamiento en tu máquina local para propósitos de desarrollo y pruebas._

Mira **Deployment** para conocer como desplegar el proyecto.

### Pre-requisitos 📋

El backend esta construido bajo .NET 8. Por lo tanto, para su correcto funcionamiento se requiere que el servidor o equipo donde vaya ser utilizado dicha aplicación cuente con el runtime de .NET 8 instalado. Por otro lado,
el frontend esta construido bajo Angular 17 utilizando NodeJS 20.15 LTS y NPM 10.8.1. Por ultimo, la base de datos fue construida por medio de CodeFirst y se encuentra en SQL Server.

```
- Runtime ASP .NET 8
- Windows 10/11 - Windows Server 2019/2022
- MS SQL Server >= 2016
- NodeJS 20.15.0
- NPM 10.8.1
- Angulas CLI 17
```

### Instalación 🔧

El proyecto tiene la posibilidad de ejecutarde dos formas. Local y nube.
Para al ejecución local del proyecto se requiere cumplir con los requisitos anteriormente mensionados, Adicional, se deben realizar los siguientes pasos.
1. En una instancia de SQL Server crear una base de datos llamada ChoucairBD.
2. Ejecutar los scripts que se encuentran en la carpeta Local/Scripts
3. Crear un sitio en IIS para el API. importante que el sitio este configurado para soportar aplicaciones en .NET 8 y adicional a ello que tenga acceso a la base de datos. [Ver guía](https://learn.microsoft.com/en-us/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-8.0&tabs=visual-studio) 
4. Crear un sitio en IIS o su servidor de preferencia para alojar el sitio en Angular. [Ver guia](https://www.c-sharpcorner.com/article/deply-of-a-angular-application-on-iis/)
5. Iniciar el servicio de IIS que contiene el Web Api e ingresar al creado en el punto 4.
```
Pasos para la ejecución del proyecto en local sin necesidad de IDEs de desarrollo para ejecutar los aplicativos.
```
Para la ejecución del proyecto en la nube es mucho mas sensillo, solo basta con acceder a las siguientes URL:

1. Sitio en Angular. [Ingresar](https://choucairspa.azurewebsites.net/)
2. Web API publico. [Ingresar](https://choucairapi.azurewebsites.net/swagger/index.html)

```
Se puede acceder a ambos sitios desde cualquier parte. Cada uno esta alojado en Azure
```
Finalmente, si se desea ejecuttar el proyecto utilizando el codigo fuente se debe realizar lo siguiente:
1. En una instancia de SQL Server crear una base de datos llamada ChoucairBD.
2. Ejecutar los scripts que se encuentran en la carpeta Local/Scripts
3. Clonar el repositorio en la maquina donde se desea abrir el proyecto. Recomendado tener Visual Studio 2022 ó Visual Studio Code. 
4. Iniciar el API o el proyecto ChoucairApp.Presentation.API para obtener la URL.
```
Por defecto tiene las urls https://localhost:7056 y http://localhost:5199.
```
5. Una vez se tenga la URL, en especial la que contenfga el protocolo https. Abrir el proyexcto de Angular e ir a la siguiente ruta:
```
src\settings\appsettings.ts
```
6. En el archivo de la ruta establecida anteriormente, se debe cambiar la URL por la URL local del proyecto en ejecución sea desde el Visual Studio, servidor local o servidor en la nube.
7. Iniciar el proyecto y probar

_Finaliza con un ejemplo de cómo obtener datos del sistema o como usarlos para una pequeña demo_

## Ejecutando las pruebas ⚙️

El proyecto cuenta con unas pequeñas pruebas unitarias. Estas se encuentran en la solución de Visual Studio:
```
src\Test\ChoucairApp.Tests
```
## Despliegue 📦
Es importante que paar el despliegue en local se deben tener las librerias descritas al inicio de este docuemento. También tener en cuenta que las herramientas donde se alojaran los sitios deben estar bien configurados con base en las guías. 

El proyecto fue desplegado en la nube para facilitar las pruebas y visualización.

## Construido con 🛠️

* **Arquitectura de Programación**
    * Domain Driven Design
* **Tecnologías**
    * Base de Datos MS SQl Server
    * Entity Framework Core 8
    * Mediator
    * CQRS (Command and Query Responsability Segregation)
    * AutoMapper
    * Wrapper
    * xUnit Tests
    * Inversión de Dependencias
    * NodeJS 20.15.0
    * Angular CLI 17
    * GitHub
    * Azure

## Wiki 📖

Información util sobre los frameworks y plugins utilizados en el proyecto:

- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Domain Driven Design](https://redis.io/glossary/domain-driven-design-ddd/)
- [IIS](https://www.c-sharpcorner.com/article/deply-of-a-angular-application-on-iis/)
- [Angular](https://angular.dev/)
- [CQRS y MediaTR](https://www.milanjovanovic.tech/blog/cqrs-pattern-with-mediatr)
- [Bootstrap](https://getbootstrap.com/docs/5.3/getting-started/introduction/)

## Autores ✒️

* **Mateo Betancur Hernández** - *Apasionado por la programación* - [MateoBetaH](https://www.linkedin.com/in/mateobetah/)

## Licencia 📄
Este proyecto está bajo propiedad publica

## Expresiones de Gratitud 🎁

⌨️ con ❤️ por [MateoBH](#) 😊

