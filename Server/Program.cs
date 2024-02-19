using Server;
using System.Data.SQLite;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IOrderRepository>(provider =>
{
    string connectPath = "Data Source=DataBase.db";
    IOrderRepository orderRepository = new SqlLiteOrderRepository(connectPath);
    return orderRepository;
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

