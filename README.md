# AuthenticationAPI

## Sobre o projeto 

Authentication API é um projeto de autenticação, tem o CRUD de Usuario e função de Login retornando token de autenticação usando JWT.

## Como executar

Pré requisitos **Docker** e **SDK .NET 8.0** (caso queira debugar).

Clone o projeto com: `https://github.com/Matheus-Sleutjes/AuthenticationAPI.git`

Acesse o projeto, e execute: `docker compose up --build -d`

Acesse a URL para a documentação: `http://localhost:8080/swagger/index.html` ou [clique aqui](http://localhost:8080/swagger/index.html) para ser redirecionado para o swagger

<img src="Authentication.API/swagger.png" alt="swagger">

Pronto! Agora é só se divertir!

## Considerações

Caso queira rodar em modo Debug voce pode executar `docker run --name db-auth -e POSTGRES_PASSWORD=postgres -p 5000:5432 -d postgres` para subir uma instância de BD, ou criar um banco em Postgres baseado na string de conexão `Host=localhost;Database=db-auth;Port=5000;Username=postgres;Password=postgres;CommandTimeout=120;`

Feito com carinho por Matheus Sleutjes
