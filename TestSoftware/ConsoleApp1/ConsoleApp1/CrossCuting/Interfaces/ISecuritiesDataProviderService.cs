using ConsoleApp1.Models;
using System.Threading.Tasks;

namespace ConsoleApp1.CrossCuting
{
    public interface ISecuritiesDataProviderService
    {
        Task<DataProviderIsinResponse?> GetIsinAsync(string? isin);
    }
}