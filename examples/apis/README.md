# example-net-api

Proyectos de ejemplo de API en .Net Framework y .Net Core. En ambos se implementaron tres proyectos:
- api: un proyecto tipo web que implementa el API y tiene controladores de ejemplos y pruebas.
- api-client: un proyecto tipo consola que implementa la ejecuci�n de los ejemplos y pruebas en el API.
- api-utils: un proyecto tipo biblioteca de clases que implemente clases reutilizables para la implementaci�n del API.

Cada API implementa los siguientes requerimientos:
- Utilizan un manejo predeterminado de excepciones, por lo que no es necesario utilizar try-catch en cada m�todo.
- Utilizan el serializador Newtonsoft como serializador predeterminado para Json y establece las siguientes configuraciones predeterminadas:
    - El CamelCasePropertyNamesContractResolver para que las propiedades de las clases se serialicen en camel case.
    - El StringEnumConverter para que los enumerados se serialicen con el texto y no con el n�mero del enumerado.
    - Si es requerido se puede establecer en el DateFormatString el formato por defecto con el que serializan en texto los DateTimes.
- Utilizan la validaci�n autom�tica de los objetos que tengan anotaciones de validaci�n, por lo que no es necesario hacer la validaci�n del modelo en cada m�todo, el m�todo s�lo se invoca si el objeto es v�lido. Si el objeto es inv�lido se retorna un resultado inv�lido con los mensajes de validaci�n.
- Para .Net Framework se registran rutas personalizadas para habilitar el uso de m�ltiples m�todos con el mismo verbo de http y el uso de m�todos con el mismo nombre del verbo de http.
- Implementan clases para resultados personalizados, con el objetivo de estandarizar las respuestas que puede tener el API, con el fin de estandarizar el c�digo fuente y facilitar a la aplicaci�n que lo consuma el manejo de las respuestas. Para facilitar el uso de las clases de resultados se implementaron m�todos de extensi�n para el controlador. El m�todo del controlador deber� utilizar c�mo tipo de retorno �Result�, que es la clase base y utilizar el m�todo extendido para el resultado correspondiente:
	- �this.ResultValid(�)�: Para retornar un resultado v�lido. Se retorna el c�digo 200.
	- �this.ResultInvalid(�)� Para retornar un error provocado por los par�metros utilizados en la invocaci�n del API. Se retorna el c�digo 400.
	- �this.ResultError(�)� Para retornar un error provocado por un problema a lo interno del API. Se retorna el c�digo 500.
    - Los resultados de error tienen c�mo respuesta estandarizada una respuesta de la forma:

```js
//resultado inv�lidos
{
    errores :
    [
        { mensaje : "identificaci�n es requerido" },
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
- Test 1: M�ltiples m�todos con el mismo verbo de http con diferente nombre. Utilizaci�n del nombre del m�todo c�mo acci�n en el API y personalizaci�n del nombre de la acci�n para que no sea el nombre del m�todo.
- Test 2: Controlador para CRUD de entidad utilizando nombres de m�todo m�ltiples m�todos con el mismo verbo de http y resultados comunes (listas de objetos, objetos).
- Test 3: Controlador para CRUD de entidad utilizando nombres de m�todo m�ltiples m�todos con el mismo verbo de http y resultados con clases personalizadas.
- Test 4: Pruebas de manejo de errores.
- Test 5: Pruebas de serializaci�n de objeto con los tipos de datos m�s comunes, para visualizar el formato de serializaci�n en Json.

TODO:
- Archivos.
- Swagger.
- Autenticaci�n.
