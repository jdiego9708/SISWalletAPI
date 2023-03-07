using System.Data.SqlClient;
using System.Data;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;

namespace SISWallet.AccesoDatos.Dacs
{
    public class DPedidos_proveedor : IPedidos_proveedorDac
    {
        #region CONSTRUCTOR
        private readonly IConexionDac IConexionDac;
        public DPedidos_proveedor(IConexionDac iConexionDac)
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

        #region METODO INSERTAR PEDIDO PROVEEDOR
        public Task<string> InsertarPedidoProveedor(Pedidos_proveedor pedidoProveedor)
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
                    CommandText = "sp_Pedidos_proveedor_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_pedido_proveedor = new()
                {
                    ParameterName = "@Id_pedido_proveedor",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_pedido_proveedor);

                SqlParameter Id_empleado = new()
                {
                    ParameterName = "@Id_empleado",
                    SqlDbType = SqlDbType.Int,
                    Value = pedidoProveedor.Id_empleado,
                };
                SqlCmd.Parameters.Add(Id_empleado);

                SqlParameter Id_proveedor = new()
                {
                    ParameterName = "@Id_proveedor",
                    SqlDbType = SqlDbType.Int,
                    Value = pedidoProveedor.Id_proveedor,
                };
                SqlCmd.Parameters.Add(Id_proveedor);

                SqlParameter Fecha_pedido = new()
                {
                    ParameterName = "@Fecha_pedido",
                    SqlDbType = SqlDbType.Date,
                    Value = pedidoProveedor.Fecha_pedido,
                };
                SqlCmd.Parameters.Add(Fecha_pedido);

                SqlParameter Hora_pedido = new()
                {
                    ParameterName = "@Hora_pedido",
                    SqlDbType = SqlDbType.Time,
                    Value = pedidoProveedor.Hora_pedido,
                };
                SqlCmd.Parameters.Add(Hora_pedido);

                SqlParameter Observaciones_pedido = new()
                {
                    ParameterName = "@Observaciones_pedido",
                    SqlDbType = SqlDbType.VarChar,
                    Size = -1,
                    Value = pedidoProveedor.Observaciones_pedido ?? string.Empty,
                };
                SqlCmd.Parameters.Add(Observaciones_pedido);

                SqlParameter Estado_pedido_proveedor = new()
                {
                    ParameterName = "@Estado_pedido_proveedor",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = pedidoProveedor.Estado_pedido_proveedor ?? string.Empty,
                };
                SqlCmd.Parameters.Add(Estado_pedido_proveedor);

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
                    pedidoProveedor.Id_pedido_proveedor = Convert.ToInt32(SqlCmd.Parameters["@Id_pedido_proveedor"].Value);
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

        #region METODO EDITAR PEDIDO PROVEEDOR
        public Task<string> EditarPedidoProveedor(Pedidos_proveedor pedidoProveedor)
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
                    CommandText = "sp_Pedidos_proveedor_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_pedido_proveedor = new()
                {
                    ParameterName = "@Id_pedido_proveedor",
                    SqlDbType = SqlDbType.Int,
                    Value =  pedidoProveedor.Id_pedido_proveedor,
                };
                SqlCmd.Parameters.Add(Id_pedido_proveedor);

                SqlParameter Id_empleado = new()
                {
                    ParameterName = "@Id_empleado",
                    SqlDbType = SqlDbType.Int,
                    Value = pedidoProveedor.Id_empleado,
                };
                SqlCmd.Parameters.Add(Id_empleado);

                SqlParameter Id_proveedor = new()
                {
                    ParameterName = "@Id_proveedor",
                    SqlDbType = SqlDbType.Int,
                    Value = pedidoProveedor.Id_proveedor,
                };
                SqlCmd.Parameters.Add(Id_proveedor);

                SqlParameter Fecha_pedido = new()
                {
                    ParameterName = "@Fecha_pedido",
                    SqlDbType = SqlDbType.Date,
                    Value = pedidoProveedor.Fecha_pedido,
                };
                SqlCmd.Parameters.Add(Fecha_pedido);

                SqlParameter Hora_pedido = new()
                {
                    ParameterName = "@Hora_pedido",
                    SqlDbType = SqlDbType.Time,
                    Value = pedidoProveedor.Hora_pedido,
                };
                SqlCmd.Parameters.Add(Hora_pedido);

                SqlParameter Observaciones_pedido = new()
                {
                    ParameterName = "@Observaciones_pedido",
                    SqlDbType = SqlDbType.VarChar,
                    Size = -1,
                    Value = pedidoProveedor.Observaciones_pedido ?? string.Empty,
                };
                SqlCmd.Parameters.Add(Observaciones_pedido);

                SqlParameter Estado_pedido_proveedor = new()
                {
                    ParameterName = "@Estado_pedido_proveedor",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = pedidoProveedor.Estado_pedido_proveedor ?? string.Empty,
                };
                SqlCmd.Parameters.Add(Estado_pedido_proveedor);

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

        #region METODO BUSCAR PEDIDOS PROVEEDORES
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
                    CommandText = "sp_Pedidos_proveedores_g",
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