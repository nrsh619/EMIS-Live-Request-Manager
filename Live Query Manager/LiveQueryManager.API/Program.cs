
using LiveQueryManager.DataAccess.Context;
using LiveQueryManager.DataAccess.DA;
using Microsoft.EntityFrameworkCore;

namespace LiveQueryManager.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();

			builder.Services.AddEntityFrameworkNpgsql().AddDbContext<LiveQueryManagerDbContext>(optionsAction: opt=>
			opt.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
				);
			builder.Services.AddScoped<LiveRequestDA>();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();			
			builder.Services.AddControllers().AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
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