using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApp7.Classes.Produtos;

namespace TesteUnitario
{
    [TestClass]
    public class Teste_Unitario
    {
        [TestMethod]
        public void TestMethod_Somar()
        {
            classeProdutos Produto = new classeProdutos();
            Assert.AreEqual(classeProdutos.SomarVendas(10, 5), 15, "OK");
        }

        [TestMethod]
        public void TestMethod_Subtrair()
        {
            classeProdutos Produto = new classeProdutos();
            Assert.AreEqual(classeProdutos.SubtrairVendas(15, 5), 10, "OK");
        }

        [TestMethod]
        public void TestMethod_Multiplicar()
        {
            classeProdutos Produto = new classeProdutos();
            Assert.AreEqual(classeProdutos.MultiplicarVendas(10, 5), 50, "OK");
        }

        [TestMethod]
        public void TestMethod_Dividir()
        {
            classeProdutos Produto = new classeProdutos();
            Assert.AreEqual(classeProdutos.DividirVendas(15, 0), 3, "OK");
        }
    }
}
