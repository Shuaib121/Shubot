namespace Shubot.Modules
{
    using System.Linq;
    using System.Threading.Tasks;

    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;

	// Create a module with the 'user' prefix
	[Group("user")]
	public class InfoModule : ModuleBase<SocketCommandContext>
	{
		[Command("info")]
		[Summary
		("Returns info about the current user, or the user parameter, if one passed.")]
		[Alias("user", "whois")]
		public async Task UserInfoAsync(SocketUser user = null)
		{
			string activeClients = null, activities = null, currentActivity = null;
			var userInfo = user ?? Context.Client.CurrentUser;

			userInfo.ActiveClients.ToList().ForEach(_ => activeClients+= $"{_}, ");
			userInfo.Activities.ToList().ForEach(_ => activities += $"{_.Name} ");

			activeClients = activeClients.Remove(activeClients.Length - 2);
			if (userInfo.Activity != null) { currentActivity = userInfo.Activity.Name; }

			var embed = new EmbedBuilder()
			     .WithTitle($"{user.Username}'s Discord Information")
				 .WithColor(Color.Blue)
				 .AddField("Tag", $"{userInfo.Username}#{userInfo.Discriminator}")
				 .AddField("Active Clients", $"{IsDataAvailable(activeClients)}")
				 .AddField("Activities", $"{IsDataAvailable(activities)}")
				 .AddField("Current Activity", $"{IsDataAvailable(currentActivity)}")
				 .AddField("Date of Account Creation", $"{userInfo.CreatedAt.DateTime}")
				 .WithThumbnailUrl(userInfo.GetAvatarUrl());

			await ReplyAsync(embed: embed.Build());
		}

		// checks if the an object is null and returns a string message.
		private object IsDataAvailable(object obj)
        {
			if(obj == null) return "None available";

			return obj;
        }
	}
}
