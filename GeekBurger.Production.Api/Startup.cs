using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using GeekBurger.Production.Infra.Ioc;

namespace GeekBurger.Production.Api
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        #region| Fields |

        public IConfiguration Configuration { set; get; }

        #endregion

        #region| Constructor |

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region| Methods |

        public void ConfigureServices(IServiceCollection services)
        {
            Bootstrapper.RegisterServices(services, Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "GeekBurger Production", Version = "v1" });
                a.IncludeXmlComments(GetXmlCommentsPath());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json",
                                 optional: false,
                                 reloadOnChange: true)
                            .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
                app.UseDeveloperExceptionPage();
            }

            Configuration = builder.Build();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekBurger Production V1");
            });

            app.UseMvc();
        }

        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\GeekBurger.Production.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }

        #endregion
    }
}
