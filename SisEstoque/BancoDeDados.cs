using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisEstoque
{
    public class BancoDeDados
    {
        private int currentBD;
        private List<IBancoDeDados> bds;

        public BancoDeDados()
        {
            currentBD = 0;
            this.bds = new List<IBancoDeDados>();
            carregarBDs();
        }

        public List<IBancoDeDados> Bds
        {
            get { return this.bds; }
            private set { this.bds = value; }
        }
        public int CurrentBD
        {
            get { return this.currentBD; }
            set { this.currentBD = value; }
        }

        private void carregarBDs()
        {
            bds.Add(new MySQL());
            bds.Add(new Postgres());
        }

        public List<Produto> select()
        {
            return bds[currentBD].select();
        }

        public Produto select(int codigo)
        {            
            return bds[currentBD].select(codigo);
        }

        public bool insert(Produto produto)
        {
            if (!codigoCadastrado(produto.Codigo))
                return false;
            
            return bds[currentBD].insert(produto);
        }

        public bool delete(Produto produto)
        {
            return bds[currentBD].delete(produto);
        }

        public bool update(Produto produto)
        {
            return bds[currentBD].update(produto);
        }

        public bool codigoCadastrado(int codigo)
        {
            if (select(codigo) == null)
                return false;
            else
                return true;
        }
    }
}
