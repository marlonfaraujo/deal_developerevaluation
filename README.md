# Descrição do Projeto

Minimal API em .NET 8 e SPA em Angular versão 19.

# Início

## Instruções de configuração do ambiente local
 
Para executar o projeto, você deve ter o seguinte instalado em seu ambiente local:

* **Node.js** para comandos npm ou npx ([link](https://nodejs.org/pt/blog/release/v22.11.0))
* **Git** para clonar o repositório ([link](https://git-scm.com/downloads))
* **.NET-SDK-8** para execução de aplicativos e desenvolvimento local ([link](https://dotnet.microsoft.com/en-us/download/dotnet/8.0))

## Configurações para executar pelo docker

* **Docker** para conteinerizar a aplicação ([link](https://docs.docker.com/engine/install/))
* **docker-compose** caso a instalação do docker não o instale automaticamente ([link](https://docs.docker.com/compose/install/))


## Frameworks

* **Entity Framework Core 8** [link](https://learn.microsoft.com/en-us/ef/core/)
* **Fluent Validation 11.10** [link](https://docs.fluentvalidation.net/en/latest/)
* **XUnit 2.5** [link](https://xunit.net/)
* **Bogus 35.6** [link](https://github.com/bchavez/Bogus)
* **PO UI 19.32** [link](https://github.com/po-ui/po-angular)
* **Dev Container 0.427** [link](https://containers.dev/)
* **Swagger (Swashbuckle)** [link](https://swagger.io/)

Pode ser necessário reiniciar o sistema após as instalações

## Instalação e execução

Para executar a aplicação, você precisa seguir os passos abaixo:
* Execute o comando abaixo no terminal para clonar o repositório:
```bash
 git clone https://github.com/marlonfaraujo/deal_developerevaluation.git
```
* Comando para acessar a pasta no terminal, exemplo: 
```bash
cd deal_developerevaluation
```

* Para execução das aplicações pelo docker execute o comando abaixo: 

```bash
docker-compose up -d
```

Comando para executar as 2 aplicações localmente:

```bash
dotnet run --project .\backend\src\Deal.DeveloperEvaluation.WebApi\
```

```bash
ng serve --port 4200 ou npx ng serve --port 4200
```

 **Execução dos testes**

```bash
dotnet test .\backend\Deal.DeveloperEvaluation.sln
```

# Documentação
Acesse a documentação e utilize as APIs.

Link: http://localhost:8080/swagger
