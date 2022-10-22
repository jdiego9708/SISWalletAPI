using SISWallet.Entidades.Models;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface ITurnosDac
    {
        Task<string> InsertarTurno(Turnos turno);
        Task<string> EditarVenta(Turnos turno);
        Task<(DataTable dt, string rpta)> BuscarTurnos(string tipo_busqueda, string[] textos_busqueda);
    }
}
