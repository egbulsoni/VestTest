using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using VestTest;

namespace VestTest
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Registre o DbContext com MySQL
            services.AddDbContext<VestTestDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                                 ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));

            // Registre os geradores
            services.AddTransient<GeradorDeDadosOfertaCursos>();
            services.AddTransient<GeradorDeDadosCandidatos>();
            services.AddTransient<GeradorDeDadosInscricoes>();
            services.AddTransient<GeradorDeDadosProcessoSeletivo>();

            // Registre o Gerador Completo
            services.AddTransient<GeradorCompletoDeDados>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Gerar os dados quando a aplicação for iniciada
            GerarDados(app);

            // Resto da configuração do pipeline...
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void GerarDados(IApplicationBuilder app)
        {
            // Obter a instância do Gerador Completo a partir do DI
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var geradorCompletoDeDados = scope.ServiceProvider.GetRequiredService<GeradorCompletoDeDados>();
                geradorCompletoDeDados.GerarDados();
            }
        }
    }
}
