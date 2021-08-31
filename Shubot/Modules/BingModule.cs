namespace Shubot.Modules
{
    using global::Shubot.Models;

    using System;
    using System.Threading.Tasks;

    using Discord;
    using Discord.Commands;

    using Newtonsoft.Json;

    using RestSharp;

    public class BingModule : ModuleBase<SocketCommandContext>
    {
        [Command("image")]
        public async Task ImageSearchAsync(string imageName = null)
        {
            if (string.IsNullOrWhiteSpace(imageName))
            {
                await ReplyAsync(Format.Bold($"Invalid Input"));
                return;
            }
            
            imageName = char.ToUpper(imageName[0]) + imageName.Substring(1);

            var client = new RestClient($"https://bing-image-search1.p.rapidapi.com/images/search?q={imageName}&safeSearch=Strict&count=150");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", Environment.GetEnvironmentVariable(EnvironmentConstants.RAPID_API_TOKEN));
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                await ReplyAsync(Format.Bold($"Images for {imageName} do not exist"));
                return;
            }

            var bingModel = JsonConvert.DeserializeObject<BingImageModel>(response.Content);

            if(bingModel.value.Count == 0)
            {
                await ReplyAsync(Format.Bold($"Images for {imageName} do not exist"));
                return;
            }

            var randomImageIndex = new Random().Next(0, 100);

            var embed = new EmbedBuilder()
                .WithTitle($"{imageName} Picture")
                .WithColor(Color.Blue)
                .WithImageUrl(bingModel.value[randomImageIndex].thumbnailUrl)
                .WithFooter("Requested by " + Context.User.Username, Context.User.GetAvatarUrl());
                

            await ReplyAsync(embed: embed.Build());
        }
    }
}
