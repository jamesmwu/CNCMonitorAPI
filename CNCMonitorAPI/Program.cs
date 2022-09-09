using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Enable CORS
builder.Services.AddCors(c =>
{
    //c.AddPolicy("corsapp", builder =>
    //{
    //    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    //});

    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

    //c.AddPolicy("AllowOrigin", options => {
    //    options.WithOrigins("https://localhost:7024", "*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

    //});


    //var frontendURL = Configuration.GetValue<string>("frontend_url");
    //c.AddDefaultPolicy(builder =>
    //{
    //    builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader()
    //        .WithExposedHeaders(new string[] { "totalAmountOfRecords" });
    //});

});

//JSON Serializer
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
    = new DefaultContractResolver());


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

