using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp7.Forms
{
    public partial class menuInformativo : MetroFramework.Forms.MetroForm
    {
        public menuInformativo()
        {
            InitializeComponent();
            textBoxInformacao.Text = "Este Software tem como objetivo: Otimizar os processos comerciais, expandindo a produtividade em tempo real, aprimorando as interações com clientes e o retorno sobre investimento. \n Principais pontos:";
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            menuPrincipal menuDeInicio = new menuPrincipal();
            this.Hide();
            menuDeInicio.ShowDialog();
        }
    }
}
