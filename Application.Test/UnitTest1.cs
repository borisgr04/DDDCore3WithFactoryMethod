using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Application.Test
{
    public class CuentasBancariasTests
    {
        BancoContext _context;

        [SetUp]
        public void Setup()
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/

            var optionsInMemory = new DbContextOptionsBuilder<BancoContext>().UseInMemoryDatabase("Banco").Options;

            _context = new BancoContext(optionsInMemory);
        }

        [Test]
        public void CrearCuentaBancariaAhorroTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "1120", Nombre = "aaaaa", TipoCuenta = "Ahorro" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual("Se creó con éxito la cuenta 1120.", response.Mensaje);
            Assert.AreEqual(request.TipoCuenta, response.TipoCuentaCreado);
        }

        [Test]
        public void CrearCuentaBancariaCorrienteTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "1121", Nombre = "aaaaa", TipoCuenta = "Corriente" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual("Se creó con éxito la cuenta 1121.", response.Mensaje);
            Assert.AreEqual(request.TipoCuenta, response.TipoCuentaCreado);
        }

        [Test]
        public void CrearConsignarCuentaAhorroTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "1111", Nombre = "aaaaa", TipoCuenta = "Ahorro" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual("Se creó con éxito la cuenta 1111.", response.Mensaje);

            var requestConsignar = new ConsignarRequest { NumeroCuenta= "1111",  Valor = 100000 };
            var serviceConsignar = new ConsignarService(new UnitOfWork(_context));
            var responseConsignar = serviceConsignar.Ejecutar(requestConsignar);
            Assert.AreEqual($"Su Nuevo saldo es {requestConsignar.Valor}.", responseConsignar.Mensaje);
        }

        [Test]
        public void CrearConsignarCuentaCorrienteTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "2111", Nombre = "aaaaa", TipoCuenta = "Corriente" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual("Se creó con éxito la cuenta 2111.", response.Mensaje);

            var requestConsignar = new ConsignarRequest { NumeroCuenta = "2111", Valor = 100000 };
            var serviceConsignar = new ConsignarService(new UnitOfWork(_context));
            var responseConsignar = serviceConsignar.Ejecutar(requestConsignar);
            Assert.AreEqual($"Su Nuevo saldo es {requestConsignar.Valor}.", responseConsignar.Mensaje);
        }
    }
}