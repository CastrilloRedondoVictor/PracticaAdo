﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaAdo.Models
{
    public class Cliente
    {
        public string CodigoCliente { get; set; }
        public string Empresa { get; set; }
        public string Contacto { get; set; }
        public string Cargo { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }

        public Cliente(string codigoCliente, string empresa, string contacto, string cargo, string ciudad, string telefono)
        {
            CodigoCliente = codigoCliente;
            Empresa = empresa;
            Contacto = contacto;
            Cargo = cargo;
            Ciudad = ciudad;
            Telefono = telefono;
        }
    }

}
