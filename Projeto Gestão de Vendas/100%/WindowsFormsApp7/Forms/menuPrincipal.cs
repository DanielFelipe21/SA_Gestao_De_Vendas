using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp7.Forms.Produtos;
using WindowsFormsApp7.Forms.Clientes;
using WindowsFormsApp7.Forms.Vendas;

namespace WindowsFormsApp7.Forms
{
    public partial class menuPrincipal : MetroFramework.Forms.MetroForm
    {
        cadastroProdutos formsProdutos = new cadastroProdutos();
        cadastroClientes formsClientes = new cadastroClientes();
        cadastroVendas formsVendas = new cadastroVendas();

        public menuPrincipal()
        {
            InitializeComponent();
        }

        private void buttonClientes_Click(object sender, EventArgs e)
        {
            this.Hide();
            formsClientes.ShowDialog();
        }

        private void buttonProdutos_Click(object sender, EventArgs e)
        {
            this.Hide();
            formsProdutos.ShowDialog();
        }

        private void buttonProdutos_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            formsProdutos.ShowDialog();
        }

        private void buttonVendas_Click(object sender, EventArgs e)
        {
            this.Hide();
            formsVendas.ShowDialog();
        }

        private void buttonInformativo_Click(object sender, EventArgs e)
        {
            menuInformativo menuDeInfo = new menuInformativo();
            this.Hide();
            menuDeInfo.ShowDialog(); 
        }
    }
}
