using DapperAPI.Models;

namespace DapperAPI.Utilities
{
    public interface IRandomObjectCreator
    {
        Student CreateRandomStudent();
        decimal GetRandomGrade();
        string Subject(int line);

    }
}