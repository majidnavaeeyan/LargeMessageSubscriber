using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace LargeMessageSubscriber.Presentation.Middlewares
{
  public class RequestResponseLogger
  {
    private readonly RequestDelegate _next;
    private readonly string _logPath;

    public RequestResponseLogger(RequestDelegate next, IConfiguration configuration)
    {
      _next = next;
      _logPath = configuration.GetSection("LogPath").Value;
    }

    public async Task Invoke(HttpContext context)
    {
      var request = await FormatRequestAsync(context.Request);
      await LogToFileAsync($"Time : {DateTime.Now}\r\n--------------------------------\r\nRequest: {request}");

      var originalResponseBodyStream = context.Response.Body;

      using (var responseBody = new MemoryStream())
      {
        context.Response.Body = responseBody;

        await _next(context);

        var response = await FormatResponseAsync(context.Response);
        await LogToFileAsync($"Time : {DateTime.Now}\r\n--------------------------------\r\nResponse: {response}\r\n===========================================================================================================================\r\n");

        await responseBody.CopyToAsync(originalResponseBodyStream);
      }
    }

    private async Task<string> FormatRequestAsync(HttpRequest request)
    {
      request.EnableBuffering();
      var body = request.Body;

      var buffer = new byte[Convert.ToInt32(request.ContentLength)];
      await body.ReadAsync(buffer, 0, buffer.Length);
      var bodyAsText = Encoding.UTF8.GetString(buffer);

      body.Seek(0, SeekOrigin.Begin);

      return $"{request.Method} {request.Path} {request.QueryString} {bodyAsText}";
    }

    private async Task<string> FormatResponseAsync(HttpResponse response)
    {
      response.Body.Seek(0, SeekOrigin.Begin);
      var text = await new StreamReader(response.Body).ReadToEndAsync();
      response.Body.Seek(0, SeekOrigin.Begin);

      return text;
    }

    private async Task LogToFileAsync(string logMessage)
    {
      var logFilePath = @$"{_logPath}\Logs\{DateTime.Today.ToString("yyyyy-MM-dd")}.log";
      Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
      File.AppendAllTextAsync(logFilePath, $"{logMessage}\n");
    }
  }

  public static class RequestResponseLoggerMiddlewareExtensions
  {
    public static IApplicationBuilder RequestResponseLogger(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<RequestResponseLogger>();
    }
  }
}