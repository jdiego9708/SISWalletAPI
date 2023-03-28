using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SISWallet.AccesoDatos;
using SISWallet.AccesoDatos.Dacs;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Helpers;
using SISWallet.Entidades.Helpers.Interfaces;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionTwilio;
using SISWallet.Servicios.Interfaces;
using SISWallet.Servicios.Servicios;
using System.Reflection;
using System.Text;
using Twilio;
using Microsoft.AspNetCore.SignalR;
using SISWallet.API.Hubs;
using Google.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddControllers()
            .AddNewtonsoftJson();

        //Assembly GetAssemblyByName(string name)
        //{
        //    Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().
        //           SingleOrDefault(assembly => assembly.GetName().Name == name);

        //    if (assembly == null)
        //        return null;

        //    return assembly;
        //}

        //var a = GetAssemblyByName("SISWallet.API");

        //using var stream = a.GetManifestResourceStream("SISWallet.API.appsettings.json");

        //var config = new ConfigurationBuilder()
        //    .AddJsonStream(stream)
        //    .Build();

        var secrets = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

        builder.Services.AddSingleton(secrets);

        //builder.Configuration.AddConfiguration(config);

        builder.Services.AddSignalR();

        var settings = builder.Configuration.GetSection("TwilioConfiguration");
        TwilioConfiguration twilioConfiguration = settings.Get<TwilioConfiguration>();

        if (twilioConfiguration == null)
            return;

        TwilioClient.Init(twilioConfiguration.AccountSID, twilioConfiguration.AuthToken);

        settings = builder.Configuration.GetSection("Jwt");
        JwtModel jwtSecurity = settings.Get<JwtModel>();

        if (jwtSecurity == null)
            return;

        var llave = Encoding.UTF8.GetBytes(jwtSecurity.Secreto);

        builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

        #region INYECCION DE ACCESO DATOS
        builder.Services.AddTransient<IConexionDac, ConexionDac>()
            .AddTransient<IAgendamiento_cobrosDac, DAgendamiento_cobros>()
            .AddTransient<IUsuariosDac, DUsuarios>()
            .AddTransient<IVentasDac, DVentas>()
            .AddTransient<ITurnosDac, DTurnos>()
            .AddTransient<ITurnosDac, DTurnos>()
            .AddTransient<ISolicitudesDac, DSolicitudes>()
            .AddTransient<ICobrosDac, DCobros>()
            .AddTransient<IGastosDac, DGastos>()
            .AddTransient<IRutas_archivosDac, DRutas_archivos>()
            .AddTransient<IReglasDac, DReglas>()
            .AddTransient<IDireccion_clientesDac, DDireccion_clientes>()
            .AddTransient<ICatalogoDac, DCatalogo>()
            .AddTransient<IPedidosDac, DPedidos>()
            .AddTransient<IProductosDac, DProductos>()

            .AddTransient<INovedadesDac, DNovedades>()
            .AddTransient<IProveedoresDac, DProveedores>()
            .AddTransient<IStock_productosDac, DStock_productos>()
            .AddTransient<IPedidos_proveedorDac, DPedidos_proveedor>()
            .AddTransient<IDetalle_pedidos_proveedorDac, DDetalle_pedidos_proveedor>()
            .AddTransient<IPrecios_productosDac, DPrecios_productos>();

        #endregion

        #region INYECCION DE SERVICIOS
        builder.Services.AddSingleton<INotificationService, NotificationService>();
        builder.Services.AddTransient<IUsuariosServicio, UsuariosServicio>();
        builder.Services.AddTransient<IAgendamientosServicio, AgendamientosServicio>();
        builder.Services.AddTransient<IVentasServicio, VentasServicio>();
        builder.Services.AddTransient<IBlobStorageService, BlobStorageService>();
        builder.Services.AddTransient<IGastosServicio, GastosServicio>();
        builder.Services.AddTransient<ICobrosServicio, CobrosServicio>();
        builder.Services.AddTransient<IReglasServicio, ReglasServicio>();
        builder.Services.AddTransient<ISolicitudesServicio, SolicitudesServicio>();
        builder.Services.AddTransient<IMensajesServicio, MensajesServicio>();
        builder.Services.AddTransient<IProductosServicio, ProductosServicio>();
        builder.Services.AddTransient<ICatalogoServicio, CatalogoServicio>();
        builder.Services.AddTransient<IChatGPTHelper, ChatGPTHelper>();

        #endregion

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSecurity.Issuer,
                    ValidAudience = jwtSecurity.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurity.Secreto))
                };
            });

        builder.Services.AddCors();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        }

        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.UseDefaultFiles();

        app.UseStaticFiles();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<NotificationHub>("/notificationhub");
        });

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
        });

        app.Run();
    }
}