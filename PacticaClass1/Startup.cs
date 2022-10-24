using DevExpress.Data;
using DevExpress.Data.Browsing;
using Microsoft.EntityFrameworkCore;
using PacticaClass1.Interfaces;
using PacticaClass1.Models;
using PacticaClass1.Services;
using System.Text.Json.Serialization;

namespace PacticaClass1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public static WebApplication StartApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<PruebaInterface, PruebaServices>();

            builder.Services.AddScoped<IPersona, PersonaServices>();
            builder.Services.AddScoped<IUsuario, UsuarioServices>();
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<Models.DataContext>(x => x.UseSqlServer(connectionString));
            builder.Services.AddMvc();
        }
        private static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }    
}
