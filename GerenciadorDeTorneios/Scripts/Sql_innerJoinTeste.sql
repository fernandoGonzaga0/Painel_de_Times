SELECT * FROM Time
SELECT * FROM Jogadores

SELECT Time.Nome, Jogadores.Nome, Jogadores.Posicao, Jogadores.Numero 
FROM GerenciadorDeTorneios.dbo.Time
INNER JOIN GerenciadorDeTorneios.dbo.Jogadores on GerenciadorDeTorneios.dbo.Time.Id = GerenciadorDeTorneios.dbo.Jogadores.TimeId