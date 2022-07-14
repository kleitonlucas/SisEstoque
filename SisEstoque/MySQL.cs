using MySqlConnector;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisEstoque
{
    internal class MySQL : IBancoDeDados
    {
        private string nome;
        private string conexaoString = "Server=localhost;Port=3306;UID=root;Password=mysql;Database=sisestoque";
        private MySqlConnection conexao;
        public MySQL()
        {
            nome = "MySQL";
            conexao = new MySqlConnection(conexaoString);
        }

        public string Nome
        {
            get { return nome; }
            private set { nome = value; }
        }

        public List<Produto> select()
        {
            List<Produto> produtos = new List<Produto>();
            conexao.Open();
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM produto", conexao))
                {
                    adapter.Fill(dataTable);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine("codRow " + row["codigo"]);
                        Produto produto = new Produto()
                        {
                            Codigo = (int)row["codigo"],
                            Nome = (string)row["nome"],
                            Marca = (string)row["marca"],
                            Categoria = (string)row["categoria"],
                            Preco = (float)Convert.ToDecimal(row["preco"]),
                            Quantidade = (int)row["quantidade"]
                        };
                        produtos.Add(produto);
                        Console.WriteLine("cod " + produto.Codigo);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conexao.Close();
                dataTable.Dispose();
            }
            return produtos;
        }

        public Produto select(int codigo)
        {
            Produto produto = new Produto();
            DataTable dataTable = new DataTable();
            conexao.Open();
            try
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM produto " +
                                    "WHERE codigo='" + codigo + "'", conexao))
                {
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count != 1)
                        return null;

                    DataRow row = dataTable.Rows[0];
                    produto.Codigo = (int)row["codigo"];
                    produto.Nome = (string)row["nome"];
                    produto.Marca = (string)row["marca"];
                    produto.Categoria = (string)row["categoria"];
                    produto.Preco = (float)row["preco"];
                    produto.Quantidade = (int)row["quantidade"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
            int nRows = 0;
            conexao.Open();
            try
            {
                using(MySqlCommand command = new MySqlCommand(
                                "INSERT into produto " +
                                "(codigo, nome, marca, categoria, preco, quantidade) " +
                                "VALUES ('" + produto.Codigo + "', '" + produto.Nome +
                                    "', '" + produto.Marca + "', '" + produto.Categoria +
                                    "', '" + produto.Preco + "', '" + produto.Quantidade +
                                    "')", conexao))
                {
                    //Verificar
                    nRows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
                using (MySqlCommand command = new MySqlCommand(
                                "DELETE FROM produto WHERE " +
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
            int nRows = 0;
            conexao.Open();
            try
            {
                using (MySqlCommand command = new MySqlCommand("UPDATE produto SET codigo='" +
                                    produto.Codigo + "'," + " nome='" + produto.Nome +
                                    "', marca='" + produto.Marca + "', categoria='" +
                                    produto.Categoria + "', preco='" + produto.Preco +
                                    "', quantidade='" + produto.Quantidade +
                                        "' WHERE codigo='" + produto.Codigo + "'", conexao))
                {
                    nRows = command.ExecuteNonQuery();
                    if (nRows >= 1)
                        Console.WriteLine("Atualizaçã de produto realizada!");
                    else
                        Console.WriteLine("Falha na atualização do produto!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
