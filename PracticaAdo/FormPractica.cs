using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PracticaAdo.Models;
using PracticaAdo.Repositories;

namespace Test
{
    public partial class FormPractica : Form
    {
        RepositoryClientesPedidos repository;
        List<Cliente> clientes;
        public FormPractica()
        {
            InitializeComponent();
            this.repository = new RepositoryClientesPedidos();
            this.clientes = new List<Cliente>();
            this.GetEmpresas();
        }

        private async void cmbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.clientes = await this.repository.GetClientesEmpresa(this.cmbclientes.SelectedItem.ToString());
            this.txtempresa.Text = clientes[0].Empresa;
            this.txtcontacto.Text = clientes[0].Contacto;
            this.txtcargo.Text = clientes[0].Cargo;
            this.txtciudad.Text = clientes[0].Ciudad;
            this.txttelefono.Text = clientes[0].Telefono;

            this.GetPedidos();
        }

        private async void GetEmpresas()
        {
            List<string> empresas = await this.repository.GetEmpresasAsync();
            this.cmbclientes.DataSource = empresas;
        }

        private async void lstpedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Pedido> pedidos = await this.repository.GetPedidosCodigoAsync(this.lstpedidos.SelectedItem.ToString());
            this.txtcodigopedido.Text = pedidos[0].CodigoPedido;
            this.txtfechaentrega.Text = pedidos[0].FechaEntrega;
            this.txtformaenvio.Text = pedidos[0].FormaEnvio;
            this.txtimporte.Text = pedidos[0].Importe.ToString();
        }

        private async void btneliminarpedido_Click(object sender, EventArgs e)
        {
            string codigo = this.lstpedidos.SelectedItem.ToString();
            await this.repository.DeletePedidoAsync(codigo);
            MessageBox.Show("Pedido " + codigo + " eliminado.");
            this.GetPedidos();
        }

        private async void GetPedidos()
        {
            List<string> pedidos = await this.repository.GetPedidosClienteAsync(this.clientes[0].CodigoCliente);
            this.lstpedidos.DataSource = pedidos;
        }
    }
}
