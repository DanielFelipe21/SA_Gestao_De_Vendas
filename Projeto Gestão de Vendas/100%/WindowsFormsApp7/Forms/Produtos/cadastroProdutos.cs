using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp7.Forms;
using WindowsFormsApp7.Forms.Clientes;
using WindowsFormsApp7.Classes.Produtos;
using WindowsFormsApp7.Classes.Clientes;
using System.Threading;

namespace WindowsFormsApp7.Forms.Produtos
{
    public partial class cadastroProdutos : MetroFramework.Forms.MetroForm
    {
        classeCliente Clientes = new classeCliente();
        classeProdutos Produtos = new classeProdutos();
        public cadastroProdutos()
        {
            InitializeComponent();
            textBoxCodigo.Enabled = false;
            textBoxCodigo.Text = Produtos.codigo.ToString();
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            this.Close();
            menuPrincipal inicioDoMenu = new menuPrincipal();
            inicioDoMenu.ShowDialog();
        }

        private void textBoxQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            /*if (e.Handled)
            {
                textBoxValorDeVenda.BackColor = Color.Red;
                textBoxValorDeVenda.Text = "Só é permitido digitar números neste campo";
                Task.Delay(10000);
                textBoxValorDeVenda.BackColor = Color.White;
                textBoxValorDeVenda.Text = "";
            }*/
        }

        private void textBoxValorDeCompra_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            /*if (e.Handled)
            {
                textBoxValorDeVenda.BackColor = Color.Red;
                textBoxValorDeVenda.Text = "Só é permitido digitar números neste campo";
                Task.Delay(10000);
                textBoxValorDeVenda.BackColor = Color.White;
                textBoxValorDeVenda.Text = "";
            }*/
        }

        private void textBoxValorDeVenda_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            /*if (e.Handled)
            {
                textBoxValorDeVenda.BackColor = Color.Red;
                textBoxValorDeVenda.Text = "Só é permitido digitar números neste campo";
                Task.Delay(10000);
                textBoxValorDeVenda.BackColor = Color.White;
                textBoxValorDeVenda.Text = "";
            }*/
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (sender == buttonSave)
            {
                var validateError = string.Empty;
                if (textBoxNome.Text.Length == 0) // Box Name
                    validateError = "- O produto necessita da inserção de um nome.";
                if (comboBoxFornecedor.Text.Length == 0)
                    validateError += "- O produto necessita da inserção de um fornecedor válido.";
                if (textBoxValorDeCompra.Text.Length == 0)
                    validateError += "- O produto necessita da inserção de um de compra válido.";
                if (textBoxValorDeVenda.Text.Length == 0)
                    validateError += "- O produto necessita da inserção de um valor de venda válido.";
                if (textBoxQuantidade.Text.Length == 0)
                    validateError += "- O produto necessita da inserção de uma quantidade válida.";
                if (dataDaCompra.Text.Length == 0)
                    validateError += "- O produto necessita da inserção de uma data de compra válida.";
                if (validateError != string.Empty)
                {
                    MessageBox.Show(validateError.Replace('.', '\n'), @"O cadastro está incompleto", MessageBoxButtons.OK);
                    return;
                }

                Produtos.nome = textBoxNome.Text;
                Produtos.fornecedor = comboBoxFornecedor.Text;
                Produtos.valorDeCompra = Convert.ToDouble(textBoxValorDeCompra.Text);
                Produtos.valorDeVenda = Convert.ToDouble(textBoxValorDeVenda.Text);
                Produtos.quantidade = int.Parse(textBoxQuantidade.Text);
                Produtos.dataDeEntrada = dataDaCompra.Text.ToString();

                double lucro = (Produtos.valorDeVenda - Produtos.valorDeCompra) * Produtos.quantidade;

                Produtos.lucro = lucro;

                ListViewItem varItem = new ListViewItem(new string[] { textBoxCodigo.Text,
                                                                textBoxNome.Text,
                                                                textBoxQuantidade.Text,
                                                                textBoxValorDeCompra.Text,
                                                                textBoxValorDeVenda.Text,
                                                                "R$: " +lucro.ToString() +" ",
                                                                dataDaCompra.Text.ToString(),
                                                                comboBoxFornecedor.Text.ToString(),
                                                                textBoxDescricao.Text
                                                               });
                listProdutos.Items.Add(varItem);
                listProdutos.Items[listProdutos.Items.Count - 1].EnsureVisible(); //for autoscroll


                MessageBox.Show($"Novo produto adicionado.");

                textBoxNome.Text = "";
                textBoxQuantidade.Text = "";
                textBoxValorDeCompra.Text = "";
                textBoxValorDeVenda.Text = "";
                dataDaCompra.Text = "";
                comboBoxFornecedor.Text = "";
                textBoxDescricao.Text = "";
                textBoxCodigo.Text = Produtos.codigo.ToString() + 1;
                labelTotal.Text = string.Format("Total de Produtos: {0}", listProdutos.Items.Count);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listProdutos.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecione um produto.");
            }
            else
            {
                string TEXT_Confirmacao = "Você realmente deseja excluir este(s) produto(s)?";
                if (MessageBox.Show(string.Format("Deseja remover a(s) {0} produtos selecionados?", listProdutos.SelectedIndices.Count), TEXT_Confirmacao, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                MessageBox.Show("O produto selecionado, foi apagado!");
                for (int i = listProdutos.SelectedItems.Count - 1; i >= 0; i--)
                {
                    ListViewItem li = listProdutos.SelectedItems[i];
                    listProdutos.Items.Remove(li);
                    labelTotal.Text = string.Format("Total de Produtos: {0}", listProdutos.Items.Count);
                    textBoxNome.Text = "";
                    textBoxQuantidade.Text = "";
                    textBoxValorDeCompra.Text = "";
                    textBoxValorDeVenda.Text = "";
                    dataDaCompra.Text = "";
                    comboBoxFornecedor.Text = "";
                    textBoxDescricao.Text = "";
                    textBoxCodigo.Text = "";
                    labelTotal.Text = "";
                }
            }
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            if (listProdutos.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Selecione um produto.");
            }
            else if (listProdutos.SelectedIndices.Count == 1)
            {
                string TEXT_Confirmacao = "Você realmente deseja edita este produto?";
                if (MessageBox.Show(string.Format("Deseja editar este produto selecionado? \nLembre-se, você terá que readiciona-lo mesmo que não faça nenhuma alteração.", listProdutos.SelectedIndices.Count), TEXT_Confirmacao, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                if (listProdutos.SelectedItems[0].Selected)
                {
                    textBoxCodigo.Text = listProdutos.FocusedItem.SubItems[0].Text;
                    textBoxNome.Text = listProdutos.FocusedItem.SubItems[1].Text;
                    comboBoxFornecedor.Text = listProdutos.FocusedItem.SubItems[7].Text;
                    textBoxValorDeCompra.Text = listProdutos.FocusedItem.SubItems[3].Text;
                    textBoxValorDeVenda.Text = listProdutos.FocusedItem.SubItems[4].Text;
                    textBoxQuantidade.Text = listProdutos.FocusedItem.SubItems[2].Text;
                    textBoxDescricao.Text = listProdutos.FocusedItem.SubItems[8].Text;
                    //Remover para readicioanr
                    for (int i = listProdutos.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem li = listProdutos.SelectedItems[i];
                        listProdutos.Items.Remove(li);
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////
                }
            }
            else
            {
                MessageBox.Show("Selecione apenas um produto para realizar uma edição.");
            }
        }

        private void cadastroProdutos_Load(object sender, EventArgs e)
        {
            comboBoxFornecedor.Items.Insert(0, Produtos.fornecedor.ToString());
        }
    }
}
