using ECOral___Relatorios.Models;
using Oracle.ManagedDataAccess.Client;

namespace ECOral___Relatorios.Services
{
    public class RelatorioService
    {
        private readonly string _connectionString;

        public RelatorioService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("OracleDbConnection");
        }

        private readonly IConfiguration _configuration;

        public List<Relatorio> GetRelatoriosPorMes(int ano)
        {
            List<Relatorio> relatorios = new List<Relatorio>();

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand("SELECT Fator, Id, Meses, Ano, Quantidade, Und_Medida FROM Relatorio WHERE Ano = :Ano", connection))
                {
                    command.Parameters.Add(new OracleParameter("Ano", ano));
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Relatorio relatorio = new Relatorio
                            {
                                Fator = reader["Fator"].ToString(),
                                Id = Convert.ToInt32(reader["Id"]),
                                Meses = reader["Meses"].ToString(),
                                Ano = Convert.ToInt32(reader["Ano"]),
                                Quantidade = Convert.ToInt32(reader["Quantidade"]),
                                UndMedida = reader["Und_Medida"].ToString()
                            };
                            relatorios.Add(relatorio);
                        }
                    }
                }
            }

            return relatorios;
        }

        public void AddRelatorio(Relatorio relatorio)
        {
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand("INSERT INTO Relatorio (Fator, Meses, Ano, Quantidade, Und_Medida) VALUES (:Fator, :Meses, :Ano, :Quantidade, :Und_Medida)", connection))
                {
                    command.Parameters.Add(new OracleParameter("Fator", relatorio.Fator));
                    command.Parameters.Add(new OracleParameter("Meses", relatorio.Meses));
                    command.Parameters.Add(new OracleParameter("Ano", relatorio.Ano));
                    command.Parameters.Add(new OracleParameter("Quantidade", relatorio.Quantidade));
                    command.Parameters.Add(new OracleParameter("Und_Medida", relatorio.UndMedida));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
