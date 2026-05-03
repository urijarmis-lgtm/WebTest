namespace WebTest.Config
{
    public class TestSettings
    {
        public string BaseUrl { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string ProductName { get; set; } = "";
        public string ExpectedCartCount { get; set; } = "";
        public string Browser { get; set; } = "";
        public bool Headless { get; set; }
        public int TimeoutSeconds { get; set; }
    }
}