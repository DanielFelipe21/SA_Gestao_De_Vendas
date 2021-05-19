using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp7.Classes.Clientes
{
    public class classeCliente
    {
        //Informações sobre a pessoa
        public String nome { get; set; }

        public String apelido { get; set; }

        public String CPF { get; set; }

        public String sexo { get; set; }

        public String RG { get; set; }

        public String nascimento { get; set; }

        //Informações sobre a pessoa adicionais
        public String email { get; set; }

        public String telefone { get; set; } 

        public String celular { get; set; }

    //Informações sobre o tipo de pessoa
        public String razaoSocial { get; set; }

        public String tipoDePessoa { get; set; }
        public int codCNPJ { get; set; }

    //Informações sobre endereço
        public String endereco { get; set; } 

        public String bairro { get; set; }

        public String cep { get; set; }

        public String complemento { get; set; }

        public String cidade { get; set; }


        public String estado { get; set; }

        /*public classeCliente(string nome, string CPF, string sexo, string RG, string bairro, string cep, string complemento, string cidade, string estado, string celular, string apelido, string email, string endereco, string nascimento, string telefone)
        {
            this.nome = nome;

            this.apelido = apelido;

            this.CPF = CPF;

            this.sexo = sexo;

            this.RG = RG;

            this.nascimento = nascimento;

            this.endereco = endereco;

            this.bairro = bairro;

            this.cep = cep;

            this.complemento = complemento;

            this.cidade = cidade;

            this.estado = estado;

            this.telefone = telefone;

            this.celular = celular;

            this.email = email;
        }*/
    }
}
