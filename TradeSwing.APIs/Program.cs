using TradeSwing.APIs;
using TradeSwing.Application;
using TradeSwing.Infrastructure;
using TradeSwing.APIs.Configuration;

var builder = WebApplication.CreateBuilder(args);
{
    // builder.Services.InstallServices(builder.Configuration, typeof(IServiceInstaller).Assembly);
    builder.Services.AddApplication()
        .AddPresentation()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
