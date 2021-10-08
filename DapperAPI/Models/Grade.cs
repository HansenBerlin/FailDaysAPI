namespace DapperAPI.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Category { get; set; }
        public double Number { get; set; }
    }
}