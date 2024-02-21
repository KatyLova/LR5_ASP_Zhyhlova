using System.Text;
using LR5_ASP_Zhyhlova;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));

var app = builder.Build();

app.Run(async (context) =>
{
    try
    {
        var response = context.Response;
        response.ContentType = "text/html; charset=utf-8";
        var request = context.Request;
        StringBuilder sb = new StringBuilder();
        sb.Append("<form method='POST'>" +
                  "<div style='color: #4eb5b5; font-family: Century Gothic; font-size: 16pt;'>Name: <input name='name' />" +
                  "<div style='color: #4eb5b5; font-family: Century Gothic; font-size: 16pt;'>Date: <input name='date' type='datetime-local' /></div></div>" +
                  "<div style='margin-left: 75px; margin-top: 10px;'><input type='submit' value='Send' /></div>" +
                  "</form>"
        );
        sb.Append("<div <div style='color: #ba840f; font-family: Century Gothic; font-size: 18pt;'> Cookies: </div>");
        if (request.Cookies["name"] != null && request.Cookies["date"] != null)
        {
            sb.Append("<div style='color: #fabb32; font-family: Century Gothic; font-size: 16pt;'>Name: " +
                      request.Cookies["name"] + "</div>");
            sb.Append("<div style='color: #fabb32; font-family: Century Gothic; font-size: 16pt;'>Date: " +
                      request.Cookies["date"] + "</div>");
        }
        else
        {
            sb.Append("There are no cookies!");
        }

        if (request.Method == "POST")
        {
            response.Cookies.Append("name", request.Form["name"]);
            response.Cookies.Append("date", request.Form["date"]);
        }

        await response.WriteAsync(sb.ToString());
    }
    catch (Exception exception)
    {
        app.Logger.LogInformation("Found exception: " + exception);
        await context.Response.WriteAsync("Found exception - for details go to file 'logger.txt'");
    }
});

app.Run();