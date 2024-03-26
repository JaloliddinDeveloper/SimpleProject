//==================================================
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//==================================================

using SimpleProject.Api.Brokers.Loggings;
using SimpleProject.Api.Brokers.Storages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IStorageBroker, StorageBroker>();
builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
