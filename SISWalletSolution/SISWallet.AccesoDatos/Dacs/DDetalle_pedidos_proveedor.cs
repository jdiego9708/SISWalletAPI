using System.Data.SqlClient;
using System.Data;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Modelos;

namespace SISWallet.AccesoDatos.Dacs
{
    public class DDetalle_pedidos_proveedor : IDetalle_pedidos_proveedorDac
    {
        #region CONSTRUCTOR
        private readonly IConexionDac IConexionDac;
        public DDetalle_pedidos_proveedor(IConexionDac iConexionDac)
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

        #region METODO INSERTAR PEDIDOS
        public Task<string> InsertarDetallePedidoProveedor(Detalle_pedido_proveedor detallePedidoProveedor)
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
                    CommandText = "sp_Detalle_pedido_proveedor_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_detalle_pedido_proveedor = new()
                {
                    ParameterName = "@Id_detalle_pedido_proveedor",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_detalle_pedido_proveedor);

                SqlParameter Id_pedido_proveedor = new()
                {
                    ParameterName = "@Id_pedido_proveedor",
                    SqlDbType = SqlDbType.Int,
                    Value = detallePedidoProveedor.Id_pedido_proveedor,
                };
                SqlCmd.Parameters.Add(Id_pedido_proveedor);

                SqlParameter Id_producto = new()
                {
                    ParameterName = "@Id_producto",
                    SqlDbType = SqlDbType.Int,
                    Value = detallePedidoProveedor.Id_producto,
                };
                SqlCmd.Parameters.Add(Id_producto);

                SqlParameter Fecha_ingreso = new()
                {
                    ParameterName = "@Fecha_ingreso",
                    SqlDbType = SqlDbType.Date,
                    Value = detallePedidoProveedor.Fecha_ingreso,
                };
                SqlCmd.Parameters.Add(Fecha_ingreso);

                SqlParameter Hora_ingreso = new()
                {
                    ParameterName = "@Hora_ingreso",
                    SqlDbType = SqlDbType.Time,
                    Value = detallePedidoProveedor.Hora_ingreso,
                };
                SqlCmd.Parameters.Add(Hora_ingreso);

                SqlParameter Observaciones = new()
                {
                    ParameterName = "@Observaciones",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 100,
                    Value = detallePedidoProveedor.Observaciones.Trim(),
                };
                SqlCmd.Parameters.Add(Observaciones);

                SqlParameter Estado_pedido = new()
                {
                    ParameterName = "@Estado_pedido",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = detallePedidoProveedor.Estado_pedido.Trim(),
                };
                SqlCmd.Parameters.Add(Estado_pedido);

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
                    detallePedidoProveedor.Id_detalle_pedido_proveedor = Convert.ToInt32(SqlCmd.Parameters["@Id_detalle_pedido_proveedor"].Value);
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
    }
}