using System.Data.SqlClient;
using System.Data;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;

namespace SISWallet.AccesoDatos.Dacs
{
    public class DNovedades : INovedadesDac
    {
        #region CONSTRUCTOR
        private readonly IConexionDac IConexionDac;
        public DNovedades(IConexionDac iConexionDac)
        {
            IConexionDac = iConexionDac;
        }
        #endregion

        #region MENSAJE SQL
        private void SqlCon_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            this.Mensaje_error = e.Message;
        }
        #endregion

        #region PROPIEDADES
        private string mensaje_error;
        public string Mensaje_error
        {
            get
            {
                return mensaje_error;
            }

            set
            {
                mensaje_error = value;
            }
        }
        #endregion

        #region METODO INSERTAR NOVEDAD
        public Task<string> InsertarNovedad(Novedades novedad)
        {
            string rpta = string.Empty;

            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;

            try
            {
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();

                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Novedades_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_novedad = new()
                {
                    ParameterName = "@Id_novedad",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_novedad);

                SqlParameter Id_stock = new()
                {
                    ParameterName = "@Id_stock",
                    SqlDbType = SqlDbType.Int,
                    Value = novedad.Id_stock,
                };
                SqlCmd.Parameters.Add(Id_stock);

                SqlParameter Fecha_novedad = new()
                {
                    ParameterName = "@Fecha_novedad",
                    SqlDbType = SqlDbType.DateTime,
                    Value = novedad.Fecha_novedad,
                };
                SqlCmd.Parameters.Add(Fecha_novedad);

                SqlParameter Hora_novedad = new()
                {
                    ParameterName = "@Hora_novedad",
                    SqlDbType = SqlDbType.Time,
                    Value = novedad.Hora_novedad,
                };
                SqlCmd.Parameters.Add(Hora_novedad);

                SqlParameter Valor_novedad = new()
                {
                    ParameterName = "@Valor_novedad",
                    SqlDbType = SqlDbType.Decimal,
                    Value = novedad.Valor_novedad,
                };
                SqlCmd.Parameters.Add(Valor_novedad);

                SqlParameter Observaciones_novedad = new()
                {
                    ParameterName = "@Observaciones_novedad",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = novedad.Observaciones_novedad.Trim(),
                };
                SqlCmd.Parameters.Add(Observaciones_novedad);

                SqlParameter Estado_novedad = new()
                {
                    ParameterName = "@Estado_novedad",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = novedad.Estado_novedad.Trim(),
                };
                SqlCmd.Parameters.Add(Estado_novedad);

                rpta = SqlCmd.ExecuteNonQuery() >= 1 ? "OK" : "NO";

                if (rpta != "OK")
                {
                    if (this.Mensaje_error != null)
                    {
                        rpta = this.Mensaje_error;
                    }
                }
                else
                {
                    novedad.Id_novedad = Convert.ToInt32(SqlCmd.Parameters["@Id_novedad"].Value);
                }
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Task.FromResult(rpta);
        }
        #endregion

        #region METODO EDITAR NOVEDAD
        public Task<string> EditarNovedad(Novedades novedad)
        {
            string rpta = string.Empty;

            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;

            try
            {
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();

                SqlCommand SqlCmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Novedades_u",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_novedad = new()
                {
                    ParameterName = "@Id_novedad",
                    SqlDbType = SqlDbType.Int,
                    Value = novedad.Id_novedad,
                };
                SqlCmd.Parameters.Add(Id_novedad);

                SqlParameter Id_stock = new()
                {
                    ParameterName = "@Id_stock",
                    SqlDbType = SqlDbType.Int,
                    Value = novedad.Id_stock,
                };
                SqlCmd.Parameters.Add(Id_stock);

                SqlParameter Fecha_novedad = new()
                {
                    ParameterName = "@Fecha_novedad",
                    SqlDbType = SqlDbType.DateTime,
                    Value = novedad.Fecha_novedad,
                };
                SqlCmd.Parameters.Add(Fecha_novedad);

                SqlParameter Hora_novedad = new()
                {
                    ParameterName = "@Hora_novedad",
                    SqlDbType = SqlDbType.Time,
                    Value = novedad.Hora_novedad,
                };
                SqlCmd.Parameters.Add(Hora_novedad);

                SqlParameter Valor_novedad = new()
                {
                    ParameterName = "@Valor_novedad",
                    SqlDbType = SqlDbType.Decimal,
                    Value = novedad.Valor_novedad,
                };
                SqlCmd.Parameters.Add(Valor_novedad);

                SqlParameter Observaciones_novedad = new()
                {
                    ParameterName = "@Observaciones_novedad",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = novedad.Observaciones_novedad.Trim(),
                };
                SqlCmd.Parameters.Add(Observaciones_novedad);

                SqlParameter Estado_novedad = new()
                {
                    ParameterName = "@Estado_novedad",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = novedad.Estado_novedad.Trim(),
                };
                SqlCmd.Parameters.Add(Estado_novedad);

                rpta = SqlCmd.ExecuteNonQuery() >= 1 ? "OK" : "NO";

                if (rpta != "OK")
                {
                    if (this.Mensaje_error != null)
                    {
                        rpta = this.Mensaje_error;
                    }
                }
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Task.FromResult(rpta);
        }
        #endregion

        #region METODO BUSCAR NOVEDADES
        public Task<(string rpta, DataTable dt)> BuscarNovedades(BusquedaBindingModel busqueda)
        {
            string rpta = "OK";
            DataTable dt = new();

            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;

            try
            {
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();

                SqlCommand Sqlcmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Novedades_g",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Tipo_busqueda = new()
                {
                    ParameterName = "@Tipo_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = busqueda.Tipo_busqueda.Trim().ToUpper()
                };
                Sqlcmd.Parameters.Add(Tipo_busqueda);

                SqlParameter Texto_busqueda1 = new()
                {
                    ParameterName = "@Texto_busqueda1",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = busqueda.Texto_busqueda1.Trim().ToUpper()
                };
                Sqlcmd.Parameters.Add(Texto_busqueda1);

                SqlParameter Texto_busqueda2 = new()
                {
                    ParameterName = "@Texto_busqueda2",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = busqueda.Texto_busqueda2.Trim().ToUpper()
                };
                Sqlcmd.Parameters.Add(Texto_busqueda2);

                SqlDataAdapter SqlData = new(Sqlcmd);
                SqlData.Fill(dt);

                if (dt.Rows.Count < 1)
                    dt = null;
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
                dt = null;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                dt = null;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Task.FromResult((rpta, dt));
        }
        #endregion
    }
}