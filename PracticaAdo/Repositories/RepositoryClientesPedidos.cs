using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdoNetCorePractica.Helpers;
using Microsoft.Data.SqlClient;
using PracticaAdo.Models;

#region PROCEDIMIENTOS ALMACENADOS
//create procedure SP_ALL_EMPRESAS
//as
//	select DISTINCT(Empresa) from CLIENTES
//go

//create procedure SP_CLIENTES_EMPRESA
//(@empresa nvarchar(50))
//as
//	select * from CLIENTES where Empresa = @empresa
//go

//alter procedure SP_ALL_PEDIDOS_CLIENTE
//(@codigocliente nvarchar(50))
//as
//	select DISTINCT(CodigoPedido) from PEDIDOS where CodigoCliente = @codigocliente
//go

//create procedure SP_PEDIDOS_CODIGO
//(@codigopedido nvarchar(50))
//as
//	select * from PEDIDOS where CodigoPedido = @codigopedido
//go

//create procedure SP_ELIMINAR_PEDIDO
//(@codigopedido nvarchar(50))
//as
//	delete from PEDIDOS where CodigoPedido = @codigopedido
//go
#endregion

namespace PracticaAdo.Repositories
{
    public class RepositoryClientesPedidos
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryClientesPedidos()
        {
            string connectionString =
                HelperConfiguration.GetConnectionString();
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public async Task<List<string>> GetEmpresasAsync()
        {
            string sql = "SP_ALL_EMPRESAS";

            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();

            this.reader = await this.com.ExecuteReaderAsync();
            List<string> empresas = new List<string>();

            while (await this.reader.ReadAsync())
            {
                string empresa = this.reader["Empresa"].ToString();
                empresas.Add(empresa);
            }

            await this.reader.CloseAsync();
            await this.cn.CloseAsync();

            return empresas;
        }

        public async Task<List<Cliente>> GetClientesEmpresa(string empresa)
        {
            string sql = "SP_CLIENTES_EMPRESA";

            this.com.Parameters.AddWithValue("@empresa", empresa);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();

            this.reader = await this.com.ExecuteReaderAsync();
            List<Cliente> clientes = new List<Cliente>();

            while (await this.reader.ReadAsync())
            {
                Cliente cliente = new Cliente(this.reader["CodigoCliente"].ToString(), this.reader["Empresa"].ToString(), this.reader["Contacto"].ToString(), this.reader["Cargo"].ToString(), this.reader["Ciudad"].ToString(), this.reader["Telefono"].ToString());
                clientes.Add(cliente);
            }

            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();


            return clientes;
        }

        public async Task<List<string>> GetPedidosClienteAsync(string codigocliente)
        {
            string sql = "SP_ALL_PEDIDOS_CLIENTE";

            this.com.Parameters.AddWithValue("@codigocliente", codigocliente);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();

            this.reader = await this.com.ExecuteReaderAsync();
            List<string> pedidos = new List<string>();

            while (await this.reader.ReadAsync())
            {
                string pedido = this.reader["CodigoPedido"].ToString();
                pedidos.Add(pedido);
            }

            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();


            return pedidos;
        }

        public async Task<List<Pedido>> GetPedidosCodigoAsync(string codigopedido)
        {
            string sql = "SP_PEDIDOS_CODIGO";

            this.com.Parameters.AddWithValue("@codigopedido", codigopedido);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();

            this.reader = await this.com.ExecuteReaderAsync();
            List<Pedido> pedidos = new List<Pedido>();

            while (await this.reader.ReadAsync())
            {
                Pedido pedido = new Pedido(this.reader["CodigoPedido"].ToString(), this.reader["CodigoCliente"].ToString(), this.reader["FechaEntrega"].ToString(), this.reader["FormaEnvio"].ToString(), int.Parse(this.reader["Importe"].ToString()));
                pedidos.Add(pedido);
            }

            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();


            return pedidos;
        }

        public async Task InsertPedidoAsync(string codigopedido, string codigocliente, string fechaentrega, string formaenvio, int importe)
        {
        }

        public async Task DeletePedidoAsync(string codigopedido)
        {
            string sql = "SP_ELIMINAR_PEDIDO";

            this.com.Parameters.AddWithValue("@codigopedido", codigopedido);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();

            await this.com.ExecuteNonQueryAsync();

            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }


    }
}
