namespace DapperAPI.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public decimal Number { get; set; } = 0m;
        public int StudentId { get; set; }
        public string Category { get; set; }
        public decimal WeightPercent { get; set; } = 0m;

    }
}