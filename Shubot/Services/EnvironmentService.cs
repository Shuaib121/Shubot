namespace Shubot
{
    using global::Shubot.Interfaces;

    using System;

    public class EnvironmentService : IEnvironmentService
    {
        string[] tokens = File.ReadAllLines(@"C:\Users\user-pc\source\repos\Shubot\Shubot\Tokens\API_TOKENS.txt");

        public void SetEnvironmentVariables()
        {
            Environment.SetEnvironmentVariable(EnvironmentConstants.TOKEN, tokens[0]);
            Environment.SetEnvironmentVariable(EnvironmentConstants.TWITTER_TOKEN, tokens[1]);
            Environment.SetEnvironmentVariable(EnvironmentConstants.RAPID_API_TOKEN, tokens[2]);
        }
    }
}
