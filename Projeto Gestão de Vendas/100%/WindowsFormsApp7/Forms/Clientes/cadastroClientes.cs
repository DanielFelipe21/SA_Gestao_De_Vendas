using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp7.Classes.Clientes;
using WindowsFormsApp7.Classes.Produtos;
using WindowsFormsApp7.Forms.Produtos;

namespace WindowsFormsApp7.Forms.Clientes
{

   

    public partial class cadastroClientes : MetroFramework.Forms.MetroForm
    {
        public cadastroClientes()
        {
            InitializeComponent();
        }

        classeCliente Clientes = new classeCliente();
        classeProdutos Produtos = new classeProdutos();
        cadastroProdutos CadastrarFornecedor = new cadastroProdutos();
       
        private void buttonSave_Click(object sender, EventArgs e) // Botão Salvar
        {
            if (sender == buttonSave)
            {
                var validateError = string.Empty;
                if (textBoxNome.Text.Length == 0) // Box Name
                    validateError = "- O cadastro necessita da inserção de um nome.";
                if (textBoxCPF.Text.Length == 0 || textBoxCPF.Text.Length < 11)
                    validateError += "- O cadastro necessita da inserção de um CPF ou CNPJ válido.";
                if (textBoxRG.Text.Length == 0)
                    validateError += "- O cadastro necessita da inserção de um RG válido.";
                if (textEndereco.Text.Length == 0)
                    validateError += "- O cadastro necessita da inserção de um endereço válido.";
                if (textBairro.Text.Length == 0)
                    validateError += "- O cadastro necessita da inserção de um bairro válido.";
                if (textComplemento.Text.Length == 0)
                    validateError += "- O cadastro necessita da inserção de um complemento válido.";
                if (textCEP.Text.Length == 0)
                    validateError += "- O cadastro necessita da inserção de um CEP válido.";
                if (textCidade.Text.Length == 0)
                    validateError += "- O cadastro necessita da inserção de uma cidade válida.";
                if (textBoxTelefone.Text.Length == 0)
                    validateError += "- O cadastro necessita da inserção de um telefone válido.";
                if (textBoxCelular.Text.Length == 0)
                    validateError += "- O cadastro necessita da inserção de um celular válido.";
                if (textBoxEmail.Text.Length == 0)
                    validateError += "- O cadastro necessita da inserção de um e-mail válido.";
                if (radioFeminino.Checked == false && radioMasculino.Checked == false)
                    validateError += "- O cadastro necessita da definição de um sexo.";
                if (radioFisica.Checked == false && radioJuridica.Checked == false)
                    validateError += "- O cadastro necessita da definição de um tipo de pessoa válido.";
                if (validateError != string.Empty)
                {
                    MessageBox.Show(validateError.Replace('.', '\n'), @"O cadastro está incompleto", MessageBoxButtons.OK);
                    return;
                }


                // Caminho que o arquivo será salvo.
                string arquivo = @"C:\Users\danie\Desktop\" + textBoxNome.Text.ToString() + "_cadastro.txt";

                MessageBox.Show($"Novo cadastro adicionado.");
                MessageBox.Show($"\nOs dados cadastrais deste clientes, foram exportados para o diretodio: "+ arquivo +".");



                if (!File.Exists(arquivo))
                {
                    using (TextWriter tw = new StreamWriter(arquivo))
                    {
                        tw.WriteLine($"Informações de Cadastro:");
                        tw.WriteLine($"\n");
                        tw.WriteLine($"- Principal:");
                        tw.WriteLine($"Nome: {textBoxNome.Text.ToString()}");
                        if (textBoxApelido.Text.Length > 0)
                        {
                            tw.WriteLine($"Apelido: {textBoxApelido.Text.ToString()}");
                        }
                        tw.WriteLine($"CPF/CNPJ: {textBoxCPF.Text.ToString()}");
                        tw.WriteLine($"RG: {textBoxRG.Text.ToString()}");
                        tw.WriteLine($"Data de Nascimento: {dateNascimento.Text.ToString()}");
                        if (radioFeminino.Checked) // Condição para o sexo
                        {
                            tw.WriteLine($"Sexo: Feminino");
                        }
                        else if (radioMasculino.Checked)
                        {
                            tw.WriteLine($"Sexo: Masculino");
                        }

                        if (radioFisica.Checked) // Condição para o tipo de pessoa
                        {
                            tw.WriteLine($"Tipo de Pessoa: Física");
                        }
                        else if (radioJuridica.Checked) // Condição para o tipo de pessoa
                        {
                            tw.WriteLine($"Tipo de Pessoa: Júridica");
                        }

                        tw.WriteLine($"\n");
                        tw.WriteLine($"- Endereço:");
                        tw.WriteLine($"Endereço: {textEndereco.Text.ToString()}");
                        tw.WriteLine($"Bairro: {textBairro.Text.ToString()}");
                        tw.WriteLine($"CEP: {textCEP.Text.ToString()}");
                        tw.WriteLine($"Complemento: {textComplemento.Text.ToString()}");
                        tw.WriteLine($"Estado: {comboBoxEstado.Text.ToString()}");
                        tw.WriteLine($"Cidade: {textCidade.Text.ToString()}");
                        tw.WriteLine($"\n");
                        tw.WriteLine($"- Contato:");
                        tw.WriteLine($"Telefone: {textBoxTelefone.Text.ToString()}");
                        tw.WriteLine($"Celular: {textBoxTelefone.Text.ToString()}");
                        tw.WriteLine($"E-mail: {textBoxEmail.Text.ToString()}");
                    }
                }
                else
                {
                    using (TextWriter tw = new StreamWriter(arquivo))
                    {
                        tw.WriteLine($"Informações de Cadastro:");
                        tw.WriteLine($"\n");
                        tw.WriteLine($"- Principal:");
                        tw.WriteLine($"Nome: {textBoxNome.Text.ToString()}");
                        if (textBoxApelido.Text.Length > 0)
                        {
                            tw.WriteLine($"Apelido: {textBoxApelido.Text.ToString()}");
                        }
                        tw.WriteLine($"CPF/CNPJ: {textBoxCPF.Text.ToString()}");
                        tw.WriteLine($"RG: {textBoxRG.Text.ToString()}");
                        tw.WriteLine($"Data de Nascimento: {dateNascimento.Text.ToString()}");
                        if (radioFeminino.Checked) // Condição para o sexo
                        {
                            tw.WriteLine($"Sexo: Feminino");
                        }
                        else if (radioMasculino.Checked)
                        {
                            tw.WriteLine($"Sexo: Masculino");
                        }

                        if (radioFisica.Checked) // Condição para o tipo de pessoa
                        {
                            tw.WriteLine($"Tipo de Pessoa: Física");
                        }
                        else if (radioJuridica.Checked) // Condição para o tipo de pessoa
                        {
                            tw.WriteLine($"Tipo de Pessoa: Júridica");
                        }

                        tw.WriteLine($"\n");
                        tw.WriteLine($"- Endereço:");
                        tw.WriteLine($"Endereço: {textEndereco.Text.ToString()}");
                        tw.WriteLine($"Bairro: {textBairro.Text.ToString()}");
                        tw.WriteLine($"CEP: {textCEP.Text.ToString()}");
                        tw.WriteLine($"Complemento: {textComplemento.Text.ToString()}");
                        tw.WriteLine($"Estado: {comboBoxEstado.Text.ToString()}");
                        tw.WriteLine($"Cidade: {textCidade.Text.ToString()}");
                        tw.WriteLine($"\n");
                        tw.WriteLine($"- Contato:");
                        tw.WriteLine($"Telefone: {textBoxTelefone.Text.ToString()}");
                        tw.WriteLine($"Celular: {textBoxTelefone.Text.ToString()}");
                        tw.WriteLine($"E-mail: {textBoxEmail.Text.ToString()}");
                    }
                }

                //Atribuir valores das variaveis e zerar campos

                Clientes.nome = textBoxNome.Text.ToString();
                Clientes.bairro = textBairro.Text.ToString();
                Clientes.complemento = textComplemento.Text.ToString();
                Clientes.cep = textCEP.Text.ToString();
                Clientes.cidade = textCidade.Text.ToString();
                Clientes.telefone = textBoxTelefone.Text.ToString();
                Clientes.celular = textBoxCelular.Text.ToString();
                Clientes.email = textBoxEmail.Text.ToString();
                Clientes.CPF = textBoxCPF.Text.ToString();
                Clientes.RG = textBoxRG.Text.ToString();
                Clientes.endereco = textEndereco.Text.ToString();

                if (textBoxApelido.Text.Length > 0)
                {
                    Clientes.apelido = textBoxApelido.Text.ToString();
                }

                if (radioFeminino.Checked)
                {
                    Clientes.sexo = radioFeminino.Text.ToString();
                }
                else if (radioMasculino.Checked)
                {
                    Clientes.sexo = radioMasculino.Text.ToString();
                }

                if (radioFisica.Checked)
                {
                    Clientes.tipoDePessoa = radioFisica.Text.ToString();
                }
                else if (radioJuridica.Checked == true)
                {
                    Clientes.codCNPJ =  0;
                    Clientes.codCNPJ = Clientes.codCNPJ + 1;
                    Clientes.tipoDePessoa = radioJuridica.Text.ToString();
                    Produtos.fornecedor = Clientes.nome;
                }

                radioFeminino.Checked = false;
                radioMasculino.Checked = false;
                radioFisica.Checked = false;
                radioJuridica.Checked = false;
                comboBoxEstado.Text = "";
                textBoxCPF.Text = "";
                textBoxRG.Text = "";
                textEndereco.Text = "";
                textBairro.Text = "";
                textComplemento.Text = "";
                textCEP.Text = "";
                textCidade.Text = "";
                textBoxTelefone.Text = "";
                textBoxCelular.Text = "";
                textBoxEmail.Text = "";
                textBoxApelido.Text = "";
                textBoxNome.Text = "";
                return;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
            menuPrincipal inicioDoMenu = new menuPrincipal();
            inicioDoMenu.Show();
        }

        private void buttonSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            ofd1.Multiselect = false;
            ofd1.Title = "Selecionar Fotos";
            ofd1.Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" + "All files (*.*)|*.*";
            ofd1.CheckFileExists = true;
            ofd1.CheckPathExists = true;
            ofd1.FilterIndex = 2;
            ofd1.RestoreDirectory = true;
            ofd1.ReadOnlyChecked = true;
            ofd1.ShowReadOnly = true;

            DialogResult dr = ofd1.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String arquivo in ofd1.FileNames)
                {
                    Image Imagem = Image.FromFile(arquivo);
                    pictureBoxFoto.Image = Imagem;
                }
            }
        }
    }
 }
