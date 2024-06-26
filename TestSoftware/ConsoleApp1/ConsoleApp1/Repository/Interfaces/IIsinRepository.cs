using ConsoleApp1.Models;

namespace ConsoleApp1.Repository.Interfaces
{
    public interface IIsinRepository
    {
        IsinModel GetIsin(string? isin);
        InsertResponse Insert(IsinModel isinModel);
        UpdateResponse Update(string? isin);
        DeleteResponse Dalete(string? isin);
    }
}