using SISWallet.Entidades.Models;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface ITurnosDac
    {
        Task<(DataTable dt, string rpta)> EstadisticasDiarias(int id_turno, DateTime fecha);
        Task<(DataTable dt, string rpta)> SincronizarClientes(int id_cobro, int id_usuario, DateTime fecha);
        Task<string> InsertarTurno(Turnos turno);
        Task<string> EditarVenta(Turnos turno);
        Task<(DataTable dt, string rpta)> BuscarTurnos(string tipo_busqueda, string[] textos_busqueda);
        Task<(DataTable dt, string rpta)> BuscarTurnos(string tipo_busqueda, string texto_busqueda);
    }
}
