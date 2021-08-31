namespace Shubot.Modules
{
    using global::Shubot.Models;
    using global::Shubot.Models.TwitterModels;

    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using Discord;
    using Discord.Commands;

    using Newtonsoft.Json;

    using RestSharp;

    [Group("twitter")]
    public class TwitterModule : ModuleBase<SocketCommandContext>
    {
        /*[Command("recent")]
		public async Task UserTweetsAsync(string user = null)
		{
            var url = $"https://api.twitter.com/2/tweets/search/recent?query=from:{user}";
            var client = new RestClient(url);
            var response = client.Execute(new RestRequest().AddHeader("Authorization", Environment.GetEnvironmentVariable(EnvironmentConstants.TWITTER_TOKEN)));

            var messages = JsonConvert.DeserializeObject<TweetsModel>(response.Content);

            string reply = string.Empty;
            int count = 0;

            foreach(var msg in messages.data)
            {
                reply += "Tweet Number "+ count++ + msg.text +"\n";
            }

            await ReplyAsync(reply);
		}*/

        [Command("profile")]
        public async Task UserProfileAsync(string user = null)
        {
            var url = $"https://api.twitter.com/2/users/by/username/{user}?user.fields=created_at,description,name,public_metrics,url,profile_image_url";
            var client = new RestClient(url);
            var response = await client.ExecuteAsync(new RestRequest().AddHeader("Authorization", Environment.GetEnvironmentVariable(EnvironmentConstants.TWITTER_TOKEN)));

            var profile = JsonConvert.DeserializeObject<TwitterProfileModel>(response.Content);

            if (profile.data == null)
            {
                await ReplyAsync(Format.Bold($"Twitter user {user} does not exist"));
                return;
            }

            var followingCount = profile.data.public_metrics.following_count;
            var followerCount = profile.data.public_metrics.followers_count;
            var tweetCount = profile.data.public_metrics.tweet_count;

            var embed = new EmbedBuilder()
                 .WithTitle($"{user}'s Twitter Profile")
                 .WithColor(Color.Blue)
                 .AddField("Name", profile.data.name)
                 .AddField("Date of Account Creation", profile.data.created_at.Date.ToShortDateString())
                 .WithThumbnailUrl(profile.data.profile_image_url)
                 .WithUrl($"https://twitter.com/{user}")
                 .WithFooter("Requested by " + Context.User.Username, Context.User.GetAvatarUrl()); ;

            if (!string.IsNullOrEmpty(profile.data.description)) embed.AddField("Description", profile.data.description);
            embed.AddField("Following", (followingCount > 999) ? followingCount.ToString("#,#", CultureInfo.InvariantCulture) : followingCount);
            embed.AddField("Followers", (followerCount > 999) ? followerCount.ToString("#,#", CultureInfo.InvariantCulture) : followerCount);
            embed.AddField("Tweet Count", (tweetCount > 999) ? tweetCount.ToString("#,#", CultureInfo.InvariantCulture) : tweetCount);

            await ReplyAsync(embed: embed.Build());
        }
    }
}
