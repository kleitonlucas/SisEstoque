using SisEstoque;

namespace TesteSisEstoque
{
    [TestFixture]
    public class Tests
    {
        private Tests _tests;
        [SetUp]
        public void Setup()
        {
            _tests = new Tests();
        }

        [Test]
        public void TestarMySQLSelectTodos()
        {
            BancoDeDados bd = new BancoDeDados();
            bd.CurrentBD = 0;
            List<Produto> listaTeste = bd.select();

            Assert.IsNotNull(listaTeste);
        }
        [Test]
        public void TestarPostgresSelectTodos()
        {
            BancoDeDados bd = new BancoDeDados();
            bd.CurrentBD = 1;
            List<Produto> listaTeste = bd.select();

            Assert.IsNotNull(listaTeste);
        }

        [TestCase(5, "Cabo HDMI", "Xing Ling", "Informática", 34, 13)]
        public void TestarMySQLInsert(int codigo, string nome, string marca, 
                            string categoria, float preco, int quantidade)
        {
            Produto produto = new Produto(codigo, nome, marca, categoria, preco, quantidade);
            BancoDeDados bd = new BancoDeDados();
            bd.CurrentBD = 0;

            Assert.IsTrue(bd.insert(produto));
        }
        [TestCase(5, "Cabo HDMI", "Xing Ling", "Informática", 34, 13)]
        public void TestarPostgresInsert(int codigo, string nome, string marca,
                            string categoria, float preco, int quantidade)
        {
            Produto produto = new Produto(codigo, nome, marca, categoria, preco, quantidade);
            BancoDeDados bd = new BancoDeDados();
            bd.CurrentBD = 1;

            Assert.IsTrue(bd.insert(produto));
        }
    }
}