namespace Shubot.Modules
{
    using global::Shubot.Models;

    using System;
    using System.Threading.Tasks;

    using Discord;
    using Discord.Commands;

    using Newtonsoft.Json;

    using RestSharp;

    //[Group("weather")]
    public class WeatherModule : ModuleBase<SocketCommandContext>
    {
        [Command("weather")]
        public async Task WeatheDataAsync(string city = null)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                await ReplyAsync(Format.Bold($"City does not exist"));
                return;
            }

            city = char.ToUpper(city[0]) + city.Substring(1);

            var client = new RestClient($"https://community-open-weather-map.p.rapidapi.com/weather?q={city}&units=metric");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", Environment.GetEnvironmentVariable(EnvironmentConstants.RAPID_API_TOKEN));
            IRestResponse response = client.Execute(request);

            var weatherData = JsonConvert.DeserializeObject<WeatherModel>(response.Content);

            if (weatherData.main == null)
            {
                await ReplyAsync(Format.Bold($"City {city} does not exist"));
                return;
            }

            var weatherIcon = weatherData.weather[0].icon;

            var embed = new EmbedBuilder()
                 .WithTitle($"{city}'s Weather")
                 .WithColor(Color.Blue)
                 .WithThumbnailUrl($"https://openweathermap.org/img/wn/{weatherIcon}.png")
                 .AddField("Temperature", (int)weatherData.main.temp + "°C")
                 .AddField("Feels Like", (int)weatherData.main.feels_like + "°C")
                 .AddField("Humidity", weatherData.main.humidity + "%")
                 .WithDescription($"{weatherData.weather[0].main}, {weatherData.weather[0].description}")
                 .WithFooter("Requested by " + Context.User.Username, Context.User.GetAvatarUrl()); ;

            await ReplyAsync(embed: embed.Build());
        }
    }
}
