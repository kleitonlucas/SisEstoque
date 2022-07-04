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
        public bool buttonstIsEnable { get; set; }
        public MainWindowVM()
        {
            listaProdutos = new ObservableCollection<Produto>();
            //Quero habilitar os botões remover e editar apenas quando estiver algum item selecionado
            buttonstIsEnable = true;
            IniciaComandos();
        }

        public void IniciaComandos()
        {
            Adicionar = new RelayCommand((object param) =>
            {
                Produto produto = new Produto(listaProdutos.Count+1);

                InfoProduto telaInfoCadastro = new InfoProduto();
                telaInfoCadastro.DataContext = produto;
                telaInfoCadastro.ShowDialog();

                listaProdutos.Add(produto);
            });
            Remover = new RelayCommand((object param) =>
            {
                listaProdutos.Remove(produtoSelecionado);
            });
            Editar = new RelayCommand((object param) =>
            {
                InfoProduto telaInfoCadastro = new InfoProduto();
                telaInfoCadastro.DataContext = produtoSelecionado;
                telaInfoCadastro.ShowDialog();
            });
        }
    }
}
