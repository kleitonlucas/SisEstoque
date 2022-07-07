using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisEstoque
{
    public class Produto : ICloneable, INotifyPropertyChanged
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
            set { 
                codigo = value;
                Notifica("Codigo");
            }
        }
        public string Nome
        {
            get { return nome; }
            set { 
                nome = value;
                Notifica("Nome");
            }
        }
        public string Marca
        {
            get { return marca; }
            set { 
                marca = value;
                Notifica("Marca");
            }
        }
        public string Categoria
        {
            get { return categoria; }
            set { 
                categoria = value;
                Notifica("Categoria");
            }
        }
        public float Preco
        {
            get { return preco; }
            set { 
                preco = value;
                Notifica("Preco");
            }
        }
        public int Quantidade
        {
            get { return quantidade; }
            set { 
                quantidade = value;
                Notifica("Quantidade");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notifica(string propriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
