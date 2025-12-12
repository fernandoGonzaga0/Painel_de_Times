using GerenciadorDeTorneios.Data;
using GerenciadorDeTorneios.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;

// Esse ViewModel é o intermediário entre a tela (XAML) e os dados (antes eram nos Models + Repositórios, agora é no próprio SQL Server)

namespace GerenciadorDeTorneios.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        // lista de times * como já adicionamos os dados no banco de dados, a lista pode ser comentada para facilitar a busca dos objetos diretamente no banco de dados visando a exibição em XAML.
        /*
           List<Time> times = new()
        {
            new Time {Nome = "Corinthians"}, // time 1
            new Time {Nome = "São Paulo"}, // time 2
            new Time {Nome = "Santos"}, // time 3
            new Time {Nome = "Vasco"}, // time 4
            new Time {Nome = "Grêmio"} // time 5
        };
        */

        // lista de jogadores * como já adicionamos os dados no banco de dados, a lista pode ser comentada para facilitar a busca dos objetos diretamente no banco de dados visando a exibição em XAML.
        /*
             List<Jogadores> jogadores = new()
            {
                // jogadores time 1
                new Jogadores {Nome = "Garro", Posicao = "Meio campo", Numero = 8, TimeId = 1},
                new Jogadores {Nome = "Hugo", Posicao = "Goleiro", Numero = 1, TimeId = 1},
                new Jogadores {Nome = "Lucas", Posicao = "Zagueiro", Numero = 3, TimeId = 1},
                new Jogadores {Nome = "Yuri", Posicao = "Atacante", Numero = 9, TimeId = 1},
                new Jogadores {Nome = "Guilherme", Posicao = "Meio campo", Numero = 6, TimeId = 1},

                // jogadores time 2
                new Jogadores {Nome = "Renato", Posicao = "Goleiro", Numero = 1, TimeId = 2},
                new Jogadores {Nome = "Thiago", Posicao = "Zagueiro", Numero = 4, TimeId = 2},
                new Jogadores {Nome = "Bruno", Posicao = "Lateral", Numero = 2, TimeId = 2},
                new Jogadores {Nome = "André", Posicao = "Meio campo", Numero = 7, TimeId = 2},
                new Jogadores {Nome = "Felipe", Posicao = "Atacante", Numero = 9, TimeId = 2},

                // jogadores time 3
                new Jogadores {Nome = "Marcelo", Posicao = "Goleiro", Numero = 1, TimeId = 3},
                new Jogadores {Nome = "Eduardo", Posicao = "Zagueiro", Numero = 3, TimeId = 3},
                new Jogadores {Nome = "Rafael", Posicao = "Lateral", Numero = 5, TimeId = 3},
                new Jogadores {Nome = "Diego", Posicao = "Meio campo", Numero = 8, TimeId = 3},
                new Jogadores {Nome = "Leandro", Posicao = "Atacante", Numero = 9, TimeId = 3},

                // jogadores time 4
                new Jogadores {Nome = "Pedro", Posicao = "Goleiro", Numero = 1, TimeId = 4},
                new Jogadores {Nome = "João", Posicao = "Zagueiro", Numero = 2, TimeId = 4},
                new Jogadores {Nome = "Mateus", Posicao = "Lateral", Numero = 6, TimeId = 4},
                new Jogadores {Nome = "Henrique", Posicao = "Meio campo", Numero = 10, TimeId = 4},
                new Jogadores {Nome = "Caio", Posicao = "Atacante", Numero = 9, TimeId = 4},

                // jogadores time 5
                new Jogadores {Nome = "Samuel", Posicao = "Goleiro", Numero = 1, TimeId = 5},
                new Jogadores {Nome = "Victor", Posicao = "Zagueiro", Numero = 3, TimeId = 5},
                new Jogadores {Nome = "Daniel", Posicao = "Lateral", Numero = 4, TimeId = 5},
                new Jogadores {Nome = "Alex", Posicao = "Meio campo", Numero = 7, TimeId = 5},
                new Jogadores {Nome = "Rodrigo", Posicao = "Atacante", Numero = 9, TimeId = 5},

            };
        */
        
        // lista de jogadores e times para XAML
        public ObservableCollection<Jogadores> JogadoresDoTime { get; set; } = new();
        public ObservableCollection<Time> Times { get; set; } = new();

        // repositórios (responsáveis por abrir a conexão com o banco, fazer SELECT/INSERT e transformar os dados SQL em objetos C#
        private readonly TimeRepository _timeRepo;
        private readonly JogadoresRepository _jogadoresRepo;

        // time selecionado para inserção no XAML
        private Time? _timeSelecionado;
        public Time? TimeSelecionado
        {
            get => _timeSelecionado;
            set
            {
                _timeSelecionado = value;
                CarregarJogadoresDoTime();
                OnPropertyChanged(nameof(TimeSelecionado));
            }
        }

        public MainWindowViewModel()
        {
            // usando a connection string do App.Config
            string connString = ConfigurationManager.ConnectionStrings["GerenciadorDeTorneios"].ConnectionString;

            // criando os repositórios usando essa connection string
            _timeRepo = new TimeRepository(connString);
            _jogadoresRepo = new JogadoresRepository(connString);

            // gravando os dados no banco de dados * métodos abaixo comentados pois já salvei os dados no banco de dados
            // SalvarTimesNoBanco();
            // SalvarJogadoresNoBanco();

            

            CarregarTimes(); // os times serão carregados automaticamente após a execução desse método

        }

        // classe para salvar times no banco de dados * método comentado pois já salvamos os objetos no banco de dados
        /*
        private Dictionary<string, int> mapaIds = new();
        private void SalvarTimesNoBanco()
        {
            foreach (var time in times)
            {
                int idGerado = _timeRepo.TimeRepositoryAdd(time); // insere os dados no SQL
                mapaIds[time.Nome] = idGerado; // salvando o Id em um mapa
            }
        }         
        */

        // classe para salvar jogadores no banco de dados * método comentado pois já salvamos os objetos no banco de dados
        /*
        private void SalvarJogadoresNoBanco()
        {
            foreach (var jogador in jogadores)
            { 
                string nomeDoTime = times[jogador.TimeId - 1].Nome;
                int idReal = mapaIds[nomeDoTime];

                jogador.TimeId = idReal;

                _jogadoresRepo.JogadoresRepositoryAdd(jogador);
            }
        }         
        */

        // classe para carregar times 
        private void CarregarTimes()
        {
            Times.Clear();

            foreach (var t in _timeRepo.GetAll())
            {
                // carregando os jogadores de cada time
                t.Jogadores = _jogadoresRepo.GetByTimeId(t.Id);
                Times.Add(t);
            }
        }
        
        // classe para carregar jogadores dos times
        private void CarregarJogadoresDoTime()
        {
            if (TimeSelecionado == null) return;

            JogadoresDoTime.Clear();

            foreach (var j in _jogadoresRepo.GetByTimeId(TimeSelecionado.Id))
            {
                JogadoresDoTime.Add(j);
            }
        }

        // usando PropertyChanged para atualizar o WPF automaticamente caso a propriedade mude no banco de dados 
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}