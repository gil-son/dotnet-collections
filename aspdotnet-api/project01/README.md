
.NET é um ecossistema assim como o Spring e o ASP.NET é uma parte desse ecossistema assim como o Springboot faz parte do ecossistema Spring

Comando para criar api: 

```
dotnet new webapi -n myCRUD
```

Existem outros comando, se fosse a criação de um mvc, seria: 

```
dotnet new mvc -n myCRUD
```

Abra o arquivo launchSettings.json localizado na pasta Properties do seu projeto.
Procure a seção "applicationUrl" e adicione a propriedade "httpsPort" com um número de porta válido, por exemplo:

```
"applicationUrl": "https://localhost:5139"
```

Execute:

```
donet run
```

Acesse:

```
http://localhost:5139/swagger/index.html
```
