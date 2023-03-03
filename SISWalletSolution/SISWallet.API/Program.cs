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
using Twilio.Rest.Api.V2010.Account;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

Assembly GetAssemblyByName(string name)
{
    Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().
           SingleOrDefault(assembly => assembly.GetName().Name == name);

    if (assembly == null)
        return null;

    return assembly;
}

var a = GetAssemblyByName("SISWallet.API");

using var stream = a.GetManifestResourceStream("SISWallet.API.appsettings.json");

var config = new ConfigurationBuilder()
    .AddJsonStream(stream)
    .Build();

builder.Configuration.AddConfiguration(config);

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
    .AddTransient<IDireccion_clientesDac, DDireccion_clientes>();

#endregion

#region INYECCION DE SERVICIOS
builder.Services.AddTransient<IUsuariosServicio, UsuariosServicio>();
builder.Services.AddTransient<IAgendamientosServicio, AgendamientosServicio>(); 
builder.Services.AddTransient<IVentasServicio, VentasServicio>();
builder.Services.AddTransient<IBlobStorageService, BlobStorageService>();
builder.Services.AddTransient<IGastosServicio, GastosServicio>();
builder.Services.AddTransient<ICobrosServicio, CobrosServicio>();
builder.Services.AddTransient<IReglasServicio, ReglasServicio>();
builder.Services.AddTransient<ISolicitudesServicio, SolicitudesServicio>();
builder.Services.AddTransient<IMensajesServicio, MensajesServicio>();
#endregion

//builder.Services.AddHttpContextAccessor()
//                .AddAuthorization()
//                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(options =>
//                {
//                    options.RequireHttpsMetadata = false;
//                    options.SaveToken = true;
//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        ValidateIssuer = false,
//                        ValidateAudience = false,
//                        ValidateLifetime = true,
//                        ValidateIssuerSigningKey = false,
//                        IssuerSigningKey = new SymmetricSecurityKey(llave),
//                        ClockSkew = TimeSpan.Zero
//                    };
//                });

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "https://siswalletapi.azurewebsites.net"; // URL de la autoridad de autenticación
    options.Audience = jwtSecurity.Audience; // Identificador de audiencia del API
});

builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseSwagger();

app.UseSwaggerUI();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseDefaultFiles();

app.UseStaticFiles();

app.Run();
