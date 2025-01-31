using test_binance_api.Models;
using test_binance_api.Repository.GenericRepository;

namespace test_binance_api.Repository.TransactionRepository
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
    }
}
