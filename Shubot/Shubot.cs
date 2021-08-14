namespace Shubot
{
    using global::Shubot.Interfaces;

    using System;
    using System.Threading.Tasks;

	using Discord;
    using Discord.Commands;
    using Discord.WebSocket;

    public class Shubot
    {
        private readonly IEnvironmentService environmentService;
        private readonly ILoggingService loggingService;

        private CommandService commandService = new CommandService();
        private DiscordSocketClient discordSocketClient = new DiscordSocketClient();
        private CommandHandler commandHandler;

        public Shubot(IEnvironmentService environmentService, ILoggingService loggingService)
        {
            this.environmentService = environmentService;
            this.loggingService = loggingService;
        }

        public async Task MainAsync()
        {
            Initialize();
            Login();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async void Initialize()
        {
            await new CommandHandler(discordSocketClient, commandService).InstallCommandsAsync();

            environmentService.SetEnvironmentVariables();
            loggingService.SetLogEvents(discordSocketClient, commandService);
        }

        private async void Login()
        {
            var token = Environment.GetEnvironmentVariable(EnvironmentConstants.TOKEN);

            await discordSocketClient.LoginAsync(TokenType.Bot, token);
            await discordSocketClient.StartAsync();
        }
    }
}
