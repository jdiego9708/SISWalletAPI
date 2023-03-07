using System.Data.SqlClient;
using System.Data;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;

namespace SISWallet.AccesoDatos.Dacs
{
    public class DPrecios_productos : IPrecios_productosDac
    {
        #region CONSTRUCTOR
        private readonly IConexionDac IConexionDac;
        public DPrecios_productos(IConexionDac iConexionDac)
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

        #region METODO INSERTAR PRECIO PRODUCTO
        public Task<string> InsertarPrecioProducto(Precios_productos precioProducto)
        {
            string rpta = string.Empty;

            SqlConnection sqlCon = new();
            sqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            sqlCon.FireInfoMessageEventOnUserErrors = true;

            try
            {
                sqlCon.ConnectionString = this.IConexionDac.Cn();
                sqlCon.Open();

                SqlCommand sqlCmd = new()
                {
                    Connection = sqlCon,
                    CommandText = "sp_Precios_productos_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter idPrecio = new()
                {
                    ParameterName = "@Id_precio",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                sqlCmd.Parameters.Add(idPrecio);

                SqlParameter idProducto = new()
                {
                    ParameterName = "@Id_producto",
                    SqlDbType = SqlDbType.Int,
                    Value = precioProducto.Id_producto,
                };
                sqlCmd.Parameters.Add(idProducto);

                SqlParameter fechaPrecio = new()
                {
                    ParameterName = "@Fecha_precio",
                    SqlDbType = SqlDbType.Date,
                    Value = precioProducto.Fecha_precio,
                };
                sqlCmd.Parameters.Add(fechaPrecio);

                SqlParameter horaPrecio = new()
                {
                    ParameterName = "@Hora_precio",
                    SqlDbType = SqlDbType.Time,
                    Value = precioProducto.Hora_precio,
                };
                sqlCmd.Parameters.Add(horaPrecio);

                SqlParameter valorBase = new()
                {
                    ParameterName = "@Valor_base",
                    SqlDbType = SqlDbType.Decimal,
                    Value = precioProducto.Valor_base,
                };
                sqlCmd.Parameters.Add(valorBase);

                SqlParameter valorDescuento = new()
                {
                    ParameterName = "@Valor_descuento",
                    SqlDbType = SqlDbType.Decimal,
                    Value = precioProducto.Valor_descuento,
                    IsNullable = true,
                };
                sqlCmd.Parameters.Add(valorDescuento);

                SqlParameter valorTotal = new()
                {
                    ParameterName = "@Valor_total",
                    SqlDbType = SqlDbType.Decimal,
                    Value = precioProducto.Valor_total,
                };
                sqlCmd.Parameters.Add(valorTotal);

                SqlParameter observaciones = new()
                {
                    ParameterName = "@Observaciones",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 500,
                    Value = precioProducto.Observaciones?.Trim(),
                    IsNullable = true,
                };
                sqlCmd.Parameters.Add(observaciones);

                rpta = sqlCmd.ExecuteNonQuery() >= 1 ? "OK" : "NO";

                if (rpta != "OK")
                {
                    if (this.Mensaje_error != null)
                    {
                        rpta = this.Mensaje_error;
                    }
                }
                else
                {
                    precioProducto.Id_precio = Convert.ToInt32(sqlCmd.Parameters["@Id_precio"].Value);
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
                if (sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }
            return Task.FromResult(rpta);
        }
        #endregion

        #region METODO EDITAR PRECIO PRODUCTO
        public Task<string> EditarPrecioProducto(Precios_productos precioProducto)
        {
            string rpta = string.Empty;

            SqlConnection sqlCon = new();
            sqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            sqlCon.FireInfoMessageEventOnUserErrors = true;

            try
            {
                sqlCon.ConnectionString = this.IConexionDac.Cn();
                sqlCon.Open();

                SqlCommand sqlCmd = new()
                {
                    Connection = sqlCon,
                    CommandText = "sp_Precios_productos_u",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter idPrecio = new()
                {
                    ParameterName = "@Id_precio",
                    SqlDbType = SqlDbType.Int,
                    Value = precioProducto.Id_precio,
                };
                sqlCmd.Parameters.Add(idPrecio);

                SqlParameter idProducto = new()
                {
                    ParameterName = "@Id_producto",
                    SqlDbType = SqlDbType.Int,
                    Value = precioProducto.Id_producto,
                };
                sqlCmd.Parameters.Add(idProducto);

                SqlParameter fechaPrecio = new()
                {
                    ParameterName = "@Fecha_precio",
                    SqlDbType = SqlDbType.Date,
                    Value = precioProducto.Fecha_precio,
                };
                sqlCmd.Parameters.Add(fechaPrecio);

                SqlParameter horaPrecio = new()
                {
                    ParameterName = "@Hora_precio",
                    SqlDbType = SqlDbType.Time,
                    Value = precioProducto.Hora_precio,
                };
                sqlCmd.Parameters.Add(horaPrecio);

                SqlParameter valorBase = new()
                {
                    ParameterName = "@Valor_base",
                    SqlDbType = SqlDbType.Decimal,
                    Value = precioProducto.Valor_base,
                };
                sqlCmd.Parameters.Add(valorBase);

                SqlParameter valorDescuento = new()
                {
                    ParameterName = "@Valor_descuento",
                    SqlDbType = SqlDbType.Decimal,
                    Value = precioProducto.Valor_descuento,
                    IsNullable = true,
                };
                sqlCmd.Parameters.Add(valorDescuento);

                SqlParameter valorTotal = new()
                {
                    ParameterName = "@Valor_total",
                    SqlDbType = SqlDbType.Decimal,
                    Value = precioProducto.Valor_total,
                };
                sqlCmd.Parameters.Add(valorTotal);

                SqlParameter observaciones = new()
                {
                    ParameterName = "@Observaciones",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 500,
                    Value = precioProducto.Observaciones?.Trim(),
                    IsNullable = true,
                };
                sqlCmd.Parameters.Add(observaciones);

                rpta = sqlCmd.ExecuteNonQuery() >= 1 ? "OK" : "NO";

                if (rpta != "OK")
                {
                    if (this.Mensaje_error != null)
                    {
                        rpta = this.Mensaje_error;
                    }
                }
                else
                {
                    precioProducto.Id_precio = Convert.ToInt32(sqlCmd.Parameters["@Id_precio"].Value);
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
                if (sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }
            return Task.FromResult(rpta);
        }
        #endregion

        #region METODO BUSCAR PRECIOS PRODUCTOS
        public Task<(string rpta, DataTable dt)> BuscarPreciosProductos(BusquedaBindingModel busqueda)
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
                    CommandText = "sp_Precios_productos_g",
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