using ECOral___Relatorios.Models;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace ECOral___Relatorios.Controllers
{
    public class RelatorioMap : ClassMap<Relatorio>
    {
        public RelatorioMap()
        {
            // Mapa do CSV
            Map(m => m.Fator).Name("Fator");
            Map(m => m.Meses).Name("Meses");
            Map(m => m.Ano).Name("Ano");
            Map(m => m.Quantidade).Name("Quantidade");
            Map(m => m.UndMedida).Name("Und_Medida");
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RelatorioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<List<Relatorio>> GetRelatorios()
        {
            string connectionString = _configuration.GetConnectionString("OracleDbConnection");

            List<Relatorio> relatorios = new List<Relatorio>();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand("SELECT Fator, Id, Meses, Ano, Quantidade, Und_Medida FROM Relatorio", connection))
                {
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

        [HttpPost]
        public IActionResult AddRelatorio(Relatorio relatorio)
        {
            string connectionString = _configuration.GetConnectionString("OracleDbConnection");

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Relatorio (Fator, Meses, Ano, Quantidade, Und_Medida) VALUES (:Fator, :Meses, :Ano, :Quantidade, :Und_Medida)";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Fator", relatorio.Fator));
                    command.Parameters.Add(new OracleParameter("Meses", relatorio.Meses));
                    command.Parameters.Add(new OracleParameter("Ano", relatorio.Ano));
                    command.Parameters.Add(new OracleParameter("Quantidade", relatorio.Quantidade));
                    command.Parameters.Add(new OracleParameter("Und_Medida", relatorio.UndMedida));

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Falha ao adicionar o relatório.");
                    }
                }
            }
        }

        [HttpPost("import")]
        public IActionResult ImportRelatorios()
        {   
            //Inserir caminho para CSV
            string filePath = "C:\\Users\\ti1_febrapo\\Desktop\\Entregavel_Enterprise_ECOral\\IMPORTAR-DADOS.csv";
            string connectionString = _configuration.GetConnectionString("OracleDbConnection");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";", 
                HasHeaderRecord = true 
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<RelatorioMap>();
                var relatorios = csv.GetRecords<Relatorio>();

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Relatorio (Fator, Meses, Ano, Quantidade, Und_Medida) VALUES (:Fator, :Meses, :Ano, :Quantidade, :Und_Medida)";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        foreach (var relatorio in relatorios)
                        {
                            command.Parameters.Clear();
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

            return Ok("Dados importados com sucesso.");
        }

        [HttpGet("ano/{ano}")]
        public ActionResult<List<Relatorio>> GetRelatoriosPorAno(int ano)
        {
            string connectionString = _configuration.GetConnectionString("OracleDbConnection");

            List<Relatorio> relatorios = new List<Relatorio>();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Fator, Id, Meses, Ano, Quantidade, Und_Medida FROM Relatorio WHERE Ano = :Ano";
                using (OracleCommand command = new OracleCommand(query, connection))
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

        [HttpGet("fator/{fator}")]
        public ActionResult<List<Relatorio>> GetRelatoriosPorFator(string fator)
        {
            string connectionString = _configuration.GetConnectionString("OracleDbConnection");

            List<Relatorio> relatorios = new List<Relatorio>();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Fator, Id, Meses, Ano, Quantidade, Und_Medida FROM Relatorio WHERE Fator = :Fator";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Fator", fator));
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

        [HttpDelete("{id}")]
        public IActionResult DeleteRelatorio(int id)
        {
            string connectionString = _configuration.GetConnectionString("OracleDbConnection");

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Relatorio WHERE Id = :Id";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Id", id));

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Ok("Relatório excluído com sucesso.");
                    }
                    else
                    {
                        return NotFound("Relatório não encontrado.");
                    }
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRelatorio(int id, Relatorio relatorio)
        {
            string connectionString = _configuration.GetConnectionString("OracleDbConnection");

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Relatorio SET Fator = :Fator, Meses = :Meses, Ano = :Ano, Quantidade = :Quantidade, Und_Medida = :Und_Medida WHERE Id = :Id";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("Fator", relatorio.Fator));
                    command.Parameters.Add(new OracleParameter("Meses", relatorio.Meses));
                    command.Parameters.Add(new OracleParameter("Ano", relatorio.Ano));
                    command.Parameters.Add(new OracleParameter("Quantidade", relatorio.Quantidade));
                    command.Parameters.Add(new OracleParameter("Und_Medida", relatorio.UndMedida));
                    command.Parameters.Add(new OracleParameter("Id", id));

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Ok("Relatório atualizado com sucesso.");
                    }
                    else
                    {
                        return NotFound("Relatório não encontrado.");
                    }
                }
            }
        }
    }
}

