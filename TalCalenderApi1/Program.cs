var builder = WebApplication.CreateBuilder(args);

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

app.UseCors(policy =>
{
    policy.WithOrigins("http://localhost:3000")  // Adjust as needed for your frontend URL
          .AllowAnyMethod()
          .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



////using Microsoft.AspNetCore.Builder;
////using Microsoft.AspNetCore.Hosting;
////using Microsoft.Extensions.DependencyInjection;
////using Microsoft.Extensions.Hosting;
////using TalCalenderApi1.Model;

////var builder = WebApplication.CreateBuilder(args);

////// Add services to the container.
//////builder.Services.AddSingleton<ITimeSlotService, TimeSlotService>();
////builder.Services.AddControllers();

////var app = builder.Build();

////// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.UseDeveloperExceptionPage();
////}

////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}

////app.UseRouting();

////// Enable CORS
////app.UseCors(policy =>
////{
////    policy.WithOrigins("http://localhost:3000")  // Adjust as needed for your frontend URL
////          .AllowAnyMethod()
////          .AllowAnyHeader();
////});

////app.UseAuthorization();

////app.MapControllers();

////app.Run();
