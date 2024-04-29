# example-net-webapi

Proyecto de ejemplo de un web api modelo en .Net Core.
-	Utiliza un patrón repositorio para mediar entre la aplicación y la base de datos.
-	Utiliza Nlog para realizar el log de la aplicación.
-	Utiliza Data Transfer Objects (DTOs) para mapear los objetos modelo y utilizarlos para enviar y recibir datos del cliente.
-	Implementa las operaciones básicas de mantenimiento (GET, POST, PUT, DELETE).
-	Implementa operaciones avanzadas cómo paginación, búsqueda y ordenamiento en un método especial de GET.

Operaciones básicas:
-	get list - GET - (http://localhost:5025/api/person)
-	get one - GET - (http://localhost:5025/api/person/xyz)
-	insert - POST - (http://localhost:5025/api/person) with { data }
-	update - PUT - (http://localhost:5025/api/person/xyz) with { data }
-	delete - DELETE - (http://localhost:5025/api/person/xyz)

Operaciones avanzadas:
-	get list page - GET - (http://localhost:5025/api/person/page?pageNumber=1&pageSize=10)
-	get list page search - GET - (http://localhost:5025/api/person/page?pageNumber=1&pageSize=10&searchname=Bill)
-	get list page order - GET - (http://localhost:5025/api/person/page?pageNumber=1&pageSize=10&orderBy=Id)
