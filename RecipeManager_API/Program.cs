
using Microsoft.EntityFrameworkCore;
using RecipeManager_API.Data;
using RecipeManager_API.Seeder;

namespace RecipeManager_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Regristiert DBContext und legt In-Memory-Db 
            builder.Services.AddDbContext<RecipeDBContext>(options =>
                options.UseInMemoryDatabase("RecipeDb"));

            // CORS hinzufuegen
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowWpfClient", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Seeder Aufruf
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<RecipeDBContext>();
                DbSeeder.SeedData(context);
            }
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowWpfClient");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
