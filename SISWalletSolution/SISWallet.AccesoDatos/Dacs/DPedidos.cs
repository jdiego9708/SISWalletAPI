using System.Data.SqlClient;
using System.Data;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;

namespace SISWallet.AccesoDatos.Dacs
{
    public class DPedidos : IPedidosDac
    {
        #region CONSTRUCTOR
        private readonly IConexionDac IConexionDac;
        public DPedidos(IConexionDac iConexionDac)
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
        public Task<string> InsertarPedidos(Pedidos pedido)
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
                    CommandText = "sp_Pedidos_i",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_pedido = new()
                {
                    ParameterName = "@Id_pedido",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_pedido);

                SqlParameter Fecha_pedido = new()
                {
                    ParameterName = "@Fecha_pedido",
                    SqlDbType = SqlDbType.Date,
                    Value = pedido.Fecha_pedido.ToString("yyyy-MM-dd"),
                };
                SqlCmd.Parameters.Add(Fecha_pedido);

                SqlParameter Hora_pedido = new()
                {
                    ParameterName = "@Hora_pedido",
                    SqlDbType = SqlDbType.Time,
                    Value = pedido.Hora_pedido,
                };
                SqlCmd.Parameters.Add(Hora_pedido);

                SqlParameter Id_tipo_pedido = new()
                {
                    ParameterName = "@Id_tipo_pedido",
                    SqlDbType = SqlDbType.Int,
                    Value = pedido.Id_tipo_pedido,
                };
                SqlCmd.Parameters.Add(Id_tipo_pedido);

                SqlParameter Id_cliente = new()
                {
                    ParameterName = "@Id_cliente",
                    SqlDbType = SqlDbType.Int,
                    Value = pedido.Id_cliente,
                };
                SqlCmd.Parameters.Add(Id_cliente);

                SqlParameter Id_empleado = new()
                {
                    ParameterName = "@Id_empleado",
                    SqlDbType = SqlDbType.Int,
                    Value = pedido.Id_empleado,
                };
                SqlCmd.Parameters.Add(Id_empleado);

                SqlParameter Observaciones_pedido = new()
                {
                    ParameterName = "@Observaciones_pedido",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 200,
                    Value = pedido.Observaciones_pedido.ToUpper().Trim(),
                };
                SqlCmd.Parameters.Add(Observaciones_pedido);

                SqlParameter Informacion_adicional = new()
                {
                    ParameterName = "@Informacion_adicional",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 200,
                    Value = pedido.Informacion_adicional.ToUpper().Trim(),
                };
                SqlCmd.Parameters.Add(Informacion_adicional);

                SqlParameter Estado_pedido = new()
                {
                    ParameterName = "@Estado_pedido",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = pedido.Estado_pedido.ToUpper().Trim(),
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
                    pedido.Id_pedido = Convert.ToInt32(SqlCmd.Parameters["@Id_pedido"].Value);
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

        #region METODO BUSCAR PEDIDOS
        public Task<(string rpta, DataTable dt)> BuscarPedidos(BusquedaBindingModel busqueda)
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
                    CommandText = "sp_Pedidos_g",
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