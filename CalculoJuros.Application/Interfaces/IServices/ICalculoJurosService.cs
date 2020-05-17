using System.Threading.Tasks;

namespace CalculoJuros.Application.Interfaces.IServices
{
    public interface ICalculoJurosService
    {
        double CalcularJurosCompostos(double valorInicial, int meses, double taxa);

        Task<double> ObterTaxaJurosAsync();

        string FormatarValorDoubleParaDuasCasasDecimais(double valor);
    }
}
