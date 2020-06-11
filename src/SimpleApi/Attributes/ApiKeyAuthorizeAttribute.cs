using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleAPI.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ApiKeyAuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        public ApiKeyAuthorizeAttribute()
        {
            //_apiKey = _configuration.GetValue<string>("Api:Key");
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //before
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorization))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Date", out var xapidate))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Hash", out var hash))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            
            //Authorization
            var userName = configuration.GetValue<string>("Api:Username");
            var password = configuration.GetValue<string>("Api:Password");
            var compareAuth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ":" + password));

            if (string.IsNullOrEmpty(authorization) || compareAuth != authorization)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            //Timestamp
            if (!DateTime.TryParseExact(xapidate,
                                       "ddd, dd MMM yyyy HH:mm:ss GMT",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.AdjustToUniversal,
                                       out var dateValue))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var validFor = configuration.GetValue<int>("Api:ValidForMinutes");
            
            if(DateTime.UtcNow > dateValue.AddMinutes(validFor))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            //Content Hash
            var key = configuration.GetValue<string>("Api:Key");

            //context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin); 

            using (StreamReader stream = new StreamReader(context.HttpContext.Request.Body))
            {
                string body = await stream.ReadToEndAsync();
                if(!string.IsNullOrEmpty(body))
                {
                    if (ComputeHashUsingSHA256(body, key) != hash)
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }
                }
            }

            await next().ConfigureAwait(false);

            //after
        }

        private static string ComputeHashUsingSHA256(string content, string secret, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] contentByte = encoding.GetBytes(content);
            byte[] secretByte = encoding.GetBytes(secret);
            using (var hmacsha256 = new HMACSHA256(secretByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(contentByte);
                return Convert.ToBase64String(hashmessage);
            }
        }
    }
}
