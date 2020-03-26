using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factory
{
    /// <summary>
    /// Clase con la Responsabilidad de Crear Cuentan Bancaria de un Tipo Especifico (Ahorro o Corrriente)
    /// </summary>
    public class CuentaBancancariaFactory
    {
        public CuentaBancaria Create(string tipoCuenta) 
        {
            CuentaBancaria cuentaNueva;
            if (tipoCuenta == "Ahorro")
            {
                cuentaNueva = new CuentaAhorro();
            }
            else
            {
                cuentaNueva = new CuentaCorriente();
            }
            return cuentaNueva;
        }
    }
}
