using System.Linq;
using test_task_web.Models;
// Интерфейс для удобства создания последующих представлений
namespace test_task_web.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Drink> Drinks { get; }
        IQueryable<Coin> Coins { get; }
        void SaveProduct(Drink drink);
        void SaveCoin(Coin coin);
        Drink DeleteDrink(int productID);
    }
}