using Domain.Entities;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConsignacionAhorroTest()
        {
            var cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Consignar(10000);
            Assert.AreEqual(10000, cuenta.Saldo);
        }

        [Test]
        [TestCase("0101", "Cuenta #1", 100000, 100000, TestName = "Consignacion de 100000")]
        [TestCase("0102", "Cuenta #2", 200000, 200000, TestName = "Consignacion de 200000")]
        [TestCase("0103", "Cuenta #3", 300000, 300000, TestName = "Consignacion de 300000")]
        [TestCase("0104", "Cuenta #4", 400000, 400000, TestName = "Consignacion de 400000")]
        public void ConsignacionExitosaAhorroTest(string numeroCuenta, string nombreCuenta, double valorConsignar, double saldoEsperado)
        {
            var cuenta = new CuentaAhorro
            {
                Numero = numeroCuenta,
                Nombre = nombreCuenta
            };
            cuenta.Consignar(valorConsignar);
            Assert.AreEqual(saldoEsperado, cuenta.Saldo);
        }
        #region TestCaseSource
        [TestCaseSource("DataSourceConsignacion")]
        public void ConsignacionExitosaAhorroTestWithCaseSource(string numeroCuenta, string nombreCuenta, double valorConsignar, double saldoEsperado)
        {
            var cuenta = new CuentaAhorro
            {
                Numero = numeroCuenta,
                Nombre = nombreCuenta
            };
            cuenta.Consignar(valorConsignar);
            Assert.AreEqual(saldoEsperado, cuenta.Saldo);
        }
        private static IEnumerable<TestCaseData> DataSourceConsignacion()
        {
            //ustedes pueder leer un archivo de excel, csv o base de datos y retornar la infomación
            yield return new TestCaseData("0101", "Cuenta #1", 100000, 100000).SetName("Data source Consignacion de 100000");
            yield return new TestCaseData("0102", "Cuenta #2", 200000, 200000).SetName("Data source Consignacion de 200000");
            yield return new TestCaseData("0103", "Cuenta #3", 300000, 300000).SetName("Data source Consignacion de 300000");
            yield return new TestCaseData("0104", "Cuenta #4", 400000, 400000).SetName("Data source Consignacion de 400000");
        }
        #endregion

        #region EjemploDeUsoAdicionalYielReturn
        [Test]
        public void ConvertToUpper()
        {
            foreach (var palabra in DataSourceConvertToUpper())
            {
                TestContext.WriteLine(palabra.TestName);
            }
        }

        private static IEnumerable<TestCaseData> DataSourceConvertToUpper()
        {
            string[] palabras = new string[] { "unicesar", "clases", "ddp" };
            foreach (var item in palabras)
            {
                yield return new TestCaseData(item).SetName($"Data source {item}");
            }
        }
        #endregion EjemploDeUsoAdicionalYielReturn

        
        [Test, TestCaseSource(typeof(DataSourceConsignacionWithObject), "CasosDePrueba")]
        public void ConsignacionExitosaAhorroTestWithCaseSource(CaseDataSourceConsignacionRequest request, CaseDataSourceConsignacionExpected expected)
        {
            var cuenta = new CuentaAhorro
            {
                Numero = request.NumeroCuenta,
                Nombre = request.NombreCuenta
            };
            cuenta.Consignar(request.ValorConsignar);
            Assert.AreEqual(expected.SaldoEsperado, cuenta.Saldo);
        }

        public class DataSourceConsignacionWithObject
        {
            protected DataSourceConsignacionWithObject() { }
            public static IEnumerable CasosDePrueba
            {
                get
                {
                    var testcase1 = CreateTestCaseDataSourceConsignacion1();
                    yield return new TestCaseData(testcase1.Request, testcase1.Expected).SetName("Set Data source Consignacion de 100000");
                }
            }
        }
     
        #region CrearTestCase1
        private static TestCaseDataSourceConsignacion CreateTestCaseDataSourceConsignacion1()
        {
            var testCase = new TestCaseDataSourceConsignacion();
            testCase.Request.NombreCuenta = "Cuenta 1";
            testCase.Request.NumeroCuenta = "0101";
            testCase.Request.ValorConsignar = 2000000;
            testCase.Expected.SaldoEsperado = 2000000;
            return testCase;
        }
        #endregion

        #region CrearTestCase2
        private static TestCaseDataSourceConsignacion CreateTestCaseDataSourceConsignacion2()
        {
            var testCase = new TestCaseDataSourceConsignacion();
            testCase.Request.NombreCuenta = "Cuenta 2";
            testCase.Request.NumeroCuenta = "0102";
            testCase.Request.ValorConsignar = 1000000;
            testCase.Expected.SaldoEsperado = 1000000;
            return testCase;
        }
        #endregion
    }




    #region Estructura de Datos de Entrada y Salida de las Pruebas
    public class TestCaseDataSourceConsignacion
    {
        public TestCaseDataSourceConsignacion()
        {
            Request = new CaseDataSourceConsignacionRequest();
            Expected = new CaseDataSourceConsignacionExpected();
        }
        public CaseDataSourceConsignacionRequest Request { get; set; }
        public CaseDataSourceConsignacionExpected Expected { get; set; }
    }

    public class CaseDataSourceConsignacionRequest
    {
        public string NumeroCuenta { get; set; }
        public string NombreCuenta { get; set; }
        public double ValorConsignar { get; set; }
    }

    public class CaseDataSourceConsignacionExpected
    {
        public double SaldoEsperado { get; set; }
    }
    #endregion 
}