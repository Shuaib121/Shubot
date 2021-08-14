namespace Shubot
{
    using global::Shubot.Interfaces;

    using System;

    public class EnvironmentService : IEnvironmentService
    {
        public void SetEnvironmentVariables()
        {
            Environment.SetEnvironmentVariable(EnvironmentConstants.TOKEN, "ODcyOTAyOTA0MTA3OTAwOTY5.YQwoLg.wVZp3fo_qQJqEtITPDhq4rrLADU");
            Environment.SetEnvironmentVariable(EnvironmentConstants.TWITTER_TOKEN, "Bearer AAAAAAAAAAAAAAAAAAAAAN8KSgEAAAAA0%2BrpCmSm0Ev5XONFXs%2F2rn8ghjk%3DV9FtAdE4N2XTytBHJMuVgnh36QJBxb1PNXfJbbr4LGPyEYT4Kf");
        }
    }
}
