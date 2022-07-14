using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SisEstoque
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public ObservableCollection<Produto> listaProdutos { get; set; }
        //public ConexaoBD Conexao { get; private set; }
        private Produto produtoSelecionado;
        private int bdSelecionado;
        private bool buttonstIsEnable;

        //Apenas teste
        //public Postgres postgres { get; set; }
        //private MySQL mysql;
        private BancoDeDados bd;

        /* Esconder detalhes dos BDs da mainWindow
         * 
         */

        public MainWindowVM()
        {
            //Conexao = new ConexaoBD();
            //postgres = new Postgres();
            //mysql = new MySQL();
            //listaProdutos = new ObservableCollection<Produto>(postgres.select());//(Conexao.todosProdutos());//Retornar o que já tem na tabela produto
            //listaProdutos = new ObservableCollection<Produto>(mysql.select());
            IniciaComandos();
            bd = new BancoDeDados();
            listaProdutos = new ObservableCollection<Produto>(bd.select());
            //Quero habilitar os botões remover e editar apenas quando estiver algum item selecionado
            buttonstIsEnable = true;
        }

        public BancoDeDados Bd
        {
            get { return bd; }
            set { bd = value; }
        }
        public ICommand Adicionar { get; private set; }
        public ICommand Remover { get; private set; }
        public ICommand Editar { get; private set; }
        public ICommand Mudar { get; private set; }
        public bool ButtonstIsEnable
        {
            get { return buttonstIsEnable; }
            set { buttonstIsEnable = value; }
        }
        public Produto ProdutoSelecionado {
            get { return produtoSelecionado; }
            set { 
                produtoSelecionado = value;
            }
        }
        public int BdSelecionado
        {
            get { return bdSelecionado; }
            set { bdSelecionado = value; }
        }

        public void IniciaComandos()
        {
            Adicionar = new RelayCommand((object param) =>
            {
                Produto produto = new Produto(listaProdutos.Count+1);

                InfoProduto telaInfoCadastro = new InfoProduto();
                telaInfoCadastro.DataContext = produto;
                telaInfoCadastro.ShowDialog();

                if(telaInfoCadastro.DialogResult == true)
                {
                    //Conexao.inserir(produto.Codigo, produto.Nome, produto.Marca,
                    //                produto.Categoria, produto.Preco, produto.Quantidade);
                    //postgres.insert(produto);
                    //mysql.insert(produto);
                    Bd.insert(produto);
                    listaProdutos.Add(produto);//Quero que ele faça outro select
                }
            });
            Remover = new RelayCommand((object param) =>
            {
                //Conexao.excluir(produtoSelecionado.Codigo);
                //postgres.delete(produtoSelecionado);
                //mysql.delete(ProdutoSelecionado);
                Bd.delete(ProdutoSelecionado);
                listaProdutos.Remove(ProdutoSelecionado);
            });
            Editar = new RelayCommand((object param) =>
            {
                Produto produtoAtualizado = (Produto)ProdutoSelecionado.Clone();

                InfoProduto telaInfoCadastro = new InfoProduto();
                telaInfoCadastro.DataContext = produtoAtualizado;
                telaInfoCadastro.ShowDialog();

                if(telaInfoCadastro.DialogResult == true)
                {
                    //Conexao.atualizar(produtoAtualizado.Codigo, produtoAtualizado.Nome,
                    //        produtoAtualizado.Marca, produtoAtualizado.Categoria, 
                    //        produtoAtualizado.Preco, produtoAtualizado.Quantidade);
                    //postgres.update(produtoAtualizado);
                    //mysql.update(produtoAtualizado);
                    Bd.update(produtoAtualizado);
                    ProdutoSelecionado.Nome = produtoAtualizado.Nome;
                    ProdutoSelecionado.Marca = produtoAtualizado.Marca;
                    ProdutoSelecionado.Categoria = produtoAtualizado.Categoria;
                    ProdutoSelecionado.Preco = produtoAtualizado.Preco;
                    ProdutoSelecionado.Quantidade = produtoAtualizado.Quantidade;
                }
            });
            Mudar = new RelayCommand((object param) =>
            {
                Bd.CurrentBD = BdSelecionado;
                listaProdutos.Clear();
                atualizaLista();

                Produto p = Bd.select(1);
                Console.WriteLine("cod " + p.Codigo + " nome " + p.Nome);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notifica(string propriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propriedade));
        }

        public void atualizaLista()
        {
            foreach (Produto p in Bd.select())
            {
                listaProdutos.Add(p);
            }
        }
    }
}
