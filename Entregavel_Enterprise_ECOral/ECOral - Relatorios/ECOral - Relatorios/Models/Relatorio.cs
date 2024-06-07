namespace ECOral___Relatorios.Models
{
    public class Relatorio
    {
        public string Fator { get; set; }
        public int Id { get; set; }
        public string Meses { get; set; }
        public int Ano { get; set; }
        public float Quantidade { get; set; }
        public string UndMedida { get; set; }
 

        public Relatorio()
        {
            Fator = string.Empty;
            Meses = string.Empty;
            UndMedida = string.Empty;
        }
    }
}
