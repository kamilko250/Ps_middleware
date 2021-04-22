using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAParser;

namespace Net__middleware
{
    public class Middleware
    {
        private RequestDelegate _request;
        public Middleware(RequestDelegate request)
        {
            _request = request;
        }
        public Task Invoke(HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo client = uaParser.Parse(userAgent);
            string browser = client.UserAgent.Family;
            if (browser == "Edge" || browser == "IE" || browser == "EgdeChromium")
            {
                context.Response.WriteAsync($"<p>{browser } - Przegladarka nieobslugiwana</p>");
            }
            return _request(context);
        }
    }
}
