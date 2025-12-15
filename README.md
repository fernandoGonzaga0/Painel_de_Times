# Painel de Times usando C# e SQL Server

<h4>Aplicação WPF usando C# que se conecta de forma assíncrona com um banco de dados SQL.</h4>

<hr>

<p>Esse projeto é um exemplo que utilizei para entender melhor como trazer dados de um banco de dados para uma aplicação real em dispositivos Windows. Para estruturação do projeto, usei MVVM (Model-View-ViewModel).</p>

## Objetivo do projeto

<p>Demonstrar como:</p>

  - Conectar uma aplicação WPF a um banco de dados SQL Server
  - Implementar o padrão MVVM em um projeto real
  - Trabalhar com a abordagem Database First
  - Exibir dados relacionais (Times -> Jogadores) em interface gráfica

## Arquitetura do Projeto

<p>O projeto segue a metodologia Database First, onde criei primeiramente a estrutura do banco relacional pelo SQL Server Management (para acessar o script de criação, acesse Scrips -> Sql_createTables.sql). A estrutura de pastas ficou assim: </p>

```
GerenciadorDeTorneios/
├── Models/                    # Entidades do domínio
│   ├── Time.cs               # Classe Time com propriedades e lista de jogadores
│   └── Jogadores.cs          # Classe Jogador com propriedades básicas
│
├── Data/                     # Camada de acesso a dados
│   ├── TimeRepository.cs     # Operações CRUD para Time
│   └── JogadoresRepository.cs # Operações CRUD para Jogadores
│
├── ViewModel/                # Camada de lógica de apresentação
│   └── MainWindowViewModel.cs # ViewModel principal com binding e commands
│
├── Views/                    # Interface do usuário
│   └── MainWindow.xaml      # Tela principal com listagem de times e jogadores
│
├── App.xaml                  # Ponto de entrada da aplicação
└── App.config               # Configurações (connection string)
```
## Banco de Dados

<p>No banco de dados existem apenas duas tabelas: Time e Jogadores, relacionadas em 1:N (um time pode ter vários jogadores). Essas tabelas são conectadas por uma chave estrangeira, onde Jogadores.TimeId referencia Time.Id:</p>


  
