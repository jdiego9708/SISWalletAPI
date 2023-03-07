using System.Data.SqlClient;
using System.Data;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;

namespace SISWallet.AccesoDatos.Dacs
{
    public class DProductos : IProductosDac
    {
        #region CONSTRUCTOR
        private readonly IConexionDac IConexionDac;
        public DProductos(IConexionDac iConexionDac)
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

        #region METODO INSERTAR PRODUCTO
        public Task<string> InsertarProducto(Productos producto)
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
                    CommandText = "sp_Productos_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_producto = new()
                {
                    ParameterName = "@Id_producto",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_producto);

                SqlParameter Id_tipo_producto = new()
                {
                    ParameterName = "@Id_tipo_producto",
                    SqlDbType = SqlDbType.Int,
                    Value = producto.Id_tipo_producto,
                };
                SqlCmd.Parameters.Add(Id_tipo_producto);

                SqlParameter Nombre_producto = new()
                {
                    ParameterName = "@Nombre_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = producto.Nombre_producto.Trim(),
                };
                SqlCmd.Parameters.Add(Nombre_producto);

                SqlParameter Precio_producto = new()
                {
                    ParameterName = "@Precio_producto",
                    SqlDbType = SqlDbType.Decimal,
                    Value = producto.Precio_producto,
                };
                SqlCmd.Parameters.Add(Precio_producto);

                SqlParameter Descripcion_producto = new()
                {
                    ParameterName = "@Descripcion_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 200,
                    Value = producto.Descripcion_producto.ToUpper().Trim(),
                };
                SqlCmd.Parameters.Add(Descripcion_producto);

                SqlParameter Estado_producto = new()
                {
                    ParameterName = "@Estado_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = producto.Estado_producto.ToUpper().Trim(),
                };
                SqlCmd.Parameters.Add(Estado_producto);

                SqlParameter Fecha = new()
                {
                    ParameterName = "@Fecha",
                    SqlDbType = SqlDbType.Date,
                    Value = DateTime.Now.ToString("yyyy-MM-dd"),
                };
                SqlCmd.Parameters.Add(Fecha);

                SqlParameter Hora = new()
                {
                    ParameterName = "@Hora",
                    SqlDbType = SqlDbType.Time,
                    Value = DateTime.Now.TimeOfDay,
                };
                SqlCmd.Parameters.Add(Hora);

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
                    producto.Id_producto = Convert.ToInt32(SqlCmd.Parameters["@Id_producto"].Value);
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

        #region METODO EDITAR PRODUCTO
        public Task<string> EditarProducto(Productos producto)
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
                    CommandText = "sp_Productos_u",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_producto = new()
                {
                    ParameterName = "@Id_producto",
                    SqlDbType = SqlDbType.Int,
                    Value = producto.Id_producto,
                };
                SqlCmd.Parameters.Add(Id_producto);

                SqlParameter Id_tipo_producto = new()
                {
                    ParameterName = "@Id_tipo_producto",
                    SqlDbType = SqlDbType.Int,
                    Value = producto.Id_tipo_producto,
                };
                SqlCmd.Parameters.Add(Id_tipo_producto);

                SqlParameter Nombre_producto = new()
                {
                    ParameterName = "@Nombre_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = producto.Nombre_producto.Trim(),
                };
                SqlCmd.Parameters.Add(Nombre_producto);

                SqlParameter Precio_producto = new()
                {
                    ParameterName = "@Precio_producto",
                    SqlDbType = SqlDbType.Decimal,
                    Value = producto.Precio_producto,
                };
                SqlCmd.Parameters.Add(Precio_producto);

                SqlParameter Descripcion_producto = new()
                {
                    ParameterName = "@Descripcion_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 200,
                    Value = producto.Descripcion_producto.ToUpper().Trim(),
                };
                SqlCmd.Parameters.Add(Descripcion_producto);

                SqlParameter Estado_producto = new()
                {
                    ParameterName = "@Estado_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = producto.Estado_producto.ToUpper().Trim(),
                };
                SqlCmd.Parameters.Add(Estado_producto);

                SqlParameter Fecha = new()
                {
                    ParameterName = "@Fecha",
                    SqlDbType = SqlDbType.Date,
                    Value = DateTime.Now.ToString("yyyy-MM-dd"),
                };
                SqlCmd.Parameters.Add(Fecha);

                SqlParameter Hora = new()
                {
                    ParameterName = "@Hora",
                    SqlDbType = SqlDbType.Time,
                    Value = DateTime.Now.TimeOfDay,
                };
                SqlCmd.Parameters.Add(Hora);

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

        #region METODO BUSCAR PRODUCTOS
        public Task<(string rpta, DataTable dt)> BuscarProductos(BusquedaBindingModel busqueda)
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
                    CommandText = "sp_Productos_g",
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