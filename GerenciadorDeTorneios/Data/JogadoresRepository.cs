using Microsoft.Data.SqlClient;
using GerenciadorDeTorneios.Models;

namespace GerenciadorDeTorneios.Data
{
    public class JogadoresRepository
    {
        // guarda a string de conexão passada pelo App.Config
        private readonly string _connString;


        // construtor que recebe a connection string
        public JogadoresRepository(string connString)
        {
            _connString = connString;
        }

        // método para buscar todos os jogadores no banco de dados
        public List<Jogadores> GetAll()
        {
            // lista a ser preenchida com os jogadores encontrados no banco
            var lista = new List<Jogadores>();
            // using garante que a conexão será fechada e descartada corretamente
            using (var conn = new SqlConnection(_connString))
            {
                // abre a conexão com o SQL Server
                conn.Open();
                // criando o comando SQL que será enviado ao banco
                var cmd = new SqlCommand("SELECT Id, Nome, Posicao, Numero, TimeId FROM Jogadores", conn);
                // executa o comando SQL e armazena o resultado em DataReader
                var reader = cmd.ExecuteReader();
                // faz um loop por cada linha retornada na consulta SQL
                while (reader.Read())
                {
                    // para cada linha do banco, criamos um novo objeto Jogadores
                    lista.Add(new Jogadores
                    {
                        // aloca em cada propriedade o valor vindo da coluna 
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Posicao = reader.GetString(2),
                        Numero = reader.GetInt32(3),
                        TimeId = reader.GetInt32(4)
                    });
                }
            }
            return lista;
        }

        // adicionando os dados criados em MainWindowViewModel no banco de dados SQL
        public void JogadoresRepositoryAdd(Jogadores jogador)
        {
            // abrindo a conexão
            using (var conn = new SqlConnection(_connString))
            {
                // abrindo a conexão com SQL Server
                conn.Open();

                // criando comando para adicionar os jogadores no banco de dados
                var cmd = new SqlCommand("INSERT INTO Jogadores (Nome, Posicao, Numero, TimeId) VALUES (@Nome, @Posicao, @Numero, @TimeId)", conn);

                // preenchendo os parâmetros com os valores do objeto Jogadores
                cmd.Parameters.AddWithValue("@Nome", jogador.Nome);
                cmd.Parameters.AddWithValue("@Posicao", jogador.Posicao);
                cmd.Parameters.AddWithValue("@Numero", jogador.Numero);
                cmd.Parameters.AddWithValue("@TimeId", jogador.TimeId);

                // executando o comando SQL para rodar o insertr
                cmd.ExecuteNonQuery();
            }
        }

        // método que retorna jogadores por time, visto que queremos retornar dentro da MainWindow os jogadores por time
        // seria o equivalente a fazer um SELECT * FROM Jogadores WHERE TimeId = @TimeId
        public List<Jogadores> GetByTimeId(int timeId)
        {
            var lista = new List<Jogadores>();

            using (var conn = new SqlConnection(_connString))
            {
                conn.Open();

                var cmd = new SqlCommand(
                    "SELECT Id, Nome, Posicao, Numero, TimeId FROM Jogadores WHERE TimeId = @TimeId", conn
                    );

                cmd.Parameters.AddWithValue("@TimeId", timeId);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Jogadores()
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Posicao = reader.GetString(2),
                        Numero = reader.GetInt32(3),
                        TimeId = reader.GetInt32(4)
                    });
                }
            }
            return lista;

        }
    }
}