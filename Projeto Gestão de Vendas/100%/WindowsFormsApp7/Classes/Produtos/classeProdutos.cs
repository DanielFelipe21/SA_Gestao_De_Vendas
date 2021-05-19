using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp7.Classes.Produtos
{
    class classeProdutos
    {
        //Informaçoes do Produto
        public int codigo { get; set; }
        public String nome { get; set; }

        public String descProduto { get; set; }
        public String fornecedor { get; set; }

        public int quantidade { get; set; }

        public String dataDeEntrada { get; set; }

        public double valorDeVenda { get; set; }

        public double valorDeCompra { get; set; }
        public double lucro { get; set; }


        public static int SomarVendas(int num1, int num2)
        {
            return num1 + num2;
        }

        public static int SubtrairVendas(int num1, int num2)
        {
            return num1 - num2;
        }

        public static int MultiplicarVendas(int num1, int num2)
        {
            return num1 * num2;
        }

        public static int DividirVendas(int num1, int num2)
        {
            return num1 / num2;
        }
    }
}
