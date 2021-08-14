namespace Shubot.Models
{
    using System;

    public class TwitterProfileModel
    {
        public TwitterProfile data;
    }

    public class TwitterProfile
    {
        public DateTime created_at { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public TwitterPublicMetricsModel public_metrics { get; set; }
        public string description { get; set; }
        public string profile_image_url { get; set; }
        public string id { get; set; }
    }
}
