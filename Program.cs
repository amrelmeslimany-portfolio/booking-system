using api.Config;
using api.Config.Exceptions;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInitInjects(builder.Configuration);
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseCustomAppException();

app.UseDefaultFiles();
app.UseStaticFiles();

// Add Comment for testing github

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(op => op.ConfigObject.AdditionalItems.Add("persistAuthorization", "true"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/dashboard-jobs");

app.MapControllers();

app.Run();
