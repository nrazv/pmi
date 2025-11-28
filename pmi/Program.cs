
using System.Threading.Channels;
using pmi.Data;
using pmi.DataContext;
using pmi.ExecutedTool;
using pmi.ExecutedTool.Repository;
using pmi.ExecutedTool.Service;
using pmi.Project.Builders;
using pmi.Project.Repository;
using pmi.Project.Service;
using pmi.Tool.Models;
using pmi.Tool.Services;
using Microsoft.EntityFrameworkCore;

namespace pmi;

public class Program
{
    public static void Main(string[] args)
    {

        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.WithOrigins("http://localhost:3000", "http://localhost:5000")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowCredentials();
                              });
        });


        builder.Services.AddDbContext<PmiDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );


        // SignalR
        builder.Services.AddSignalR();


        // Channel for jobs
        var channel = Channel.CreateUnbounded<ToolJob>();
        builder.Services.AddSingleton(channel);


        // Add services
        builder.Services.AddScoped<IToolService, ToolService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<IExecutedToolService, ExecutedToolService>();
        builder.Services.AddHostedService<ToolExecutionBackgroundService>();

        // Add repository
        builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
        builder.Services.AddScoped<IExecutedToolRepository, ExecutedToolRepository>();


        builder.Services.AddScoped<ToolsDataJSON>();
        builder.Services.AddScoped<ProjectEntityBuilder>();
        builder.Services.AddScoped<PmiDbContext>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var app = builder.Build();

        var env = app.Environment.EnvironmentName;
        app.Logger.LogInformation("Running in environment: {Environment}", env);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        //To be removed
        app.UseSwagger();
        app.UseSwaggerUI();

        // Serve static files (React builds in wwwroot)
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseCors(MyAllowSpecificOrigins);
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.MapHub<ToolHub>("/toolhub");
        app.UseWebSockets();
        app.Run();
    }
}
