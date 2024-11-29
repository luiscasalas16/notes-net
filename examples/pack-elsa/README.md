# ejecutar en docker

Run Elsa Server + Studio
```powershell
docker pull elsaworkflows/elsa-server-and-studio-v3:latest
docker run -t -i -e ASPNETCORE_ENVIRONMENT='Development' -e HTTP_PORTS=8080 -e HTTP__BASEURL=http://localhost:13000 -p 13000:8080 elsaworkflows/elsa-server-and-studio-v3:latest
```
Username: admin
Password: password

# ejemplos

https://github.com/elsa-workflows/elsa-core/tree/master/src/samples
https://v3.elsaworkflows.io/

```powershell
#obtener definiciones de flujos
$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("Authorization", "ApiKey 00000000-0000-0000-0000-000000000000")
$response = Invoke-RestMethod 'http://localhost:5001/elsa/api/workflow-definitions/' -Method 'GET' -Headers $headers
$response | ConvertTo-Json
```

```powershell
#ejecución de flujo de ejemplo
$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("Authorization", "ApiKey 00000000-0000-0000-0000-000000000000")
$headers.Add("Content-Type", "application/json")
$body = @"
{
    `"input`": {
        `"Employee`": {
            `"Name`": `"Alice Smith`",
            `"Email`": `"alice.smith@acme.com`"
        }
    }
}
"@
$response = Invoke-RestMethod 'http://localhost:5001/elsa/api/workflow-definitions/3e970eee281541c9/execute' -Method 'POST' -Headers $headers -Body $body
$response | ConvertTo-Json
```