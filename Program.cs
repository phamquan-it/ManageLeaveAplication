using ManageLeaveAplication.Models;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(session=>{
    session.Cookie.Name =  "login";
    session.IdleTimeout = new  TimeSpan(0,30,0); 
});
builder.Services.AddCors(options=>{
    options.AddPolicy("AllowAllOrigins",
    builder => 
        builder.AllowAnyOrigin().AllowAnyHeader()
                                                  .AllowAnyMethod()

    );
});
builder.Services.AddDbContext<ManageLeaveAplicationContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultDbConnection")));
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
app.UseCors("AllowAllOrigins");
app.Use(async (context, next) =>
{
    context.Request.Headers.Append("Content-Type", "application/json");
    context.Request.Headers.Append("Connection","keep-alive");
    context.Request.Headers.Append("Accept","*/*");
    await next();
});
app.UseHttpsRedirection();
app.UseSession();

app.UseAuthorization();
app.MapControllers();

app.Run();
