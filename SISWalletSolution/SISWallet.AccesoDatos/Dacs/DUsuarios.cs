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
    using System.Linq;
    using SISWallet.Entidades.ModelosBindeo;
    using SISWallet.Entidades.Helpers;

    public class DUsuarios : IUsuariosDac
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
        public DUsuarios(IConexionDac iConexionDac)
        {
            IConexionDac = iConexionDac;
        }
        #endregion

        #region METODO INSERTAR
        public Task<string> InsertarUsuario(Usuarios usuario)
        {
            int id_usuario = 0;
            int contador = 0;
            string rpta = string.Empty;

            string consulta = "INSERT INTO Usuarios (Fecha_ingreso, Alias, Nombres, Apellidos, Identificacion, " +
                "Celular, Email, Tipo_usuario, Estado_usuario) " +
                "VALUES(@Fecha_ingreso, @Alias, @Nombres, @Apellidos, @Identificacion, " +
                "@Celular, @Email, @Tipo_usuario, @Estado_usuario) " +
                "SET @Id_usuario = SCOPE_IDENTITY() ";

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
                SqlParameter Id_usuario = new()
                {
                    ParameterName = "@Id_usuario",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_usuario);

                SqlParameter Fecha_ingreso = new()
                {
                    ParameterName = "@Fecha_ingreso",
                    SqlDbType = SqlDbType.Date,
                    Value = usuario.Fecha_ingreso,
                };
                SqlCmd.Parameters.Add(Fecha_ingreso);
                contador += 1;

                SqlParameter Alias = new()
                {
                    ParameterName = "@Alias",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Alias.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Alias);
                contador += 1;

                SqlParameter Nombres = new()
                {
                    ParameterName = "@Nombres",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Nombres.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Nombres);
                contador += 1;

                SqlParameter Apellidos = new()
                {
                    ParameterName = "@Apellidos",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Apellidos.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Apellidos);
                contador += 1;

                SqlParameter Identificacion = new()
                {
                    ParameterName = "@Identificacion",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Identificacion.Trim()
                };
                SqlCmd.Parameters.Add(Identificacion);
                contador += 1;

                SqlParameter Celular = new()
                {
                    ParameterName = "@Celular",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Celular.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Celular);
                contador += 1;

                SqlParameter Email = new()
                {
                    ParameterName = "@Email",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Email.Trim()
                };
                SqlCmd.Parameters.Add(Email);
                contador += 1;

                SqlParameter Tipo_usuario = new()
                {
                    ParameterName = "@Tipo_usuario",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Tipo_usuario.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Tipo_usuario);
                contador += 1;

                SqlParameter Estado_usuario = new()
                {
                    ParameterName = "@Estado_usuario",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Estado_usuario.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Estado_usuario);
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
                    id_usuario = Convert.ToInt32(SqlCmd.Parameters["@Id_usuario"].Value);
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
        public Task<string> EditarUsuario(Usuarios usuario)
        {
            int contador = 0;
            string rpta = string.Empty;

            string consulta = "UPDATE Usuarios SET " +
                "Fecha_ingreso = @Fecha_ingreso, " +
                "Alias = @Alias, " +
                "Nombres = @Nombres, " +
                "Apellidos = @Apellidos, " +
                "Identificacion = @Identificacion, " +
                "Celular = @Celular, " +
                "Email = @Email, " +
                "Tipo_usuario = @Tipo_usuario, " +
                "Estado_usuario = @Estado_usuario " +
                "WHERE Id_usuario = @Id_usuario ";

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

                SqlParameter Id_usuario = new()
                {
                    ParameterName = "@Id_usuario",
                    SqlDbType = SqlDbType.Int,
                    Value = usuario.Id_usuario,
                };
                SqlCmd.Parameters.Add(Id_usuario);

                SqlParameter Fecha_ingreso = new()
                {
                    ParameterName = "@Fecha_ingreso",
                    SqlDbType = SqlDbType.Date,
                    Value = usuario.Fecha_ingreso,
                };
                SqlCmd.Parameters.Add(Fecha_ingreso);
                contador += 1;

                SqlParameter Alias = new()
                {
                    ParameterName = "@Alias",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Alias.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Alias);
                contador += 1;

                SqlParameter Nombres = new()
                {
                    ParameterName = "@Nombres",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Nombres.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Nombres);
                contador += 1;

                SqlParameter Apellidos = new()
                {
                    ParameterName = "@Apellidos",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Apellidos.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Apellidos);
                contador += 1;

                SqlParameter Identificacion = new()
                {
                    ParameterName = "@Identificacion",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Identificacion.Trim()
                };
                SqlCmd.Parameters.Add(Identificacion);
                contador += 1;

                SqlParameter Celular = new()
                {
                    ParameterName = "@Celular",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Celular.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Celular);
                contador += 1;

                SqlParameter Email = new()
                {
                    ParameterName = "@Email",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Email.Trim()
                };
                SqlCmd.Parameters.Add(Email);
                contador += 1;

                SqlParameter Tipo_usuario = new()
                {
                    ParameterName = "@Tipo_usuario",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Tipo_usuario.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Tipo_usuario);
                contador += 1;

                SqlParameter Estado_usuario = new()
                {
                    ParameterName = "@Estado_usuario",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Tipo_usuario.Trim().ToUpper()
                };
                SqlCmd.Parameters.Add(Estado_usuario);
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

        #region METODO BUSCAR USUARIOS
        public Task<(DataTable dtUsuarios, string rpta)> BuscarUsuarios(string tipo_busqueda, string texto_busqueda)
        {
            string rpta = "OK";

            StringBuilder consulta = new();

            consulta.Append("SELECT * FROM Usuarios us ");

            if (tipo_busqueda.Equals("ID USUARIO"))
            {
                consulta.Append("WHERE us.Id_usuario = @Texto_busqueda ");
            }
            else if (tipo_busqueda.Equals("TIPO USUARIO"))
            {
                consulta.Append("WHERE us.Tipo_usuario = @Texto_busqueda ");
            }

            consulta.Append("ORDER BY us.Id_usuario DESC");

            DataTable DtResultado = new("Usuarios");
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
                    CommandText = consulta.ToString(),
                    CommandType = CommandType.Text
                };

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
        public Task<(DataTable dtClientes, string rpta)> BuscarClientes(string tipo_busqueda, string texto_busqueda1,
            string texto_busqueda2)
        {
            string rpta = "OK";
            DataTable DtResultado = new("Clientes");
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
                    CommandText = "sp_Buscar_clientes",
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
                    Value = texto_busqueda1.Trim()
                };
                Sqlcmd.Parameters.Add(Texto_busqueda1);

                SqlParameter Texto_busqueda2 = new()
                {
                    ParameterName = "@Texto_busqueda2",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = texto_busqueda2.Trim()
                };
                Sqlcmd.Parameters.Add(Texto_busqueda2);

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
            return Task.FromResult((DtResultado, rpta));
        }
        #endregion

        public Task<string> InsertarUsuarioVenta(Usuarios_ventas usuarios)
        {
            string rpta = "";

            string consulta = "INSERT INTO Usuarios_ventas (Id_venta, Id_usuario) " +
                "VALUES(@Id_venta, @Id_usuario) ";

            SqlConnection SqlCon = new SqlConnection();
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

                SqlParameter Id_venta = new()
                {
                    ParameterName = "@Id_venta",
                    SqlDbType = SqlDbType.Int,
                    Value = usuarios.Id_venta
                };
                SqlCmd.Parameters.Add(Id_venta);

                SqlParameter Id_usuario = new()
                {
                    ParameterName = "@Id_usuario",
                    SqlDbType = SqlDbType.Int,
                    Value = usuarios.Id_usuario
                };
                SqlCmd.Parameters.Add(Id_usuario);

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

        public Task<(string rpta, LoginDataModel loginData)> Login(string usuario, string pass, string fecha)
        {
            string rpta = "OK";
            LoginDataModel loginData = new();
            DataSet ds = new("Login");
            SqlConnection SqlCon = new();
            SqlCon.InfoMessage += new SqlInfoMessageEventHandler(SqlCon_InfoMessage);
            SqlCon.FireInfoMessageEventOnUserErrors = true;
            try
            {
                StringBuilder consulta = new();
                SqlCommand Sqlcmd;
                SqlCon.ConnectionString = this.IConexionDac.Cn();
                SqlCon.Open();
                Sqlcmd = new()
                {
                    Connection = SqlCon,
                    CommandText = "sp_Loginv2",
                    CommandType = CommandType.StoredProcedure
                };

                #region PARÁMETROS

                SqlParameter Usuario = new()
                {
                    ParameterName = "@Usuario",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = usuario.Trim()
                };
                Sqlcmd.Parameters.Add(Usuario);

                SqlParameter Pass = new()
                {
                    ParameterName = "@Pass",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = pass.Trim()
                };
                Sqlcmd.Parameters.Add(Pass);

                SqlParameter Fecha = new()
                {
                    ParameterName = "@Fecha",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = fecha.Trim()
                };
                Sqlcmd.Parameters.Add(Fecha);
                #endregion

                SqlDataAdapter SqlData = new(Sqlcmd);
                SqlData.Fill(ds);

                bool result = false;
                string tipo_usuario = string.Empty;
                //1->Primer tabla es la respuesta
                DataTable dtRespuesta = ds.Tables[0];
                if (dtRespuesta.Rows.Count > 0)
                {
                    //Comprobar respuesta
                    string respuestaSQL = ConvertValueHelper.ConvertirCadena(dtRespuesta.Rows[0]["Respuesta"]);

                    if (respuestaSQL.Equals("OK"))
                    {
                        tipo_usuario = ConvertValueHelper.ConvertirCadena(dtRespuesta.Rows[0]["Tipo_usuario"]);
                        result = true;
                    }
                    else
                        throw new Exception(respuestaSQL);
                }
                else
                    throw new Exception("No se encontró la respuesta del procedimiento");

                if (result)
                {
                    if (tipo_usuario.Equals("COBRADOR"))
                    {
                        Turnos turno = new();
                        Credenciales credencial = new();
                        DataTable dtCobros = new();

                        if (ds.Tables.Count >= 3)
                        {
                            DataTable dtCredencial = ds.Tables[1];

                            //Obtener la credencial
                            if (dtCredencial.Rows.Count > 0)
                                credencial = new Credenciales(dtCredencial.Rows[0]);
                            else
                                throw new Exception("No se encontraron las credenciales");

                            DataTable dtTurno = ds.Tables[2];

                            //Obtener el último turno
                            if (dtTurno.Rows.Count > 0)
                                turno = new Turnos(dtTurno.Rows[0]);
                            else
                                throw new Exception("No se encontro el turno");

                            dtCobros = ds.Tables[3];
                            List<Cobros> cobros = (from DataRow dr in dtCobros.Rows
                                                   select new Cobros(dr)).ToList();

                            loginData.Credenciales = credencial;
                            loginData.Turno = turno;
                            loginData.Cobros = cobros;

                            loginData.CobroDefault = cobros[0];
                            loginData.TipoProductoDefault = cobros[0].Tipo_producto;
                            loginData.ZonaDefault = cobros[0].Zona;
                            loginData.CiudadDefault = cobros[0].Zona.Ciudad;
                            loginData.PaisDefault = cobros[0].Zona.Ciudad.Pais;
                        }
                        else
                        {
                            throw new Exception($"Las tablas del procedimiento Login no vienen completas, son 3 y vienen: {ds.Tables.Count}");
                        }
                    }
                    else if (tipo_usuario.Equals("ADMINISTRADOR"))
                    {
                        Credenciales credencial = new();
                        DataTable dtCredencial = ds.Tables[1];
                        //Obtener la credencial
                        if (dtCredencial.Rows.Count > 0)
                            credencial = new Credenciales(dtCredencial.Rows[0]);

                        loginData.Credenciales = credencial;
                    }
                }
                else
                {
                    throw new Exception("No se pudo iniciar sesión");
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
            return Task.FromResult((rpta, loginData));
        }
    }
}
