using Xunit;
using Moq;
using System.Collections.Generic;
using BitstampSimulador.Domain.Entities;
using BitstampSimulador.Application.Simulacoes;
using BitstampSimulador.Infrastructure;

namespace BitstampSimulador.Tests.Simulacoes
{
    public class ServicoSimulacaoTests
    {
        private readonly ServicoSimulacao _servico;
        private readonly Mock<MongoService> _mongoMock;

        public ServicoSimulacaoTests()
        {
            _mongoMock = new Mock<MongoService>();
            _servico = new ServicoSimulacao(_mongoMock.Object);
        }

        [Fact]
        public void Simular_DeveRetornarResultadoParaCompraValida()
        {
            var snapshot = new OrderBookSnapshot
            {
                Ativo = "BTCUSD",
                Bids = new(),
                Asks = new List<Ordem>
                {
                    new Ordem { Preco = 10000, Quantidade = 1 },
                    new Ordem { Preco = 11000, Quantidade = 1 }
                }
            };

            _mongoMock.Setup(m => m.BuscarUltimoSnapshot("BTCUSD")).Returns(snapshot);

            var resultado = _servico.Simular("BTCUSD", "compra", 1.5m);

            Assert.NotNull(resultado);
            Assert.Equal("BTCUSD", resultado.Ativo);
            Assert.Equal(10500, resultado.PrecoMedio);
        }

        [Fact]
        public void Simular_DeveRetornarResultadoParaVendaValida()
        {
            var snapshot = new OrderBookSnapshot
            {
                Ativo = "BTCUSD",
                Asks = new(),
                Bids = new List<Ordem>
                {
                    new Ordem { Preco = 12000, Quantidade = 1 },
                    new Ordem { Preco = 11000, Quantidade = 1 }
                }
            };

            _mongoMock.Setup(m => m.BuscarUltimoSnapshot("BTCUSD")).Returns(snapshot);

            var resultado = _servico.Simular("BTCUSD", "venda", 1.5m);

            Assert.NotNull(resultado);
            Assert.Equal("BTCUSD", resultado.Ativo);
            Assert.Equal(11500, resultado.PrecoMedio);
        }

        [Fact]
        public void Simular_DeveRetornarNull_TipoInvalido()
        {
            var resultado = _servico.Simular("BTCUSD", "doacao", 1);
            Assert.Null(resultado);
        }

        [Fact]
        public void Simular_DeveRetornarNull_SemSnapshot()
        {
            _mongoMock.Setup(m => m.BuscarUltimoSnapshot("BTCUSD")).Returns((OrderBookSnapshot)null);

            var resultado = _servico.Simular("BTCUSD", "compra", 1);
            Assert.Null(resultado);
        }

        [Fact]
        public void Simular_DeveRetornarNull_QuantidadeMaiorQueDisponivel()
        {
            var snapshot = new OrderBookSnapshot
            {
                Ativo = "BTCUSD",
                Asks = new List<Ordem>
                {
                    new Ordem { Preco = 10000, Quantidade = 0.5m }
                }
            };

            _mongoMock.Setup(m => m.BuscarUltimoSnapshot("BTCUSD")).Returns(snapshot);

            var resultado = _servico.Simular("BTCUSD", "compra", 1);
            Assert.Null(resultado);
        }
    }
}