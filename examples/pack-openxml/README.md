# example-net-excel

Proyecto de ejemplo de lectura y escritura de archivo excel, utilizando DocumentFormat.OpenXml.

El Helper tiene la implementación de los métodos para realizar la lectura y escritura de datos, que se hace utilizando un DataTable como estructura genérica de intercambio de datos.

El Data contiene la definición de las clases de Usuarios y Ordenes; y realiza la generación de datos de prueba utilizando Bogus. Además, tiene los métodos para hacer las transformaciones entre listas de objetos (Usuarios y Ordenes) y DataTable y viceversa.

En el Program se realizan 2 ejemplos:
- La creación de un archivo, en la que se toman las listas de objetos de prueba, se transforman a DataTables, se crea un archivo excel vació y se insertan dos hojas con los datos, una para cada conjunto de datos y finalmente se escribe el archivo.
- La lectura de un archivo, en la que se toma un archivo con dos hojas, se leen los datos de cada hoja cómo un DataTable y finalmente se convierten los DataTables en listas de objetos (Usuarios y Ordenes).

Referencias:
- (https://www.nuget.org/packages/DocumentFormat.OpenXml/)
- (https://github.com/bchavez/Bogus/)
