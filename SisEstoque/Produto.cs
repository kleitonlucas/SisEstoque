using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisEstoque
{
    public class Produto
    {
        private int codigo;
        private string nome;
        private string marca;
        private string categoria;
        private float preco;
        private int quantidade;

        public Produto()
        {
        }
        public Produto(int codigo)
        {
            this.codigo = codigo;
        }
        public Produto(int codigo, string nome, string marca, string categoria, float preco, int quantidade)
        {
            this.codigo = codigo;
            this.nome = nome;
            this.marca = marca;
            this.categoria = categoria;
            this.preco = preco;
            this.quantidade = quantidade;
        }

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public string Marca
        {
            get { return marca; }
            set { marca = value; }
        }
        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }
        public float Preco
        {
            get { return preco; }
            set { preco = value; }
        }
        public int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }
    }
}
