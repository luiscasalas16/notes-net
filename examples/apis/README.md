# example-net-api

Proyectos de ejemplo de API en .Net Framework y .Net Core. En ambos se implementaron tres proyectos:
- api: un proyecto tipo web que implementa el API y tiene controladores de ejemplos y pruebas.
- api-client: un proyecto tipo consola que implementa la ejecución de los ejemplos y pruebas en el API.
- api-utils: un proyecto tipo biblioteca de clases que implemente clases reutilizables para la implementación del API.

Cada API implementa los siguientes requerimientos:
- Utilizan un manejo predeterminado de excepciones, por lo que no es necesario utilizar try-catch en cada método.
- Utilizan el serializador Newtonsoft como serializador predeterminado para Json y establece las siguientes configuraciones predeterminadas:
    - El CamelCasePropertyNamesContractResolver para que las propiedades de las clases se serialicen en camel case.
    - El StringEnumConverter para que los enumerados se serialicen con el texto y no con el número del enumerado.
    - Si es requerido se puede establecer en el DateFormatString el formato por defecto con el que serializan en texto los DateTimes.
- Utilizan la validación automática de los objetos que tengan anotaciones de validación, por lo que no es necesario hacer la validación del modelo en cada método, el método sólo se invoca si el objeto es válido. Si el objeto es inválido se retorna un resultado inválido con los mensajes de validación.
- Para .Net Framework se registran rutas personalizadas para habilitar el uso de múltiples métodos con el mismo verbo de http y el uso de métodos con el mismo nombre del verbo de http.
- Implementan clases para resultados personalizados, con el objetivo de estandarizar las respuestas que puede tener el API, con el fin de estandarizar el código fuente y facilitar a la aplicación que lo consuma el manejo de las respuestas. Para facilitar el uso de las clases de resultados se implementaron métodos de extensión para el controlador. El método del controlador deberá utilizar cómo tipo de retorno “Result”, que es la clase base y utilizar el método extendido para el resultado correspondiente:
	- “this.ResultValid(…)”: Para retornar un resultado válido. Se retorna el código 200.
	- “this.ResultInvalid(…)” Para retornar un error provocado por los parámetros utilizados en la invocación del API. Se retorna el código 400.
	- “this.ResultError(…)” Para retornar un error provocado por un problema a lo interno del API. Se retorna el código 500.
    - Los resultados de error tienen cómo respuesta estandarizada una respuesta de la forma:

```js
//resultado inválidos
{
    errores :
    [
        { mensaje : "identificación es requerido" },
        { mensaje : "nombre es requerido" }
    ]
}

//resultado error
{
    errores :
    [
        { mensaje : "internal api error" }
    ]
}
```

Cada API implementa los siguientes ejemplos y pruebas:
- Test 1: Múltiples métodos con el mismo verbo de http con diferente nombre. Utilización del nombre del método cómo acción en el API y personalización del nombre de la acción para que no sea el nombre del método.
- Test 2: Controlador para CRUD de entidad utilizando nombres de método múltiples métodos con el mismo verbo de http y resultados comunes (listas de objetos, objetos).
- Test 3: Controlador para CRUD de entidad utilizando nombres de método múltiples métodos con el mismo verbo de http y resultados con clases personalizadas.
- Test 4: Pruebas de manejo de errores.
- Test 5: Pruebas de serialización de objeto con los tipos de datos más comunes, para visualizar el formato de serialización en Json.

TODO:
- Archivos.
- Swagger.
- Autenticación.
