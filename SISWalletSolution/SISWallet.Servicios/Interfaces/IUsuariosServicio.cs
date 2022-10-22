﻿using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;

namespace SISWallet.Servicios.Interfaces
{
    public interface IUsuariosServicio
    {
        RespuestaServicioModel ProcesarLogin(LoginModel login);
    }
}