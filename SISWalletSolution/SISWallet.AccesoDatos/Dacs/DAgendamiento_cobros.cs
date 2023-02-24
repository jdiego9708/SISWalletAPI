namespace SISWallet.AccesoDatos
{
    using SISWallet.Entidades.Models;
    using SISWallet.AccesoDatos.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Threading.Tasks;

    public class DAgendamiento_cobros : IAgendamiento_cobrosDac
    {
        #region MENSAJE
        private void SqlCon_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            this.Mensaje_respuesta = e.Message;
        }
        #endregion

        #region PROPIEDADES
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
        public DAgendamiento_cobros(IConexionDac iConexionDac)
        {
            IConexionDac = iConexionDac;
        }
        #endregion

        #region METODO INSERTAR
        public Task<string> InsertarAgendamiento(Agendamiento_cobros agendamiento)
        {
            string rpta = string.Empty;

            string consulta = "INSERT INTO Agendamiento_cobros (Id_venta, Id_turno, Orden_cobro, Fecha_cobro, Hora_cobro, " +
                "Valor_cobro, Valor_pagado, Saldo_restante, Tipo_cobro, Observaciones_cobro, Estado_cobro) " +
                "VALUES(@Id_venta, @Id_turno, @Orden_cobro, @Fecha_cobro, @Hora_cobro, " +
                "@Valor_cobro, @Valor_pagado, @Saldo_restante, @Tipo_cobro, @Observaciones_cobro, @Estado_cobro) " +
                "SET @Id_agendamiento = SCOPE_IDENTITY() ";

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

                SqlParameter Id_agendamiento = new()
                {
                    ParameterName = "@Id_agendamiento",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_agendamiento);

                SqlParameter Id_venta = new()
                {
                    ParameterName = "@Id_venta",
                    SqlDbType = SqlDbType.Int,
                    Value = agendamiento.Id_venta
                };
                SqlCmd.Parameters.Add(Id_venta);

                SqlParameter Id_turno = new()
                {
                    ParameterName = "@Id_turno",
                    SqlDbType = SqlDbType.Int,
                    Value = agendamiento.Id_turno
                };
                SqlCmd.Parameters.Add(Id_turno);

                SqlParameter Orden_cobro = new()
                {
                    ParameterName = "@Orden_cobro",
                    SqlDbType = SqlDbType.Int,
                    Value = agendamiento.Orden_cobro
                };
                SqlCmd.Parameters.Add(Orden_cobro);

                SqlParameter Fecha_cobro = new()
                {
                    ParameterName = "@Fecha_cobro",
                    SqlDbType = SqlDbType.Date,
                    Value = agendamiento.Fecha_cobro
                };
                SqlCmd.Parameters.Add(Fecha_cobro);

                SqlParameter Hora_cobro = new()
                {
                    ParameterName = "@Hora_cobro",
                    SqlDbType = SqlDbType.Time,
                    Value = agendamiento.Hora_cobro
                };
                SqlCmd.Parameters.Add(Hora_cobro);

                SqlParameter Valor_cobro = new()
                {
                    ParameterName = "@Valor_cobro",
                    SqlDbType = SqlDbType.Decimal,
                    Value = agendamiento.Valor_cobro
                };
                SqlCmd.Parameters.Add(Valor_cobro);

                SqlParameter Valor_pagado = new()
                {
                    ParameterName = "@Valor_pagado",
                    SqlDbType = SqlDbType.Decimal,
                    Value = agendamiento.Valor_pagado
                };
                SqlCmd.Parameters.Add(Valor_pagado);

                SqlParameter Saldo_restante = new()
                {
                    ParameterName = "@Saldo_restante",
                    SqlDbType = SqlDbType.Decimal,
                    Value = agendamiento.Saldo_restante
                };
                SqlCmd.Parameters.Add(Saldo_restante);

                SqlParameter Tipo_cobro = new()
                {
                    ParameterName = "@Tipo_cobro",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = agendamiento.Tipo_cobro.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Tipo_cobro);

                SqlParameter Observaciones_cobro = new()
                {
                    ParameterName = "@Observaciones_cobro",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 150,
                    Value = agendamiento.Observaciones_cobro == null ? string.Empty : agendamiento.Observaciones_cobro.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Observaciones_cobro);

                SqlParameter Estado_cobro = new()
                {
                    ParameterName = "@Estado_cobro",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = agendamiento.Estado_cobro.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Estado_cobro);

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
                    agendamiento.Id_agendamiento = Convert.ToInt32(SqlCmd.Parameters["@Id_agendamiento"].Value);
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
        public Task<string> EditarAgendamiento(Agendamiento_cobros agendamiento)
        {
            int contador = 0;
            string rpta = string.Empty;

            string consulta = "UPDATE Agendamiento_cobros SET " +
                "Id_venta = @Id_venta, " +
                "Id_turno = @Id_turno, " +
                "Orden_cobro = @Orden_cobro, " +
                "Fecha_cobro = @Fecha_cobro, " +
                "Hora_cobro = @Hora_cobro, " +
                "Valor_cobro = @Valor_cobro, " +
                "Valor_pagado = @Valor_pagado, " +
                "Saldo_restante = @Saldo_restante, " +
                "Tipo_cobro = @Tipo_cobro, " +
                "Observaciones_cobro = Observaciones_cobro, " +
                "Estado_cobro = @Estado_cobro " +
                "WHERE Id_agendamiento = @Id_agendamiento ";

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

                SqlParameter Id_agendamiento = new()
                {
                    ParameterName = "@Id_agendamiento",
                    SqlDbType = SqlDbType.Int,
                    Value = agendamiento.Id_agendamiento,
                };
                SqlCmd.Parameters.Add(Id_agendamiento);

                SqlParameter Id_venta = new()
                {
                    ParameterName = "@Id_venta",
                    SqlDbType = SqlDbType.Int,
                    Value = agendamiento.Id_venta
                };
                SqlCmd.Parameters.Add(Id_venta);
                contador += 1;

                SqlParameter Id_turno = new()
                {
                    ParameterName = "@Id_turno",
                    SqlDbType = SqlDbType.Int,
                    Value = agendamiento.Id_turno
                };
                SqlCmd.Parameters.Add(Id_turno);
                contador += 1;

                SqlParameter Orden_cobro = new()
                {
                    ParameterName = "@Orden_cobro",
                    SqlDbType = SqlDbType.Int,
                    Value = agendamiento.Orden_cobro
                };
                SqlCmd.Parameters.Add(Orden_cobro);
                contador += 1;

                SqlParameter Fecha_cobro = new()
                {
                    ParameterName = "@Fecha_cobro",
                    SqlDbType = SqlDbType.Date,
                    Value = agendamiento.Fecha_cobro
                };
                SqlCmd.Parameters.Add(Fecha_cobro);
                contador += 1;

                SqlParameter Hora_cobro = new()
                {
                    ParameterName = "@Hora_cobro",
                    SqlDbType = SqlDbType.Time,
                    Value = agendamiento.Hora_cobro
                };
                SqlCmd.Parameters.Add(Hora_cobro);
                contador += 1;

                SqlParameter Valor_cobro = new()
                {
                    ParameterName = "@Valor_cobro",
                    SqlDbType = SqlDbType.Decimal,
                    Value = agendamiento.Valor_cobro
                };
                SqlCmd.Parameters.Add(Valor_cobro);
                contador += 1;

                SqlParameter Valor_pagado = new()
                {
                    ParameterName = "@Valor_pagado",
                    SqlDbType = SqlDbType.Decimal,
                    Value = agendamiento.Valor_pagado
                };
                SqlCmd.Parameters.Add(Valor_pagado);
                contador += 1;

                SqlParameter Saldo_restante = new()
                {
                    ParameterName = "@Saldo_restante",
                    SqlDbType = SqlDbType.Decimal,
                    Value = agendamiento.Saldo_restante
                };
                SqlCmd.Parameters.Add(Saldo_restante);
                contador += 1;

                SqlParameter Tipo_cobro = new()
                {
                    ParameterName = "@Tipo_cobro",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = agendamiento.Tipo_cobro.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Tipo_cobro);
                contador += 1;

                SqlParameter Observaciones_cobro = new()
                {
                    ParameterName = "@Observaciones_cobro",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 150,
                    Value = agendamiento.Observaciones_cobro.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Observaciones_cobro);
                contador += 1;

                SqlParameter Estado_cobro = new()
                {
                    ParameterName = "@Estado_cobro",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = agendamiento.Estado_cobro.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Estado_cobro);
                contador += 1;

                
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
        public Task<string> EditarOrden(List<int[]> items)
        {
            string rpta = string.Empty;
            StringBuilder consulta = new();

            for (int i = 0; i <= items.Count - 1; i++)
            {
                int[] vs = items[i];
                consulta.Append("UPDATE Agendamiento_cobros SET " + 
                    "Orden_cobro = " + (vs[1] + 1) + " " + 
                    "WHERE Id_agendamiento = " + vs[0] + "; ");
            }

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
                    CommandText = consulta.ToString(),
                    CommandType = CommandType.Text
                };

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

        #region MÉTODO CAMBIAR ESTADO
        public Task<string> CambiarEstadoAgendamiento(int id_agendamiento, string estado)
        {
            int contador = 0;
            string rpta = string.Empty;

            string consulta = "UPDATE Agendamiento_cobros SET " +
                "Estado_cobro = @Estado_cobro " +
                "WHERE Id_agendamiento = @Id_agendamiento ";

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

                SqlParameter Id_agendamiento = new()
                {
                    ParameterName = "@Id_agendamiento",
                    SqlDbType = SqlDbType.Int,
                    Value = id_agendamiento,
                };
                SqlCmd.Parameters.Add(Id_agendamiento);

                SqlParameter Estado_cobro = new()
                {
                    ParameterName = "@Estado_cobro",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = estado.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Estado_cobro);
                contador += 1;

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

        #region MÉTODO TERMINAR AGENDAMIENTO
        public Task<string> TerminarAgendamiento(int id_agendamiento, string estado, decimal valor_pagado, decimal saldo_restante)
        {
            int contador = 0;
            string rpta = string.Empty;

            string consulta = "UPDATE Agendamiento_cobros SET " +
                "Estado_cobro = @Estado_cobro, " +
                "Valor_pagado = @Valor_pagado, " +
                "Saldo_restante = @Saldo_restante " +
                "WHERE Id_agendamiento = @Id_agendamiento ";

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

                SqlParameter Id_agendamiento = new()
                {
                    ParameterName = "@Id_agendamiento",
                    SqlDbType = SqlDbType.Int,
                    Value = id_agendamiento,
                };
                SqlCmd.Parameters.Add(Id_agendamiento);

                SqlParameter Estado_cobro = new()
                {
                    ParameterName = "@Estado_cobro",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = estado.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Estado_cobro);
                contador += 1;

                SqlParameter Valor_pagado = new()
                {
                    ParameterName = "@Valor_pagado",
                    SqlDbType = SqlDbType.Decimal,
                    Value = valor_pagado
                };
                SqlCmd.Parameters.Add(Valor_pagado);
                contador += 1;

                SqlParameter Saldo_restante = new()
                {
                    ParameterName = "@Saldo_restante",
                    SqlDbType = SqlDbType.Decimal,
                    Value = saldo_restante
                };
                SqlCmd.Parameters.Add(Saldo_restante);
                contador += 1;

                
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

        #region MÉTODO ELIMINAR AGENDAMIENTO
        public Task<string> EliminarAgendamiento(int id_agendamiento)
        {
            string rpta = string.Empty;

            string consulta =
                "DELETE FROM Agendamiento_cobros " +
                "WHERE Id_agendamiento = @Id_agendamiento ";

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

                SqlParameter Id_agendamiento = new()
                {
                    ParameterName = "@Id_agendamiento",
                    SqlDbType = SqlDbType.Int,
                    Value = id_agendamiento,
                };
                SqlCmd.Parameters.Add(Id_agendamiento);

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

        #region EDITAR SALDO AGENDAMIENTO
        public Task<string> EditarSaldoAgendamiento(int id_agendamiento, decimal saldo)
        {
            string rpta = string.Empty;

            string consulta =
                "UPDATE Agendamiento_cobros SET " +
                $"Saldo_restante = @Saldo_restante " +
                $"WHERE Id_agendamiento = @Id_agendamiento ";

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

                SqlParameter Id_agendamiento = new()
                {
                    ParameterName = "@Id_agendamiento",
                    SqlDbType = SqlDbType.Int,
                    Value = id_agendamiento,
                };
                SqlCmd.Parameters.Add(Id_agendamiento);


                SqlParameter Saldo_restante = new()
                {
                    ParameterName = "@Saldo_restante",
                    SqlDbType = SqlDbType.Decimal,
                    Value = saldo,
                };
                SqlCmd.Parameters.Add(Saldo_restante);

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

        #region METODO BUSCAR AGENDAMIENTOS
        public Task<(DataTable dtAgendamientos, string rpta)> BuscarAgendamiento(string tipo_busqueda, string[] textos_busqueda)
        {
            string rpta = "OK";
     
            DataTable dtAgendamientos = new("Agendamientos");
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
                    CommandText = "sp_Buscar_agendamientos",
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

                SqlParameter Texto_busqueda1 = new()
                {
                    ParameterName = "@Texto_busqueda1",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = textos_busqueda[0].Trim()
                };
                Sqlcmd.Parameters.Add(Texto_busqueda1);

                SqlParameter Id_cobro = new()
                {
                    ParameterName = "@Id_cobro",
                    SqlDbType = SqlDbType.Int,
                    Value = textos_busqueda[1]
                };
                Sqlcmd.Parameters.Add(Id_cobro);

                SqlDataAdapter SqlData = new(Sqlcmd);
                SqlData.Fill(dtAgendamientos);

                if (dtAgendamientos.Rows.Count < 1)
                {
                    dtAgendamientos = null;
                }
            }
            catch (SqlException ex)
            {
                rpta = ex.Message;
                dtAgendamientos = null;
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
                dtAgendamientos = null;
            }
            return Task.FromResult((dtAgendamientos, rpta));
        }
        #endregion

        #region METODO ACTUALIZAR ORDEN
        public Task<string> ActualizarOrden(int id_agendamiento, int orden)
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
                    CommandText = "sp_Actualizar_orden",
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter Id_agendamiento = new()
                {
                    ParameterName = "@Id_agendamiento",
                    SqlDbType = SqlDbType.Int,
                    Value = id_agendamiento,
                };
                SqlCmd.Parameters.Add(Id_agendamiento);

                SqlParameter Orden = new()
                {
                    ParameterName = "@Orden",
                    SqlDbType = SqlDbType.Int,
                    Value = orden
                };
                SqlCmd.Parameters.Add(Orden);
                
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
    }
}
