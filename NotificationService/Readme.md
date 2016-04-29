To run this project, there are a few spots needed to change the configuration to match what you have setup.

The Redis Server Url
NotificationService.Api\Web.config:
	<appSettings>
		<add key="RedisServerName" value="jwrediscache.redis.cache.windows.net" />
		<add key="PortNumber" value="6380" />
		<add key="Password" value="" />
	</appSettings>



The SignalR Hub Url
NotificationService.Desktop\App.config:
	<add key="SignalRHubUri" value="http://jwsignalr.azurewebsites.net/" />

NotificationService.Publisher\App.config:
	<add key="SignalRHubUri" value="http://jwsignalr.azurewebsites.net/" />

NotificationService.Web\Index.html (at bottom):
	<script src="http://jwsignalr.azurewebsites.net/signalr/hubs"></script>

NotificationService.Web\app\services\signalr.js:
	$.connection.hub.url = "http://jwsignalr.azurewebsites.net/signalr";



Need to install additional counters on web app
http://www.asp.net/signalr/overview/performance/using-signalr-performance-counters-in-an-azure-web-role



To run additional load on the system as a whole, change the url inside Performance\RunCrank.bat to point to your SignalR Hub Server
	.\Crank\crank.exe /Url="http://jwsignalr.azurewebsites.net/"

Then open a command prompt in that directory and run RunCrank.bat