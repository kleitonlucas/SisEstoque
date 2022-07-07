using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SisEstoque
{
    public class MainWindowVM
    {
        public ObservableCollection<Produto> listaProdutos { get; set; }
        public ICommand Adicionar { get; private set; }
        public ICommand Remover { get; private set; }
        public ICommand Editar { get; private set; }
        public Produto produtoSelecionado { get; set; }
        public ConexaoBD Conexao { get; private set; }
        public bool buttonstIsEnable { get; set; }
        
        public MainWindowVM()
        {
            Conexao = new ConexaoBD();
            listaProdutos = new ObservableCollection<Produto>(Conexao.todosProdutos());//Retornar o que já tem na tabela produto
            IniciaComandos();
            //Quero habilitar os botões remover e editar apenas quando estiver algum item selecionado
            buttonstIsEnable = true;
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
                    Conexao.inserir(produto.Codigo, produto.Nome, produto.Marca,
                                    produto.Categoria, produto.Preco, produto.Quantidade);
                    listaProdutos.Add(produto);//Quero que ele faça outro select
                }
            });
            Remover = new RelayCommand((object param) =>
            {
                Conexao.excluir(produtoSelecionado.Codigo);
                listaProdutos.Remove(produtoSelecionado);
            });
            Editar = new RelayCommand((object param) =>
            {
                Produto produtoAtualizado = (Produto)produtoSelecionado.Clone();

                InfoProduto telaInfoCadastro = new InfoProduto();
                telaInfoCadastro.DataContext = produtoAtualizado;
                telaInfoCadastro.ShowDialog();

                if(telaInfoCadastro.DialogResult == true)
                {
                    Conexao.atualizar(produtoAtualizado.Codigo, produtoAtualizado.Nome,
                            produtoAtualizado.Marca, produtoAtualizado.Categoria, 
                            produtoAtualizado.Preco, produtoAtualizado.Quantidade);
                    produtoSelecionado.Nome = produtoAtualizado.Nome;
                    produtoSelecionado.Marca = produtoAtualizado.Marca;
                    produtoSelecionado.Categoria = produtoAtualizado.Categoria;
                    produtoSelecionado.Preco = produtoAtualizado.Preco;
                    produtoSelecionado.Quantidade = produtoAtualizado.Quantidade;
                }
            });
        }
    }
}
