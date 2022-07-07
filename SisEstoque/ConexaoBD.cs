using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisEstoque
{
    public class ConexaoBD
    {
        private NpgsqlConnection conexao;
        private string conexaoString = @"Server=localhost;Port=54320;User id=postgres;Password=postgres;Database=postgres";
        
        public ConexaoBD()
        {
            conexao = new NpgsqlConnection(ConexaoString);
        }
        public NpgsqlConnection Conexao
        {
            get { return conexao; }
            private set { conexao = value; }
        }
        public string ConexaoString
        {
            get { return conexaoString; }
            private set { conexaoString = value; }
        }
        public void abrirConexaoBD()
        {
            if(Conexao.State != ConnectionState.Open)
            {
                Conexao.Open();
            }
        }
        public void fecharConexaoBD()
        {
            if (Conexao.State != ConnectionState.Closed)
            {
                Conexao.Close();
            }
        }
        public List<Produto> todosProdutos()
        {
            List<Produto> todos = new List<Produto>();
            abrirConexaoBD();
            DataTable dataTable = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM produto", Conexao);
            
            try
            {
                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Produto produto = new Produto()
                    {
                        Codigo = (int)row["codigo"],
                        Nome = (string)row["nome"],
                        Marca = (string)row["marca"],
                        Categoria = (string)row["categoria"],
                        Preco = (float)row["preco"],
                        Quantidade = (int)row["quantidade"],
                    };
                    todos.Add(produto);
                }
            }
            finally
            {
                fecharConexaoBD();
                dataTable.Dispose();
                adapter.Dispose();
            }
            return todos;
        }
        public void inserir(int codigo, string nome, string marca, string categoria, float preco,
                        int quantidade)
        {
            abrirConexaoBD();
            NpgsqlCommand insert = new NpgsqlCommand("INSERT into produto " +
                                        "(codigo, nome, marca, categoria, preco, quantidade) " +
                                        "VALUES ('"+codigo+"', '"+nome+"', '"+marca+"', '" +
                                        categoria+"', '"+preco+"', '"+quantidade+"')", Conexao);
            try
            {
                int nRows = insert.ExecuteNonQuery();
                if (nRows >= 1)
                    Console.WriteLine("Produto inserido com sucesso!");
                else
                {
                    Console.WriteLine("Falha na inserção do produto!");
                }
            }
            finally
            {
                fecharConexaoBD();
                insert.Dispose();
            }
        }
        public void excluir(int codigo)
        {
            abrirConexaoBD();
            NpgsqlCommand delete = new NpgsqlCommand("DELETE FROM produto WHERE "+
                                    "codigo='"+codigo+"'", Conexao);
            try
            {
                int nRows = delete.ExecuteNonQuery();
                if (nRows >= 1)
                    Console.WriteLine("Produto excluído com sucesso!");
                else
                    Console.WriteLine("Não foi possível excluir o produto!");
            }
            finally
            {
                fecharConexaoBD();
                delete.Dispose();
            }
        }
        public void atualizar(int codigo, string nome, string marca, string categoria,
                        float preco, int quantidade)
        {
            abrirConexaoBD();
            NpgsqlCommand update = new NpgsqlCommand("UPDATE produto SET codigo='"+codigo+"'," +
                                    " nome='"+nome+"', marca='"+marca+"', categoria='"+
                                    categoria+"', preco='"+preco+"', quantidade='"+quantidade+
                                    "' WHERE codigo='"+codigo+"'", Conexao);
            try
            {
                int nRows = update.ExecuteNonQuery();
                if (nRows >= 1)
                    Console.WriteLine("Atualizaçã de produto realizada!");
                else
                    Console.WriteLine("Falha na atualização do produto!");
            }
            finally
            {
                update.Dispose();
                fecharConexaoBD();
            }
        }
    }
}
