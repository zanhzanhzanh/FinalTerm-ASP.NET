using FinalTerm.Interfaces;
using FinalTerm.Repository;

namespace FinalTerm.Startup {
    public static class ApplicationSettings {
        public static IServiceCollection ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration) {
            // Add services to the container.
            services.AddControllers();

            // Register Services for IServiceCollection
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            // Config AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Config Database
            services.AddDbContext<DataContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Config Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static WebApplication ConfigureMiddleware(this WebApplication app) {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
