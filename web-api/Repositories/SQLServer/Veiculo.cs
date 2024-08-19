using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace web_api.Repositories.SQLServer
{
    public class Veiculo
    {
        private readonly SqlConnection _conn;
        private readonly SqlCommand _cmd;

        public Veiculo(string conn)
        {
            _conn = new SqlConnection(conn);
            _cmd = new SqlCommand();
            _cmd.Connection = _conn;
        }

        public List<Models.Veiculo> Select()
        {
            List<Models.Veiculo> veiculos = new List<Models.Veiculo>();
            using (_conn)
            {
                _conn.Open();

                using (_cmd)
                {
                    _cmd.CommandText = $"SELECT Id, Marca, Nome, AnoModelo, DataFabricacao, Valor, Opcionais FROM Veiculos;";

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Models.Veiculo veiculo = new Models.Veiculo();

                            veiculo.Id = (int)dr["Id"];
                            veiculo.Marca = (string)dr["Marca"];
                            veiculo.Nome = (string)dr["Nome"];
                            veiculo.AnoModelo = (int)dr["AnoModelo"];
                            veiculo.DataFabricacao = (DateTime)dr["DataFabricacao"];
                            veiculo.Valor = (decimal)dr["Valor"];
                            veiculo.Opcionais = (string)dr["Opcionais"];

                            veiculos.Add(veiculo);
                        }
                    }
                }
            }

            return (veiculos);
        }

        public Models.Veiculo Select(int Id)
        {
            Models.Veiculo veiculo = null;

            using (_conn)
            {
                _conn.Open();

                using (_cmd)
                {
                    _cmd.CommandText = $"SELECT Id, Marca, Nome, AnoModelo, DataFabricacao, Valor, Opcionais FROM Veiculos WHERE Id = @Id;";
                    _cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = Id;

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            veiculo = new Models.Veiculo();

                            veiculo.Id = (int)dr["Id"];
                            veiculo.Marca = (string)dr["Marca"];
                            veiculo.Nome = (string)dr["Nome"];
                            veiculo.AnoModelo = (int)dr["AnoModelo"];
                            veiculo.DataFabricacao = (DateTime)dr["DataFabricacao"];
                            veiculo.Valor = (decimal)dr["Valor"];
                            veiculo.Opcionais = (string)dr["Opcionais"];
                        }
                    }
                }
            }
            return veiculo;
        }

        public List<Models.Veiculo> Select(string Nome)
        {
            List<Models.Veiculo> veiculos = new List<Models.Veiculo>();

            using (_conn)
            {
                _conn.Open();

                using (_cmd)
                {
                    _cmd.CommandText = $"SELECT Id, Marca, Nome, AnoModelo, DataFabricacao, Valor, Opcionais FROM Veiculos WHERE Nome LIKE @Nome;";
                    _cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar)).Value = $"%{Nome}%";

                    using (SqlDataReader dr = _cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Models.Veiculo veiculo = new Models.Veiculo();

                            veiculo.Id = (int)dr["Id"];
                            veiculo.Marca = (string)dr["Marca"];
                            veiculo.Nome = (string)dr["Nome"];
                            veiculo.AnoModelo = (int)dr["AnoModelo"];
                            veiculo.DataFabricacao = (DateTime)dr["DataFabricacao"];
                            veiculo.Valor = (decimal)dr["Valor"];
                            veiculo.Opcionais = (string)dr["Opcionais"];

                            veiculos.Add(veiculo);
                        }
                    }
                }
            }
            return veiculos;
        }

        public bool Insert(Models.Veiculo veiculo)
        {
            using (_conn)
            {
                _conn.Open();

                using (_cmd)
                {
                    _cmd.CommandText = "INSERT Veiculos(Marca, Nome, AnoModelo, DataFabricacao, Valor, Opcionais) VALUES (@Marca, @Nome, @AnoModelo, @DataFabricacao, @Valor, @Opcionais);SELECT convert(INT,SCOPE_IDENTITY()) as Id";

                    _cmd.Parameters.Add(new SqlParameter("@Marca", SqlDbType.VarChar)).Value = veiculo.Marca;
                    _cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar)).Value = veiculo.Nome;
                    _cmd.Parameters.Add(new SqlParameter("@AnoModelo", SqlDbType.Int)).Value = veiculo.AnoModelo;
                    _cmd.Parameters.Add(new SqlParameter("@DataFabricacao", SqlDbType.Date)).Value = veiculo.DataFabricacao;
                    _cmd.Parameters.Add(new SqlParameter("@Valor", SqlDbType.Decimal)).Value = veiculo.Valor;
                    _cmd.Parameters.Add(new SqlParameter("@Opcionais", SqlDbType.VarChar)).Value = veiculo.Opcionais;

                    veiculo.Id = (int)_cmd.ExecuteScalar();
                }

            }
            return veiculo.Id != 0 ? true : false;
        }

        public bool Update(Models.Veiculo veiculo)
        {

            int linhasAfetadas = 0;

            using (_conn)
            {
                _conn.Open();

                using (_cmd)
                {
                    _cmd.CommandText = "UPDATE Veiculos SET Marca = @Marca, Nome = @Nome, AnoModelo = @AnoModelo, DataFabricacao = @DataFabricacao, Valor = @Valor, Opcionais = @Opcionais WHERE Id = @Id";

                    _cmd.Parameters.Add(new SqlParameter("@Marca", SqlDbType.VarChar)).Value = veiculo.Marca;
                    _cmd.Parameters.Add(new SqlParameter("@Nome", SqlDbType.VarChar)).Value = veiculo.Nome;
                    _cmd.Parameters.Add(new SqlParameter("@AnoModelo", SqlDbType.Int)).Value = veiculo.AnoModelo;
                    _cmd.Parameters.Add(new SqlParameter("@DataFabricacao", SqlDbType.Date)).Value = veiculo.DataFabricacao;
                    _cmd.Parameters.Add(new SqlParameter("@Valor", SqlDbType.Decimal)).Value = veiculo.Valor;
                    _cmd.Parameters.Add(new SqlParameter("@Opcionais", SqlDbType.VarChar)).Value = veiculo.Opcionais;
                    _cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = veiculo.Id;

                    linhasAfetadas = _cmd.ExecuteNonQuery();
                }

            }
            return linhasAfetadas != 0;
        }

        public bool Delete(int Id)
        {
            int linhasAfetadas = 0;

            using (_conn)
            {
                _conn.Open();

                using (_cmd)
                {
                    _cmd.CommandText = "DELETE FROM Veiculos WHERE Id = @Id";

                    _cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = Id;

                    linhasAfetadas = _cmd.ExecuteNonQuery();
                }

            }
            return linhasAfetadas == 1;
        }
    }
}