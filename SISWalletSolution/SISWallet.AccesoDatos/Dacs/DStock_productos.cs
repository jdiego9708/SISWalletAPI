using System.Data.SqlClient;
using System.Data;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;

namespace SISWallet.AccesoDatos.Dacs
{
    public class DStock_productos : IStock_productosDac
    {
        #region CONSTRUCTOR
        private readonly IConexionDac IConexionDac;
        public DStock_productos(IConexionDac iConexionDac)
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

        #region METODO INSERTAR STOCK PRODUCTOS
        public Task<string> InsertarStockProducto(Stock_producto stockProducto)
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
                    CommandText = "sp_Stock_productos_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_stock = new()
                {
                    ParameterName = "@Id_stock",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_stock);

                SqlParameter Id_producto = new()
                {
                    ParameterName = "@Id_producto",
                    SqlDbType = SqlDbType.Int,
                    Value = stockProducto.Id_producto,
                };
                SqlCmd.Parameters.Add(Id_producto);

                SqlParameter Tipos_producto = new()
                {
                    ParameterName = "@Tipos_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 2000,
                    Value = stockProducto.Tipos_producto,
                };
                SqlCmd.Parameters.Add(Tipos_producto);

                SqlParameter Fecha_stock = new()
                {
                    ParameterName = "@Fecha_stock",
                    SqlDbType = SqlDbType.Date,
                    Value = stockProducto.Fecha_stock,
                };
                SqlCmd.Parameters.Add(Fecha_stock);

                SqlParameter Hora_stock = new()
                {
                    ParameterName = "@Hora_stock",
                    SqlDbType = SqlDbType.Time,
                    Value = stockProducto.Hora_stock,
                };
                SqlCmd.Parameters.Add(Hora_stock);

                SqlParameter Unidad = new()
                {
                    ParameterName = "@Unidad",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = stockProducto.Unidad.Trim(),
                };
                SqlCmd.Parameters.Add(Unidad);

                SqlParameter Cantidad = new()
                {
                    ParameterName = "@Cantidad",
                    SqlDbType = SqlDbType.Decimal,
                    Value = stockProducto.Cantidad,
                };
                SqlCmd.Parameters.Add(Cantidad);

                SqlParameter Estado_stock = new()
                {
                    ParameterName = "@Estado_stock",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = stockProducto.Estado_stock.Trim(),
                };
                SqlCmd.Parameters.Add(Estado_stock);

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
                    stockProducto.Id_stock = Convert.ToInt32(SqlCmd.Parameters["@Id_stock"].Value);
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

        #region METODO EDITAR STOCK PRODUCTO
        public Task<string> EditarStockProducto(Stock_producto stockProducto)
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
                    CommandText = "sp_StockProductos_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_stock = new()
                {
                    ParameterName = "@Id_stock",
                    SqlDbType = SqlDbType.Int,
                    Value = stockProducto.Id_stock,
                };
                SqlCmd.Parameters.Add(Id_stock);

                SqlParameter Id_producto = new()
                {
                    ParameterName = "@Id_producto",
                    SqlDbType = SqlDbType.Int,
                    Value = stockProducto.Id_producto,
                };
                SqlCmd.Parameters.Add(Id_producto);

                SqlParameter Tipos_producto = new()
                {
                    ParameterName = "@Tipos_producto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 2000,
                    Value = stockProducto.Tipos_producto,
                };
                SqlCmd.Parameters.Add(Tipos_producto);

                SqlParameter Fecha_stock = new()
                {
                    ParameterName = "@Fecha_stock",
                    SqlDbType = SqlDbType.Date,
                    Value = stockProducto.Fecha_stock,
                };
                SqlCmd.Parameters.Add(Fecha_stock);

                SqlParameter Hora_stock = new()
                {
                    ParameterName = "@Hora_stock",
                    SqlDbType = SqlDbType.Time,
                    Value = stockProducto.Hora_stock,
                };
                SqlCmd.Parameters.Add(Hora_stock);

                SqlParameter Unidad = new()
                {
                    ParameterName = "@Unidad",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = stockProducto.Unidad.Trim(),
                };
                SqlCmd.Parameters.Add(Unidad);

                SqlParameter Cantidad = new()
                {
                    ParameterName = "@Cantidad",
                    SqlDbType = SqlDbType.Decimal,
                    Value = stockProducto.Cantidad,
                };
                SqlCmd.Parameters.Add(Cantidad);

                SqlParameter Estado_stock = new()
                {
                    ParameterName = "@Estado_stock",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = stockProducto.Estado_stock.Trim(),
                };
                SqlCmd.Parameters.Add(Estado_stock);

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

        #region METODO BUSCAR STOCK PRODUCTOS
        public Task<(string rpta, DataTable dt)> BuscarStockProductos(BusquedaBindingModel busqueda)
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
                    CommandText = "sp_Stock_productos_g",
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