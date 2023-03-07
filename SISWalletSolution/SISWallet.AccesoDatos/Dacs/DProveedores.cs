using System.Data.SqlClient;
using System.Data;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;

namespace SISWallet.AccesoDatos.Dacs
{
    public class DProveedores : IProveedoresDac
    {
        #region CONSTRUCTOR
        private readonly IConexionDac IConexionDac;
        public DProveedores(IConexionDac iConexionDac)
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

        #region METODO INSERTAR PROVEEDOR
        public Task<string> InsertarProveedor(Proveedores proveedor)
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
                    CommandText = "sp_Proveedores_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_proveedor = new()
                {
                    ParameterName = "@Id_proveedor",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_proveedor);

                SqlParameter Fecha_ingreso = new()
                {
                    ParameterName = "@Fecha_ingreso",
                    SqlDbType = SqlDbType.DateTime,
                    Value = proveedor.Fecha_ingreso,
                };
                SqlCmd.Parameters.Add(Fecha_ingreso);

                SqlParameter Nombre_proveedor = new()
                {
                    ParameterName = "@Nombre_proveedor",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = proveedor.Nombre_proveedor.Trim(),
                };
                SqlCmd.Parameters.Add(Nombre_proveedor);

                SqlParameter Descripcion_proveedor = new()
                {
                    ParameterName = "@Descripcion_proveedor",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = proveedor.Descripcion_proveedor.Trim(),
                };
                SqlCmd.Parameters.Add(Descripcion_proveedor);

                SqlParameter Contacto_proveedor = new()
                {
                    ParameterName = "@Contacto_proveedor",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = proveedor.Contacto_proveedor.Trim(),
                };
                SqlCmd.Parameters.Add(Contacto_proveedor);

                SqlParameter Estado_proveedor = new()
                {
                    ParameterName = "@Estado_proveedor",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 20,
                    Value = proveedor.Estado_proveedor.Trim(),
                };
                SqlCmd.Parameters.Add(Estado_proveedor);

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
                    proveedor.Id_proveedor = Convert.ToInt32(SqlCmd.Parameters["@Id_proveedor"].Value);
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

        #region METODO EDITAR PROVEEDOR
        public Task<string> EditarProveedor(Proveedores proveedor)
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
                    CommandText = "sp_Proveedores_u",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_proveedor = new()
                {
                    ParameterName = "@Id_proveedor",
                    SqlDbType = SqlDbType.Int,
                    Value = proveedor.Id_proveedor,
                };
                SqlCmd.Parameters.Add(Id_proveedor);

                SqlParameter Fecha_ingreso = new()
                {
                    ParameterName = "@Fecha_ingreso",
                    SqlDbType = SqlDbType.DateTime,
                    Value = proveedor.Fecha_ingreso,
                };
                SqlCmd.Parameters.Add(Fecha_ingreso);

                SqlParameter Nombre_proveedor = new()
                {
                    ParameterName = "@Nombre_proveedor",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = proveedor.Nombre_proveedor.Trim(),
                };
                SqlCmd.Parameters.Add(Nombre_proveedor);

                SqlParameter Descripcion_proveedor = new()
                {
                    ParameterName = "@Descripcion_proveedor",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = proveedor.Descripcion_proveedor.Trim(),
                };
                SqlCmd.Parameters.Add(Descripcion_proveedor);

                SqlParameter Contacto_proveedor = new()
                {
                    ParameterName = "@Contacto_proveedor",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = proveedor.Contacto_proveedor.Trim(),
                };
                SqlCmd.Parameters.Add(Contacto_proveedor);

                SqlParameter Estado_proveedor = new()
                {
                    ParameterName = "@Estado_proveedor",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 20,
                    Value = proveedor.Estado_proveedor.Trim(),
                };
                SqlCmd.Parameters.Add(Estado_proveedor);

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

        #region METODO BUSCAR PROVEEDORES
        public Task<(string rpta, DataTable dt)> BuscarProveedores(BusquedaBindingModel busqueda)
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
                    CommandText = "sp_Proveedores_g",
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