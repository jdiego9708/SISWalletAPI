namespace SISWallet.AccesoDatos
{
    using SISWallet.Entidades.Models;
    using SISWallet.AccesoDatos.Interfaces;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using SISWallet.Entidades.ModelosBindeo;

    public class DReglas : IReglasDac
    {
        #region MENSAJE
        private void SqlCon_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            this.Mensaje_respuesta = e.Message;
        }
        #endregion

        #region VARIABLES
        string _mensaje_respuesta;
        public string Mensaje_respuesta
        {
            get
            {
                return _mensaje_respuesta;
            }

            set
            {
                _mensaje_respuesta = value;
            }
        }
        #endregion

        #region CONSTRUCTOR
        private readonly IConexionDac IConexionDac;
        public DReglas(IConexionDac iConexionDac)
        {
            IConexionDac = iConexionDac;
        }
        #endregion

        #region METODO INSERTAR
        public Task<string> InsertarRegla(Reglas regla)
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
                    CommandText = "sp_Reglas_i",
                    CommandType = CommandType.StoredProcedure,
                };

                SqlParameter Id_regla = new()
                {
                    ParameterName = "@Id_regla",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_regla);

                SqlParameter Id_cobro = new()
                {
                    ParameterName = "@Id_cobro",
                    SqlDbType = SqlDbType.Int,
                    Value = regla.Id_cobro
                };
                SqlCmd.Parameters.Add(Id_cobro);

                SqlParameter Nombre_regla = new()
                {
                    ParameterName = "@Nombre_regla",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = regla.Nombre_regla.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Nombre_regla);

                SqlParameter Descripcion_regla = new()
                {
                    ParameterName = "@Descripcion_regla",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 500,
                    Value = regla.Descripcion_regla.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Descripcion_regla);

                SqlParameter Valor_regla = new()
                {
                    ParameterName = "@Valor_regla",
                    SqlDbType = SqlDbType.Decimal,
                    Value = regla.Valor_regla
                };
                SqlCmd.Parameters.Add(Valor_regla);

                SqlParameter Estado_regla = new()
                {
                    ParameterName = "@Estado_regla",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = regla.Estado_regla.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Estado_regla);

                SqlParameter Tipo_usuario_regla = new()
                {
                    ParameterName = "@Tipo_usuario_regla",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = regla.Tipo_usuario_regla.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Tipo_usuario_regla);

                rpta = SqlCmd.ExecuteNonQuery() >= 1 ? "OK" : "NO SE INGRESÓ";
                if (!rpta.Equals("OK"))
                {
                    if (this.Mensaje_respuesta != null)
                    {
                        rpta = this.Mensaje_respuesta;
                    }
                }
                else
                {
                    regla.Id_regla = Convert.ToInt32(SqlCmd.Parameters["@Id_regla"].Value);
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

        #region METODO EDITAR
        public Task<string> EditarRegla(Reglas regla)
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
                    CommandText = "sp_Reglas_u",
                    CommandType = CommandType.StoredProcedure,
                };

                SqlParameter Id_regla = new()
                {
                    ParameterName = "@Id_regla",
                    SqlDbType = SqlDbType.Int,
                    Value = regla.Id_regla,
                };
                SqlCmd.Parameters.Add(Id_regla);

                SqlParameter Id_cobro = new()
                {
                    ParameterName = "@Id_cobro",
                    SqlDbType = SqlDbType.Int,
                    Value = regla.Id_cobro
                };
                SqlCmd.Parameters.Add(Id_cobro);

                SqlParameter Nombre_regla = new()
                {
                    ParameterName = "@Nombre_regla",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = regla.Nombre_regla.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Nombre_regla);

                SqlParameter Descripcion_regla = new()
                {
                    ParameterName = "@Descripcion_regla",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 500,
                    Value = regla.Descripcion_regla.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Descripcion_regla);

                SqlParameter Valor_regla = new()
                {
                    ParameterName = "@Valor_regla",
                    SqlDbType = SqlDbType.Decimal,
                    Value = regla.Valor_regla
                };
                SqlCmd.Parameters.Add(Valor_regla);

                SqlParameter Estado_regla = new()
                {
                    ParameterName = "@Estado_regla",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = regla.Estado_regla.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Estado_regla);

                SqlParameter Tipo_usuario_regla = new()
                {
                    ParameterName = "@Tipo_usuario_regla",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = regla.Tipo_usuario_regla.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Tipo_usuario_regla);

                rpta = SqlCmd.ExecuteNonQuery() >= 1 ? "OK" : "NO SE INGRESÓ";
                if (!rpta.Equals("OK"))
                {
                    if (this.Mensaje_respuesta != null)
                    {
                        rpta = this.Mensaje_respuesta;
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

        #region METODO BUSCAR REGLAS
        public Task<(DataTable dt, string rpta)> BuscarReglas(BusquedaBindingModel busqueda)
        {
            string rpta = "OK";

            DataTable DtResultado = new("dtReglas");
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
                    CommandText = "sp_Reglas_g",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Tipo_busqueda = new()
                {
                    ParameterName = "@Tipo_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = busqueda.Tipo_busqueda.Trim()
                };
                Sqlcmd.Parameters.Add(Tipo_busqueda);

                SqlParameter Texto_busqueda1 = new()
                {
                    ParameterName = "@Texto_busqueda1",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = busqueda.Texto_busqueda1.Trim()
                };
                Sqlcmd.Parameters.Add(Texto_busqueda1);

                SqlParameter Texto_busqueda2 = new()
                {
                    ParameterName = "@Texto_busqueda2",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = busqueda.Texto_busqueda2 ?? ""
                };
                Sqlcmd.Parameters.Add(Texto_busqueda2);

                SqlDataAdapter SqlData = new(Sqlcmd);
                SqlData.Fill(DtResultado);

                if (DtResultado.Rows.Count < 1)
                    DtResultado = null;
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
                DtResultado = null;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                DtResultado = null;
            }
            return Task.FromResult((DtResultado, rpta));
        }
        #endregion
    }
}
