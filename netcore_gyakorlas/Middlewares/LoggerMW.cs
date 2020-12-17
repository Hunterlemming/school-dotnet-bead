using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using netcore_gyakorlas.Models;
using Newtonsoft.Json.Linq;

namespace netcore_gyakorlas.Middlewares
{
    public class LoggerMW
    {
        private readonly RequestDelegate _next;

        public LoggerMW(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments(new PathString("/api")) || !context.User.Identity.IsAuthenticated)
            {
                await _next(context);
                return;
            }
            
            var requestBodyContent = await GetRequestBodyContent(context.Request);
            
            JObject content = String.IsNullOrEmpty(requestBodyContent) ? new JObject() : JObject.Parse(requestBodyContent);
            
            JObject logJson = new JObject();

            logJson.Add("content", content);
            logJson.Add("method", context.Request.Method);
            logJson.Add("endpoint", context.Request.GetEncodedPathAndQuery());
            logJson.Add("userName", context.User.Identity.Name);
            logJson.Add("id", context.User.Claims.FirstOrDefault(x => x.Type==ClaimTypes.Sid)?.Value);
            Console.WriteLine(logJson);
            
            
            await _next(context);
        }

        private async Task<string> GetRequestBodyContent(HttpRequest request)
        {
            request.EnableBuffering();

            var bodyText = await new StreamReader(request.Body).ReadToEndAsync();

            request.Body.Position = 0;

            return bodyText;
        }
    }
    
    
}