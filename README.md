# Desafio Ganho de Capital

Este projeto implementa uma solução para o desafio de cálculo do imposto sobre Ganho de Capital. O programa processa operações de compra e venda de ações, calculando o imposto devido com base no preço médio ponderado, prejuízo acumulado e regras de isenção.

## Decisões Técnicas e Arquiteturais

*   **Linguagem de Programação:** C#.
*   **Abordagem:** O programa é projetado como uma aplicação de linha de comando (CLI) que recebe operações de compra e venda no formato de entrada padrão (stdin) e retorna os impostos calculados através da saída padrão (stdout).
*   **Classe `TaxCalculator`:** Esta classe é o núcleo da solução, responsável por:
    *   Manter o estado interno (preço médio, prejuízo, quantidade de ações) *em memória* durante o processamento de cada conjunto de operações.
    *   Calcular o imposto para cada operação, seguindo as regras fornecidas.
    *   Reinicializar o estado interno para cada novo conjunto de operações.
*   **Classe `OpertionsProcessor`:** Responsável por receber o input, passar o input para o TaxCalculator e escrever o resultado;
*   **Sem Persistência:** O programa **não** utiliza nenhum tipo de persistência (banco de dados, arquivos, etc.). Cada conjunto de operações é tratado de forma isolada.

## Justificativa para o Uso de Frameworks ou Bibliotecas

*   O projeto foi desenvolvido sem o uso de frameworks ou bibliotecas externas, além das bibliotecas padrão do .NET.
*   Para os testes foram utilizados os pacotes AutoBogus, FluentAssertions e XUnit  
*   Esta decisão foi tomada para manter a simplicidade e o foco na lógica central do cálculo do imposto, conforme recomendado nas diretrizes do desafio.

## Instruções para Compilar e Executar o Projeto

1.  **Pré-requisitos:**
    *   .NET SDK versão 8.0 instalado.

2.  **Compilação:**
    *   Abra o terminal ou prompt de comando.
    *   Navegue até o diretório raiz do projeto.
    *   Execute o comando: `dotnet build`

3.  **Execução:**
    *   Após a compilação, execute o comando: `dotnet run`
    *   O programa irá aguardar a entrada de operações no formato stdin.
    *   A saída será uma lista de impostos no formato do stdout.

    **Para executar continuamente até que o usuário pressione 'Q':**
        * Depois de rodar o comando dotnet run
        * Escreva as operações
        * Para sair do programa, escreva Q e aperte enter


## Instruções para Executar os Testes da Solução

*   
    1.  Navegue até o diretório do projeto de testes.
    2.  Execute o comando: `dotnet test`
