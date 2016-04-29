using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Configuration;

[assembly: OwinStartup(typeof(NotificationService.Api.Startup))]

namespace NotificationService.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var redisServerName = ConfigurationManager.AppSettings["RedisServerName"];
            var portNumber = Int32.Parse(ConfigurationManager.AppSettings["PortNumber"]);
            var password = ConfigurationManager.AppSettings["Password"];

            var connectionString = String.Format("{0}:{1},ssl=true,password={2}", redisServerName, portNumber, password);
            var redisScaleoutConfiguration = new RedisScaleoutConfiguration(connectionString, "NotificationService");
            GlobalHost.DependencyResolver.UseRedis(redisScaleoutConfiguration);

            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR(new HubConfiguration()
            {
                EnableDetailedErrors = true,
                EnableJSONP = true,
            });
        }
    }
}
