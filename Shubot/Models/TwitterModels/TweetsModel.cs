namespace Shubot.Models.TwitterModels
{
    public class TweetsModel
    {
        public Tweets[] data { get; set; }
    }

    public class Tweets
    {
        public string id { get; set; }
        public string text { get; set; }
    }
}
