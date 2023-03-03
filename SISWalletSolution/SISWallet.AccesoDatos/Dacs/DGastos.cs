namespace SISWallet.AccesoDatos
{
    using SISWallet.Entidades.Models;
    using SISWallet.AccesoDatos.Interfaces;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using SISWallet.Entidades.ModelosBindeo;

    public class DGastos : IGastosDac
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
        public DGastos(IConexionDac iConexionDac)
        {
            IConexionDac = iConexionDac;
        }
        #endregion

        #region METODO INSERTAR
        public Task<string> InsertarGastos(Gastos gasto)
        {
            string rpta = string.Empty;

            string consulta = "INSERT INTO Gastos (Id_tipo_gasto, Id_usuario, Id_turno, Fecha_gasto, Hora_gasto, " +
                "Valor_gasto, Observaciones_gasto, Estado_gasto) " +
                "VALUES(@Id_tipo_gasto, @Id_usuario, @Id_turno, @Fecha_gasto, @Hora_gasto, " +
                "@Valor_gasto, @Observaciones_gasto, @Estado_gasto) " +
                "SET @Id_gasto = SCOPE_IDENTITY() ";

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
                    CommandText = consulta,
                    CommandType = CommandType.Text,
                };

                SqlParameter Id_gasto = new()
                {
                    ParameterName = "@Id_gasto",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_gasto);

                SqlParameter Id_tipo_gasto = new()
                {
                    ParameterName = "@Id_tipo_gasto",
                    SqlDbType = SqlDbType.Int,
                    Value = gasto.Id_tipo_gasto
                };
                SqlCmd.Parameters.Add(Id_tipo_gasto);

                SqlParameter Id_usuario = new ()
                {
                    ParameterName = "@Id_usuario",
                    SqlDbType = SqlDbType.Int,
                    Value = gasto.Id_usuario
                };
                SqlCmd.Parameters.Add(Id_usuario);

                SqlParameter Id_turno = new ()
                {
                    ParameterName = "@Id_turno",
                    SqlDbType = SqlDbType.Int,
                    Value = gasto.Id_turno
                };
                SqlCmd.Parameters.Add(Id_turno);

                SqlParameter Fecha_gasto = new ()
                {
                    ParameterName = "@Fecha_gasto",
                    SqlDbType = SqlDbType.Date,
                    Value = gasto.Fecha_gasto,
                };
                SqlCmd.Parameters.Add(Fecha_gasto);

                SqlParameter Hora_gasto = new ()
                {
                    ParameterName = "@Hora_gasto",
                    SqlDbType = SqlDbType.Time,
                    Size = 2,
                    Value = gasto.Hora_gasto,
                };
                SqlCmd.Parameters.Add(Hora_gasto);

                SqlParameter Valor_gasto = new ()
                {
                    ParameterName = "@Valor_gasto",
                    SqlDbType = SqlDbType.Decimal,
                    Value = gasto.Valor_gasto,
                };
                SqlCmd.Parameters.Add(Valor_gasto);

                SqlParameter Observaciones_gasto = new ()
                {
                    ParameterName = "@Observaciones_gasto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = gasto.Observaciones_gasto.Trim()
                };
                SqlCmd.Parameters.Add(Observaciones_gasto);

                SqlParameter Estado_gasto = new ()
                {
                    ParameterName = "@Estado_gasto",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = gasto.Estado_gasto.Trim()
                };
                SqlCmd.Parameters.Add(Estado_gasto);

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
                    gasto.Id_gasto = Convert.ToInt32(SqlCmd.Parameters["@Id_gasto"].Value);
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

        #region METODO BUSCAR GASTOS
        public Task<(DataTable dtGastos, string rpta)> BuscarGastos(BusquedaBindingModel busqueda)
        {
            string rpta = "OK";

            DataTable DtResultado = new("Gastos");
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
                    CommandText = "sp_Gastos_g",
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

        #region METODO BUSCAR TIPO GASTOS
        public Task<(DataTable dtGastos, string rpta)> BuscarTipoGastos(string tipo_busqueda, string texto_busqueda)
        {
            string rpta = "OK";

            DataTable DtResultado = new("TipoGastos");
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
                    CommandText = "sp_Tipo_gastos_g",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Tipo_busqueda = new()
                {
                    ParameterName = "@Tipo_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = tipo_busqueda.Trim()
                };
                Sqlcmd.Parameters.Add(Tipo_busqueda);

                SqlParameter Texto_busqueda = new()
                {
                    ParameterName = "@Texto_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = texto_busqueda.Trim()
                };
                Sqlcmd.Parameters.Add(Texto_busqueda);

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
