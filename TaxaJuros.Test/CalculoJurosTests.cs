using CalculoJuros.Application.Interfaces.IServices;
using CalculoJuros.Application.Services;
using NUnit.Framework;

namespace TaxaJuros.Test
{
    public class CalculoJurosTests
    {
        private ICalculoJurosService _calculoJurosService;

        [SetUp]
        public void Setup()
        {
            _calculoJurosService = new CalculoJurosService();
        }

        [Test]
        public void CalcularTaxaDeJurosRetornandoResultadoCorreto()
        {
            var resultado = _calculoJurosService.CalcularJurosCompostos(100, 5, 0.01);
            Assert.AreEqual(105.10100501000001, resultado);
        }

        [Test]
        public void CalcularTaxaDeJurosRetornandoResultadoInvalido()
        {
            var resultado = _calculoJurosService.CalcularJurosCompostos(100, 5, 0.1);

            Assert.AreNotEqual(105.10, resultado);
        }

        [Test]
        public void TestarFormatarDoubleParaDecimal()
        {
            double valor = 105.10100501000001;

            var resultado = _calculoJurosService.FormatarValorDoubleParaDuasCasasDecimais(valor);

            Assert.AreEqual("105,10", resultado);
        }

        [Test]
        public void TestarFormatarDoubleParaDecimalComVirgula()
        {
            double valor = 105.10100501000001;

            var resultado = _calculoJurosService.FormatarValorDoubleParaDuasCasasDecimais(valor);

            Assert.IsTrue(resultado.Contains(","));
        }

        [Test]
        public void TestarFormatarDoubleParaDecimalComValorZero()
        {
            double valor = 0.00;

            var resultado = _calculoJurosService.FormatarValorDoubleParaDuasCasasDecimais(valor);

            Assert.AreEqual("0,00", resultado);
        }


        [Test]
        public void TestarConsultaDaTaxaDeJurosComApiOffline()
        {
            var resultado = _calculoJurosService.ObterTaxaJurosAsync().Result;

            Assert.AreEqual(resultado, 0);
        }


    }
}