using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisEstoque
{
    public class Postgres : IBancoDeDados
    {
        private string nome;
        private string conexaoString = "Server=localhost;Port=5432;User id=postgres;Password=postgres;Database=sisestoque";
        private NpgsqlConnection conexao;//Falta o dispose
        
        public Postgres()
        {
            nome = "Postgres";
            conexao = new NpgsqlConnection(conexaoString);
        }

        public string Nome
        {
            get { return nome; }
            private set { nome = value; }
        }

        public List<Produto> select()
        {
            List<Produto> produtos = new List<Produto>();
            DataTable dataTable = new DataTable();
            
            conexao.Open();
            try
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM produto", conexao))
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
                        produtos.Add(produto);
                        //Precisa fazer disposer do DataRow?
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
                dataTable.Dispose();
            }
            return produtos;
        }
        //Testar
        public Produto select(int codigo)
        {
            Produto produto;
            DataTable dataTable = new DataTable();
            conexao.Open();
            try
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM produto "+
                                    "WHERE codigo='" + codigo + "'", conexao))
                {
                    adapter.Fill(dataTable);
                    if(dataTable.Rows.Count != 1)
                        return null;

                    DataRow row = dataTable.Rows[0];
                    produto = new Produto()
                    {
                        Codigo = (int)row["codigo"],
                        Nome = (string)row["nome"],
                        Marca = (string)row["marca"],
                        Categoria = (string)row["categoria"],
                        Preco = (float)row["preco"],
                        Quantidade = (int)row["quantidade"],
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
                dataTable.Dispose();
            }
            return produto;
        }

        public bool insert(Produto produto)
        {
            int nRows;
            conexao.Open();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand(
                            "INSERT into produto " +
                                "(codigo, nome, marca, categoria, preco, quantidade) " +
                                "VALUES ('" + produto.Codigo + "', '" + produto.Nome +
                                    "', '" + produto.Marca + "', '" + produto.Categoria +
                                    "', '" + produto.Preco + "', '" + produto.Quantidade +
                                    "')", conexao))
                {
                    nRows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
            if (nRows >= 1)
                return true;
            else
                return false;
        }

        public bool delete(Produto produto)
        {
            int nRows;
            conexao.Open();
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand("DELETE FROM produto WHERE " +
                                "codigo='" + produto.Codigo + "'", conexao))
                {
                    nRows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
            if (nRows >= 1)
                return true;
            else
                return false;
        }

        public bool update(Produto produto)
        {
            int nRows;
            conexao.Open();
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand("UPDATE produto SET codigo='" +
                                    produto.Codigo + "'," + " nome='" + produto.Nome +
                                    "', marca='" + produto.Marca + "', categoria='" +
                                    produto.Categoria + "', preco='" + produto.Preco +
                                    "', quantidade='" + produto.Quantidade +
                                        "' WHERE codigo='" + produto.Codigo + "'", conexao))
                {
                    nRows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
            if (nRows >= 1)
                return true;
            else
                return false;
        }
    }
}
