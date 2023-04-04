namespace Bumble_bee_FE.Models
{
    public class Token
    {
        public bool status { get; set; }
        public string? errorMessage { get; set; }
        public string? accessToken { get; set; }
        public string? userId { get; set; }
        public string? userName { get; set; }       
    }
}
