namespace SISWallet.AccesoDatos
{
    using SISWallet.AccesoDatos.Interfaces;
    using SISWallet.Entidades.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class DTurnos : ITurnosDac
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
        public DTurnos(IConexionDac iConexionDac)
        {
            IConexionDac = iConexionDac;
        }
        #endregion

        #region METODO INSERTAR
        public Task<string> InsertarTurno(Turnos turno)
        {
            int contador = 0;
            string rpta = string.Empty;

            string consulta = "INSERT INTO Turnos (Id_cobrador, Id_cobro, Fecha_inicio_turno, Fecha_fin_turno, Hora_inicio_turno, Hora_fin_turno, " +
                "Valor_inicial, Clientes_iniciales, Clientes_nuevos, Clientes_cancelados, Clientes_total, Recaudo_ventas_nuevas, Recaudo_cuotas, " +
                "Recaudo_otros, Recaudo_pretendido_turno, Recaudo_real, Gastos_total, Estado_turno) " +
                "VALUES(@Id_cobrador, @Id_cobro, @Fecha_inicio_turno, @Fecha_fin_turno, @Hora_inicio_turno, @Hora_fin_turno, " +
                "@Valor_inicial, @Clientes_iniciales, @Clientes_nuevos, @Clientes_cancelados, @Clientes_total, @Recaudo_ventas_nuevas, @Recaudo_cuotas, " +
                "@Recaudo_otros, @Recaudo_pretendido_turno, @Recaudo_real, @Gastos_total, @Estado_turno) " +
                "SET @Id_turno = SCOPE_IDENTITY() ";

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
                    CommandType = CommandType.Text
                };

                #region VARIABLES
                SqlParameter Id_turno = new()
                {
                    ParameterName = "@Id_turno",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_turno);

                SqlParameter Id_cobrador = new()
                {
                    ParameterName = "@Id_cobrador",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Id_cobrador
                };
                SqlCmd.Parameters.Add(Id_cobrador);
                contador += 1;

                SqlParameter Id_cobro = new()
                {
                    ParameterName = "@Id_cobro",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Id_cobro
                };
                SqlCmd.Parameters.Add(Id_cobro);
                contador += 1;

                SqlParameter Fecha_inicio_turno = new()
                {
                    ParameterName = "@Fecha_inicio_turno",
                    SqlDbType = SqlDbType.Date,
                    Value = turno.Fecha_inicio_turno,
                };
                SqlCmd.Parameters.Add(Fecha_inicio_turno);
                contador += 1;

                SqlParameter Fecha_fin_turno = new()
                {
                    ParameterName = "@Fecha_fin_turno",
                    SqlDbType = SqlDbType.Date,
                    Value = turno.Fecha_fin_turno,
                };
                SqlCmd.Parameters.Add(Fecha_fin_turno);
                contador += 1;

                SqlParameter Hora_inicio_turno = new()
                {
                    ParameterName = "@Hora_inicio_turno",
                    SqlDbType = SqlDbType.Time,
                    Value = turno.Hora_inicio_turno,
                };
                SqlCmd.Parameters.Add(Hora_inicio_turno);
                contador += 1;

                SqlParameter Hora_fin_turno = new()
                {
                    ParameterName = "@Hora_fin_turno",
                    SqlDbType = SqlDbType.Time,
                    Size = 2,
                    Value = turno.Hora_fin_turno,
                };
                SqlCmd.Parameters.Add(Hora_fin_turno);
                contador += 1;

                SqlParameter Valor_inicial = new()
                {
                    ParameterName = "@Valor_inicial",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Valor_inicial,
                };
                SqlCmd.Parameters.Add(Valor_inicial);
                contador += 1;

                SqlParameter Clientes_iniciales = new()
                {
                    ParameterName = "@Clientes_iniciales",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Clientes_iniciales,
                };
                SqlCmd.Parameters.Add(Clientes_iniciales);
                contador += 1;

                SqlParameter Clientes_nuevos = new()
                {
                    ParameterName = "@Clientes_nuevos",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Clientes_nuevos,
                };
                SqlCmd.Parameters.Add(Clientes_nuevos);
                contador += 1;

                SqlParameter Clientes_cancelados = new()
                {
                    ParameterName = "@Clientes_cancelados",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Clientes_cancelados,
                };
                SqlCmd.Parameters.Add(Clientes_cancelados);
                contador += 1;

                SqlParameter Clientes_total = new()
                {
                    ParameterName = "@Clientes_total",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Clientes_total,
                };
                SqlCmd.Parameters.Add(Clientes_total);
                contador += 1;

                SqlParameter Recaudo_ventas_nuevas = new()
                {
                    ParameterName = "@Recaudo_ventas_nuevas",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Recaudo_ventas_nuevas,
                };
                SqlCmd.Parameters.Add(Recaudo_ventas_nuevas);
                contador += 1;

                SqlParameter Recaudo_cuotas = new()
                {
                    ParameterName = "@Recaudo_cuotas",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Recaudo_cuotas,
                };
                SqlCmd.Parameters.Add(Recaudo_cuotas);
                contador += 1;

                SqlParameter Recaudo_otros = new()
                {
                    ParameterName = "@Recaudo_otros",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Recaudo_otros,
                };
                SqlCmd.Parameters.Add(Recaudo_otros);
                contador += 1;

                SqlParameter Recaudo_pretendido_turno = new()
                {
                    ParameterName = "@Recaudo_pretendido_turno",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Recaudo_pretendido_turno,
                };
                SqlCmd.Parameters.Add(Recaudo_pretendido_turno);
                contador += 1;

                SqlParameter Recaudo_real = new()
                {
                    ParameterName = "@Recaudo_real",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Recaudo_real,
                };
                SqlCmd.Parameters.Add(Recaudo_real);
                contador += 1;

                SqlParameter Gastos_total = new()
                {
                    ParameterName = "@Gastos_total",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Gastos_total,
                };
                SqlCmd.Parameters.Add(Gastos_total);
                contador += 1;

                SqlParameter Estado_turno = new()
                {
                    ParameterName = "@Estado_turno",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = turno.Estado_turno.Trim()
                };
                SqlCmd.Parameters.Add(Estado_turno);
                contador += 1;
                #endregion

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
                    turno.Id_turno = Convert.ToInt32(SqlCmd.Parameters["@Id_turno"].Value);
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
        public Task<string> EditarVenta(Turnos turno)
        {
            int contador = 0;
            string rpta = string.Empty;

            string consulta = "UPDATE Turnos SET " +
                "Id_cobrador = @Id_cobrador, " +
                "Id_cobro = @Id_cobro, " +
                "Fecha_inicio_turno = @Fecha_inicio_turno, " +
                "Fecha_fin_turno = @Fecha_fin_turno, " +
                "Hora_inicio_turno  = @Hora_inicio_turno, " +
                "Hora_fin_turno = @Hora_fin_turno, " +
                "Valor_inicial = @Valor_inicial, " +
                "Clientes_iniciales = @Clientes_iniciales, " +
                "Clientes_nuevos = @Clientes_nuevos, " +
                "Clientes_cancelados = @Clientes_cancelados, " +
                "Clientes_total = @Clientes_total, " +
                "Recaudo_ventas_nuevas = @Recaudo_ventas_nuevas, " +
                "Recaudo_cuotas = @Recaudo_cuotas, " +
                "Recaudo_otros = @Recaudo_otros, " +
                "Recaudo_pretendido_turno = @Recaudo_pretendido_turno, " +
                "Recaudo_real = @Recaudo_real, " +
                "Gastos_total = @Gastos_total, " +
                "Estado_turno = @Estado_turno " +
                "WHERE Id_turno = @Id_turno ";

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
                    CommandType = CommandType.Text
                };

                #region PARÁMETROS
                SqlParameter Id_turno = new()
                {
                    ParameterName = "@Id_turno",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Id_turno,
                };
                SqlCmd.Parameters.Add(Id_turno);

                SqlParameter Id_cobrador = new()
                {
                    ParameterName = "@Id_cobrador",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Id_cobrador,
                };
                SqlCmd.Parameters.Add(Id_cobrador);
                contador += 1;

                SqlParameter Id_cobro = new()
                {
                    ParameterName = "@Id_cobro",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Id_cobro,
                };
                SqlCmd.Parameters.Add(Id_cobro);
                contador += 1;

                SqlParameter Fecha_inicio_turno = new()
                {
                    ParameterName = "@Fecha_inicio_turno",
                    SqlDbType = SqlDbType.Date,
                    Value = turno.Fecha_inicio_turno,
                };
                SqlCmd.Parameters.Add(Fecha_inicio_turno);
                contador += 1;

                SqlParameter Fecha_fin_turno = new()
                {
                    ParameterName = "@Fecha_fin_turno",
                    SqlDbType = SqlDbType.Date,
                    Value = turno.Fecha_fin_turno,
                };
                SqlCmd.Parameters.Add(Fecha_fin_turno);
                contador += 1;

                SqlParameter Hora_inicio_turno = new()
                {
                    ParameterName = "@Hora_inicio_turno",
                    SqlDbType = SqlDbType.Time,
                    Value = turno.Hora_inicio_turno,
                };
                SqlCmd.Parameters.Add(Hora_inicio_turno);
                contador += 1;

                SqlParameter Hora_fin_turno = new()
                {
                    ParameterName = "@Hora_fin_turno",
                    SqlDbType = SqlDbType.Time,
                    Value = turno.Hora_fin_turno,
                };
                SqlCmd.Parameters.Add(Hora_fin_turno);
                contador += 1;

                SqlParameter Valor_inicial = new()
                {
                    ParameterName = "@Valor_inicial",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Valor_inicial,
                };
                SqlCmd.Parameters.Add(Valor_inicial);
                contador += 1;

                SqlParameter Clientes_iniciales = new()
                {
                    ParameterName = "@Clientes_iniciales",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Clientes_iniciales,
                };
                SqlCmd.Parameters.Add(Clientes_iniciales);
                contador += 1;

                SqlParameter Clientes_nuevos = new()
                {
                    ParameterName = "@Clientes_nuevos",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Clientes_nuevos,
                };
                SqlCmd.Parameters.Add(Clientes_nuevos);
                contador += 1;

                SqlParameter Clientes_cancelados = new()
                {
                    ParameterName = "@Clientes_cancelados",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Clientes_cancelados,
                };
                SqlCmd.Parameters.Add(Clientes_cancelados);
                contador += 1;

                SqlParameter Clientes_total = new()
                {
                    ParameterName = "@Clientes_total",
                    SqlDbType = SqlDbType.Int,
                    Value = turno.Clientes_total,
                };
                SqlCmd.Parameters.Add(Clientes_total);
                contador += 1;

                SqlParameter Recaudo_ventas_nuevas = new()
                {
                    ParameterName = "@Recaudo_ventas_nuevas",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Recaudo_ventas_nuevas,
                };
                SqlCmd.Parameters.Add(Recaudo_ventas_nuevas);
                contador += 1;

                SqlParameter Recaudo_cuotas = new()
                {
                    ParameterName = "@Recaudo_cuotas",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Recaudo_cuotas,
                };
                SqlCmd.Parameters.Add(Recaudo_cuotas);
                contador += 1;

                SqlParameter Recaudo_otros = new()
                {
                    ParameterName = "@Recaudo_otros",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Recaudo_otros,
                };
                SqlCmd.Parameters.Add(Recaudo_otros);
                contador += 1;

                SqlParameter Recaudo_pretendido_turno = new()
                {
                    ParameterName = "@Recaudo_pretendido_turno",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Recaudo_pretendido_turno,
                };
                SqlCmd.Parameters.Add(Recaudo_pretendido_turno);
                contador += 1;

                SqlParameter Recaudo_real = new()
                {
                    ParameterName = "@Recaudo_real",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Recaudo_real,
                };
                SqlCmd.Parameters.Add(Recaudo_real);
                contador += 1;

                SqlParameter Gastos_total = new()
                {
                    ParameterName = "@Gastos_total",
                    SqlDbType = SqlDbType.Decimal,
                    Value = turno.Gastos_total,
                };
                SqlCmd.Parameters.Add(Gastos_total);
                contador += 1;

                SqlParameter Estado_turno = new()
                {
                    ParameterName = "@Estado_turno",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = turno.Estado_turno.Trim()
                };
                SqlCmd.Parameters.Add(Estado_turno);
                contador += 1;

                #endregion

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

        #region METODO BUSCAR TURNOS
        public Task<(DataTable dt, string rpta)> BuscarTurnos(string tipo_busqueda, string[] textos_busqueda)
        {
            string rpta = "OK";

            DataTable DtResultado = new("Turnos");
            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            try
            {
                StringBuilder consulta = new();
                SqlCommand Sqlcmd;
                if (tipo_busqueda.Equals("CERRAR TURNO"))
                {
                    SqlCon.ConnectionString = this.IConexionDac.Cn();
                    SqlCon.Open();
                    Sqlcmd = new()
                    {
                        Connection = SqlCon,
                        CommandText = "sp_Estadistica_cobradores_diarias",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter Fecha = new()
                    {
                        ParameterName = "@Fecha",
                        SqlDbType = SqlDbType.VarChar,
                        Size = 50,
                        Value = textos_busqueda[0],
                    };
                    Sqlcmd.Parameters.Add(Fecha);
                }
                else if (tipo_busqueda.Equals("ABRIR TURNO"))
                {
                    SqlCon.ConnectionString = this.IConexionDac.Cn();
                    SqlCon.Open();
                    Sqlcmd = new()
                    {
                        Connection = SqlCon,
                        CommandText = "sp_Abrir_turno_cobrador",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter Fecha = new()
                    {
                        ParameterName = "@Fecha",
                        SqlDbType = SqlDbType.VarChar,
                        Size = 50,
                        Value = textos_busqueda[0],
                    };
                    Sqlcmd.Parameters.Add(Fecha);
                }
                else if (tipo_busqueda.Equals("CERRAR TURNO ANTERIOR"))
                {
                    SqlCon.ConnectionString = this.IConexionDac.Cn();
                    SqlCon.Open();
                    Sqlcmd = new()
                    {
                        Connection = SqlCon,
                        CommandText = "sp_Cerrar_turnov2",
                        CommandType = CommandType.StoredProcedure
                    };

                    SqlParameter Id_turno = new()
                    {
                        ParameterName = "@Id_turno",
                        SqlDbType = SqlDbType.Int,
                        Value = Convert.ToInt32(textos_busqueda[0]),
                    };
                    Sqlcmd.Parameters.Add(Id_turno);

                    SqlParameter Id_cobro = new()
                    {
                        ParameterName = "@Id_cobro",
                        SqlDbType = SqlDbType.Int,
                        Value = Convert.ToInt32(textos_busqueda[1]),
                    };
                    Sqlcmd.Parameters.Add(Id_cobro);
                }
                else
                {
                    consulta.Append("SELECT * " +
                        "FROM Turnos tu " +
                        "INNER JOIN Usuarios us ON tu.Id_cobrador = us.Id_usuario " +
                        "INNER JOIN Cobros co ON tu.Id_cobro = co.Id_cobro " +
                        "INNER JOIN Tipo_productos tp ON co.Id_tipo_producto = tp.Id_tipo_producto " +
                        "INNER JOIN Zonas zo ON co.Id_zona = zo.Id_zona " +
                        "INNER JOIN Ciudades cd ON zo.Id_ciudad = cd.Id_ciudad ");

                    if (tipo_busqueda.Equals("ID TURNO"))
                    {
                        consulta.Append($"WHERE tu.Id_turno = {textos_busqueda[0].Trim()} ");
                    }
                    else if (tipo_busqueda.Equals("ID COBRADOR"))
                    {
                        consulta.Append($"WHERE tu.Id_cobrador = {textos_busqueda[0].Trim()} ");
                    }
                    else if (tipo_busqueda.Equals("FECHA INICIO Y COBRO"))
                    {
                        consulta.Append($"WHERE tu.Fecha_inicio_turno = '{textos_busqueda[0].Trim()}' AND " +
                            $"tu.Id_cobro = {textos_busqueda[1].Trim()} ");
                    }

                    consulta.Append("ORDER BY tu.Id_turno DESC");

                    SqlCon.ConnectionString = this.IConexionDac.Cn();
                    SqlCon.Open();
                    Sqlcmd = new()
                    {
                        Connection = SqlCon,
                        CommandText = consulta.ToString(),
                        CommandType = CommandType.Text
                    };
                }

                SqlDataAdapter SqlData = new(Sqlcmd);
                SqlData.Fill(DtResultado);

                if (DtResultado.Rows.Count < 1)
                {
                    DtResultado = null;
                }
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
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Task.FromResult((DtResultado, rpta));
        }
        public Task<(DataTable dt, string rpta)> BuscarTurnos(string tipo_busqueda, string texto_busqueda)
        {
            string rpta = "OK";

            DataTable DtResultado = new("Turnos");
            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            try
            {
                StringBuilder consulta = new();
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();
                SqlCommand Sqlcmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Turnos_g",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Tipo_busqueda = new()
                {
                    ParameterName = "@Tipo_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Value = tipo_busqueda
                };
                Sqlcmd.Parameters.Add(Tipo_busqueda);

                SqlParameter Texto_busqueda = new()
                {
                    ParameterName = "@Texto_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Value = texto_busqueda
                };
                Sqlcmd.Parameters.Add(Texto_busqueda);

                SqlDataAdapter SqlData = new(Sqlcmd);
                SqlData.Fill(DtResultado);

                if (DtResultado.Rows.Count < 1)
                {
                    DtResultado = null;
                }
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
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Task.FromResult((DtResultado, rpta));
        }
        #endregion

        #region METODO SINCRONIZAR CLIENTES
        public Task<(DataTable dt, string rpta)> SincronizarClientes(int id_cobro, int id_usuario, DateTime fecha)
        {
            string rpta = "OK";

            DataTable DtResultado = new("Agendamientos");
            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            try
            {
                StringBuilder consulta = new();
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();
                SqlCommand Sqlcmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Sincronizar_clientes",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_cobro = new()
                {
                    ParameterName = "@Id_cobro",
                    SqlDbType = SqlDbType.Int,
                    Value = id_cobro
                };
                Sqlcmd.Parameters.Add(Id_cobro);

                SqlParameter Id_usuario = new()
                {
                    ParameterName = "@Id_usuario",
                    SqlDbType = SqlDbType.Int,
                    Value = id_usuario
                };
                Sqlcmd.Parameters.Add(Id_usuario);

                SqlParameter Fecha = new()
                {
                    ParameterName = "@Fecha",
                    SqlDbType = SqlDbType.Date,
                    Value = fecha
                };
                Sqlcmd.Parameters.Add(Fecha);

                SqlDataAdapter SqlData = new(Sqlcmd);
                SqlData.Fill(DtResultado);

                if (DtResultado.Rows.Count < 1)
                {
                    DtResultado = null;
                }
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
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Task.FromResult((DtResultado, rpta));
        }
        #endregion

        #region METODO ESTADÍSTICAS DIARIAS
        public Task<(DataTable dt, string rpta)> EstadisticasDiarias(int id_turno, DateTime fecha)
        {
            string rpta = "OK";

            DataTable DtResultado = new("EstadisticasDiariias");
            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            try
            {
                StringBuilder consulta = new();
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();
                SqlCommand Sqlcmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Estadistica_cobradores_diarias",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Texto_busqueda = new()
                {
                    ParameterName = "@Texto_busqueda",
                    SqlDbType = SqlDbType.VarChar,
                    Value = id_turno.ToString(),
                };
                Sqlcmd.Parameters.Add(Texto_busqueda);

                SqlParameter Fecha = new()
                {
                    ParameterName = "@Fecha",
                    SqlDbType = SqlDbType.Date,
                    Value = fecha
                };
                Sqlcmd.Parameters.Add(Fecha);

                SqlDataAdapter SqlData = new(Sqlcmd);
                SqlData.Fill(DtResultado);

                if (DtResultado.Rows.Count < 1)
                {
                    DtResultado = null;
                }
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
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return Task.FromResult((DtResultado, rpta));
        }
        #endregion
    }
}
