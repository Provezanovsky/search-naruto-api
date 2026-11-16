# SearchNarutoAPI · Arquivo Shinobi

Aplicação **Blazor WebAssembly** em **.NET 10**, escrita em C# e executada no navegador.

- Lista os personagens da Dattebayo API com paginação e busca por nome.
- Exibe nome, imagem, clã, afiliação, patente ninja e jutsus. Campos ausentes são identificados na interface.
- Abre a ficha por ID com a lista completa dos jutsus retornados.
- Tenta as imagens da API em ordem. Se nenhuma carregar, informa que a imagem está indisponível.

## Como executar

Com o [SDK do .NET 10](https://dotnet.microsoft.com/download/dotnet/10.0) instalado, rode na pasta do projeto:

```sh
dotnet restore
dotnet run --launch-profile http
```

Abra http://localhost:5186. Para compilar no modo Release:

```sh
dotnet build -c Release
```

## API

- [Documentação da Dattebayo API](https://api-dattebayo.vercel.app/docs)
- Listagem e busca: `GET /characters?page=1&limit=9&name=Sakura` (omita `name` para listar todos).
- Ficha: `GET /characters/{id}`.

O catálogo consulta a API pública diretamente do navegador e precisa de internet. Nas fichas, rótulos da API como `Part I` e os nomes originais dos jutsus são preservados.

Rafael Provezano · Engenharia de Software
