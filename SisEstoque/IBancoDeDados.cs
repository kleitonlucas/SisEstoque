using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisEstoque
{
    public interface IBancoDeDados
    {
        List<Produto> select();
        Produto select(int codigo);
        bool insert(Produto produto);
        bool delete(Produto produto);
        bool update(Produto produto);
    }
}
