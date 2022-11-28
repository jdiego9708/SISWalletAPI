using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Models;

namespace SISWallet.AccesoDatos.Dacs
{
    public class DDireccion_clientes : IDireccion_clientesDac
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
        public DDireccion_clientes(IConexionDac iConexionDac)
        {
            IConexionDac = iConexionDac;
        }

        #endregion

        #region METODO INSERTAR
        public Task<string> InsertarDireccion(Direccion_clientes direccion)
        {
            int contador = 0;
            string rpta = "";

            string consulta = "INSERT INTO Direccion_clientes (Id_usuario, Id_zona, Direccion, Estado_direccion) " +
                "VALUES(@Id_usuario, @Id_zona, @Direccion, @Estado_direccion) " +
                "SET @Id_direccion = SCOPE_IDENTITY() ";

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

                SqlParameter Id_direccion = new()
                {
                    ParameterName = "@Id_direccion",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                SqlCmd.Parameters.Add(Id_direccion);

                SqlParameter Id_usuario = new()
                {
                    ParameterName = "@Id_usuario",
                    SqlDbType = SqlDbType.Int,
                    Value = direccion.Id_usuario
                };
                SqlCmd.Parameters.Add(Id_usuario);
                contador += 1;

                SqlParameter Id_zona = new()
                {
                    ParameterName = "@Id_zona",
                    SqlDbType = SqlDbType.Int,
                    Value = direccion.Id_zona
                };
                SqlCmd.Parameters.Add(Id_zona);
                contador += 1;

                SqlParameter Direccion = new()
                {
                    ParameterName = "@Direccion",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 200,
                    Value = direccion.Direccion.Trim().ToUpper(),
                };
                SqlCmd.Parameters.Add(Direccion);
                contador += 1;

                SqlParameter Estado_direccion = new()
                {
                    ParameterName = "@Estado_direccion",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 50,
                    Value = direccion.Estado_dirección.Trim()
                };
                SqlCmd.Parameters.Add(Estado_direccion);
                contador += 1;

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
                    direccion.Id_direccion = Convert.ToInt32(SqlCmd.Parameters["@Id_direccion"].Value);
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
