using BitstampSimulador.Domain.Entities;

namespace BitstampSimulador.Domain.Interfaces
{
    public interface IServicoSimulacao
    {
        ResultadoSimulacao Simular(string ativo, string tipo, decimal quantidade);
    }

    public class ResultadoSimulacao
    {
        public string Ativo { get; set; }
        public string Tipo { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Total { get; set; }
        public decimal PrecoMedio { get; set; }
    }
}