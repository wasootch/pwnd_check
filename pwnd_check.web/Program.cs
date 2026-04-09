using pwnd_check.common;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

var app = builder.Build();
app.UseStaticFiles();
app.MapRazorPages();

app.MapPost("/check", async (CheckRequest req) =>
{
    try
    {
        var sha1 = HibpService.ComputeSha1(req.Password);
        var (found, count) = await HibpService.CheckPasswordAsync(req.Password);
        return Results.Ok(new { sha1, found, count });
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();

record CheckRequest(string Password);
