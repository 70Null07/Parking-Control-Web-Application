namespace TestWebApplicationHTTP.Models
{
    public class AvgLoadByHour
    {
        public DateOnly Date { get; set; }
        public int Hour { get; set; }
        public int AvgLoad {  get; set; }
    }
}
