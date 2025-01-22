# ApiProveedores

En el presente proyecto se puede encontrar una Api realizada en .Net 6 con documentación en Swagger y configurado para docker, los archivos de configuración del contenedor son docker-compose.yml, Dockerfile y launchSettings.json.

Como base de datos se usa MongoDB y está configurado para usar Jwt Token como método de Autorización.

Se usó como uno de los patrones inyección de dependencias.

Para iniciar el contenedor a través de un símbolo del sistema y en la carpeta de la solución ejecutar

docker-compose up --build -d

Para ingresar a Swagger debe dirigirse a http://localhost:5000/swagger/index.html

En Swagger se encuentran todos los métodos de la aplicación.
