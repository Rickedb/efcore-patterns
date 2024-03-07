# EFCore Patterns

O pattern mais utilizado costuma ser o Repository, no entanto, de acordo com a documentação do EF o proprio `DbContext` já é uma abstração de UoW e Repository pattern:

> Summary:
A DbContext instance represents a session with the database and can be used to query and save instances of your entities. DbContext is a combination of the Unit Of Work and Repository patterns.

Portanto, o Repository pattern acaba não tendo tanto valor quanto quando usamos `SqlConnection` diretamente, 
a não ser o de separar as queries do código e a pergunta que fica é: **qual a melhor alternativa usando EF?**


Pra isso, esse repositório deve conter alguns estudos experimentais para entender 
**quais casos seriam** melhor determinado pattern.

Vamos ver se chegamos à alguma conclusão... ¯\＿(ツ)＿/¯