using SISWallet.Entidades.Models;
using System.Data;

namespace SISWallet.AccesoDatos.Interfaces
{
    public interface IAgendamiento_cobrosDac
    {
        Task<string> ActualizarOrden(int id_agendamiento, int orden);
        Task<string> InsertarAgendamiento(Agendamiento_cobros agendamiento);
        Task<string> CambiarEstadoAgendamiento(int id_agendamiento, string estado);
        Task<(DataTable dtAgendamientos, string rpta)> BuscarAgendamiento(string tipo_busqueda, string[] textos_busqueda);
        Task<string> TerminarAgendamiento(int id_agendamiento, string estado, decimal valor_pagado, decimal saldo_restante);
    }
}
