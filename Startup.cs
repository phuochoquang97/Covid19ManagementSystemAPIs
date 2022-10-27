using System.Text;
using System.Threading.Tasks;
using Covid_Project.Domain.Models.Email;
using Covid_Project.Domain.Repositories;
using Covid_Project.Domain.Repositories.Homepage;
using Covid_Project.Domain.Services.Authorization;
using Covid_Project.Domain.Services.Homepage;
using Covid_Project.Persistence.Context;
using Covid_Project.Persistence.Repositories.Authorization;
using Covid_Project.Persistence.Repositories.Homepage;
using Covid_Project.Services.Authorization;
using Covid_Project.Services.Homepage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Covid_Project.Domain.Repositories.Authorization;
using Covid_Project.Domain.Services.Confirmation;
using Covid_Project.Services.Confirmation;
using Covid_Project.Domain.Repositories.Confirmation;
using Covid_Project.Persistence.Repositories.Confirmation;
using Covid_Project.Domain.Repositories.User;
using Covid_Project.Persistence.Repositories.User;
using Covid_Project.Services.User;
using Covid_Project.Domain.Services.User;
using Covid_Project.Domain.Repositories.Common;
using Covid_Project.Persistence.Repositories.Common;
using Covid_Project.Services.Common;
using Covid_Project.Domain.Services.Common;
using Covid_Project.Persistence.Helper;
using Covid_Project.Domain.Repositories.Admin;
using Covid_Project.Persistence.Repositories.Admin;
using Covid_Project.Domain.Services.Admin;
using Covid_Project.Services.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Covid_Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Covid_Project", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AppCnn")));
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<IRegisterRepository, RegisterRepository>();
            services.AddTransient<IRandomCodeGeneratorService, RandomCodeGeneratorService>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IHomepageService, HomepageService>();
            services.AddTransient<IHomepageRepository, HomepageRepository>();
            services.AddTransient<IEmailConfirmationService, EmailConfirmationService>();
            services.AddTransient<IEmailConfirmationRepository, EmailConfirmationRepository>();

            var key = Configuration.GetSection("JwtKey").Value;
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));

            // Email Service
            services.Configure<EmailConfiguration>(Configuration.GetSection("EmailConfiguration"));
            services.AddScoped<IEmailService, EmailService>();

            // CORS Configuration
            // services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            // {
            //     builder.WithOrigins("http://localhost:5000")
            //     .AllowAnyMethod()
            //     .AllowAnyHeader()
            //     .AllowAnyOrigin();
            // }));
            services.AddCors();
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();
            services.AddTransient<IUserProfileService, UserProfileService>();
            services.AddTransient<IUserItineraryRepository, UserItineraryRepository>();
            services.AddTransient<IUserItineraryService, UserItineraryService>();
            services.AddTransient<IRoleRepository, RoleRespository>();

            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICityService, CityService>();

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddTransient<ITestingLocationRepository, TestingLocationRepository>();
            services.AddTransient<ITestingLocationService, TestingLocationService>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IUserTestingRepository, UserTestingRepository>();
            services.AddTransient<IUserTestingService, UserTestingService>();
            services.AddTransient<IAdminTestingRepository, AdminTestingRepository>();
            services.AddTransient<IAdminTestingService, AdminTestingService>();
            services.AddTransient<IAdminItineraryRepository, AdminItineraryRepository>();
            services.AddTransient<IAdminItineraryService, AdminItineraryService>();

            services.AddTransient<IAdminUserRepository, AdminUserRepository>();
            services.AddTransient<IAdminUserService, AdminUserService>();

            services.AddTransient<IAdminHomepageRepository, AdminHomepageRepository>();
            services.AddTransient<IAdminHomePageService, AdminHomepageService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if(env.IsDevelopment())
            // {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Covid_Project v1"));
            //}

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "Image")),
                RequestPath = "/Image"
            });


            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // app.Run(context =>
            // {
            //     context.Response.Redirect("swagger");
            //     return Task.CompletedTask;
            // });
        }
    }
}
