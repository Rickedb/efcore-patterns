# EFCore Patterns

O pattern mais utilizado costuma ser o Repository, no entanto, de acordo com a documentação do EF o proprio `DbContext` já é uma abstração de UoW e Repository pattern:

> Summary:
A DbContext instance represents a session with the database and can be used to query and save instances of your entities. DbContext is a combination of the Unit Of Work and Repository patterns.

Portanto, o Repository pattern acaba não tendo tanto valor quanto quando usamos `SqlConnection` diretamente, 
a não ser o de separar as queries do código e a pergunta que fica é: **qual a melhor alternativa usando EF?**


Pra isso, esse repositório deve conter alguns estudos experimentais para entender 
**quais casos seriam** melhor determinado pattern.

A ideia de uma classe como "Repository" ainda é muito presente em todos as maneiras, já que primeiramente o `DbSet<TEntity>` não pode ser herdado e também
[não é aconselhado](https://github.com/dotnet/efcore/issues/12422) pelos próprios desenvolvedores do EntityFramework e que não possuem planos pra isso pois
os ganhos em virtude da complexidade alteração são mínimos.

Vamos ver se chegamos à alguma conclusão... ¯\＿(ツ)＿/¯

## RepositoryPattern

O padrão que todos conhecem que recebe o Context e trabalha as queries em cima desse contexto.

O exemplo em `LazyWeatherForecastRepository` sugere uma abordagem sem a necessidade de usar o repository dentro do DI,
efetuando um lazy loading "na mão" e usando a propriedade que seria um DbSet, para ser o próprio repositório criado.
No entanto, ainda assim a API do DbSet se torna acessível por conta do método `Set<TEntity>()` do próprio `DbContext`.

## DbExtensionPattern

Basicamente se firma em métodos de extensão que vão extender o comportamento do DbSet, o que seria análogo à um repository, mas são acessíveis diretamente
pelo DbSet do contexto, facilitando o consumo e dando uma abordagem mais como `context.Forecasts.InRange(startAt, endAt);`.

## DbSetPattern

Atua como um [Proxy](https://refactoring.guru/design-patterns/proxy) ou [Decorator](https://refactoring.guru/design-patterns/decorator) basicamente,
podendo extender o comportamento e atuar como um Repository, mas usando sempre o DbSet ao invés do Context como seria em um Repository convencional.

A parte ruim é que existe um overhead muito grande e uma classe dedicada a ser um "one-liner" para que todas as interfaces e implementações
possam ser "redirecionadas" para o DbSet original.

# Conclusões

> Esta seção pode mudar ao longo do tempo com base nos testes e abordagens novas ou atualizações no EF

Não existe muito uma fuga do que conhecemos como Repository Pattern, sempre será necessário uma classe que receba um Context ou DbSet de uma entidade 
e ela fará a interação com eles. Conseguir jogar como se fosse um DbSet dentro do Context pode ser uma abordagem legal caso você não queira
ficar injetando dezenas de classe diferentes no seu DI.

Isso é possível seja com uma abordagem de repository ou do que foi nomeado com "DbSetPattern".