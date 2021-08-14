namespace Shubot.Interfaces
{
    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;

    public interface ILoggingService
    {
        public Task LogAsync(LogMessage message);

        public void SetLogEvents(DiscordSocketClient client, CommandService command);
    }
}
