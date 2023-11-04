using SistemaVentas.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*Configuracion para leer cadena de conexion -> cadenaSQL*/
builder.Services.InyectarDependecias(builder.Configuration);

/*Implementar Cors*/
builder.Services.AddCors(options => {
	options.AddPolicy("NuevaPolitica", app => {
		app.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod();
	}); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

/*Activar Cors*/
app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
