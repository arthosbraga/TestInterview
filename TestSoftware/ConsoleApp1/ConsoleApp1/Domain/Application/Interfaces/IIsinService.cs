using ConsoleApp1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1.Application.Interfaces
{
    public interface IIsinService
    {
        Task<List<IsinResponse>> GetBatchSecururityFinancialInstrument(List<string>? isinList);
        Task<IsinResponse?> GetSecururityFinancialInstrument(string? isin);
    }
}