# example-net-wcf

Proyecto de ejemplo de WCF en .Net Core y .Net Framework.

Se implementa un cliente y un servicio en ambos frameworks, en .Net Core utilizando (https://github.com/CoreWCF/CoreWCF) y en .Net Framework utilizando WCF tradicional. Se configura el mismo servicio para utilizar varios protocoles de comunicación: basic http, ws http y net tcp.

El ejemplo demuestra que los servicios son interoperables y pueden ser llamados por ambos clientes, es decir, desde .Net Core se pueden llamar el servicio de .Net Framework y viceversa.