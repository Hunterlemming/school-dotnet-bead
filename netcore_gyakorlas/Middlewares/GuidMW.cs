using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace netcore_gyakorlas.Middlewares
{
    public class GuidMW
    {

        private readonly RequestDelegate _next;

        public GuidMW(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments(new PathString("/api")))
            {
                await _next(context);
                return;
            }
            
            var requestBodyContent = await GetRequestBodyContent(context.Request);

            JObject json = String.IsNullOrEmpty(requestBodyContent) ? new JObject() : JObject.Parse(requestBodyContent);
            Guid guid = Guid.NewGuid();
            json.Add("guid", guid);
            context.Request.Body = GenerateStreamFromString(json.ToString());

            await _next(context);
        }

        private Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
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