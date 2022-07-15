using Moq;
using SisEstoque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteSisEstoque
{
    [TestFixture]
    public class TestUnitBD
    {
        [Test]
        public void TestarInsert()
        {
            Moq.Mock<IBancoDeDados> mock = new Moq.Mock<IBancoDeDados>();
            mock.Setup(x => x.insert(It.IsAny<Produto>())).Returns(true);

            BancoDeDados bd = new BancoDeDados(mock.Object);
            Produto produto = new Produto(5, "Cabo HDMI", "Xing Ling", "Informática", 34, 13);

            Assert.IsTrue(bd.Bds[0].insert(produto));
        }
        [Test]
        public void TestarDelete()
        {
            Moq.Mock<IBancoDeDados> mock = new Moq.Mock<IBancoDeDados>();
            mock.Setup(x => x.delete(It.IsAny<Produto>())).Returns(true);

            BancoDeDados bd = new BancoDeDados(mock.Object);
            Produto produto = new Produto(5, "Cabo HDMI", "Xing Ling", "Informática", 34, 13);

            Assert.IsTrue(bd.Bds[0].delete(produto));
        }
        [Test]
        public void TestarUpdate()
        {
            Moq.Mock<IBancoDeDados> mock = new Moq.Mock<IBancoDeDados>();
            mock.Setup(x => x.update(It.IsAny<Produto>())).Returns(true);

            BancoDeDados bd = new BancoDeDados(mock.Object);
            Produto produto = new Produto(5, "Cabo HDMI", "Xing Ling", "Informática", 34, 13);

            Assert.IsTrue(bd.Bds[0].update(produto));
        }
    }
}
