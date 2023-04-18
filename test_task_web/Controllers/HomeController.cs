using test_task_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test_task_web.Concrete;
using test_task_web.Abstract;

namespace test_task_web.Controllers
{
    public class HomeController : Controller
    {

        private IProductRepository repository;
        public HomeController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }
        
        public void DefaultViewBag()
        {
            ViewBag.BDontOne = false;
            ViewBag.BDontTwo = false;
            ViewBag.BDontFive = false;
            ViewBag.BDontTen = false;
            foreach (var c in repository.Coins)
            {
                if ((c.SNameCoin == "One") & (c.BDontCoin)) ViewBag.BDontOne = true;

                if ((c.SNameCoin == "Two") & (c.BDontCoin)) ViewBag.BDontTwo = true;

                if ((c.SNameCoin == "Five") & (c.BDontCoin)) ViewBag.BDontFive = true;

                if ((c.SNameCoin == "Ten") & (c.BDontCoin)) ViewBag.BDontTen = true;
            }
        }
        private Dictionary<int, int> CalculateChange(int Money)
        {
            Dictionary<int, int> Dic = new Dictionary<int, int>();
            int[] FaceValues = { 10, 5, 2, 1 };
            foreach (int item in FaceValues)
            {
                if (Money / item == 0) continue;
                Dic.Add(item, Money / item);
                Money %= item;
                if (Money == 0) break;
            }
            return Dic;
        }
       
        private void SaveCoinInBase(string NameNumberCoin)
        {
            Coin coin = repository.Coins.FirstOrDefault(d => d.SNameNumberCoin == NameNumberCoin);
            coin.iCountCoin ++;
            repository.SaveCoin(coin);
        }
        private void SaveCoinInBase(string NameCoin,int ValueCountCoin)
        {
            Coin coin = repository.Coins.FirstOrDefault(d => d.SNameCoin == NameCoin);
            coin.iCountCoin += ValueCountCoin;
            repository.SaveCoin(coin);
        }
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.SumMoney = int.Parse("0");
            ViewBag.RestOfMoney = 0;
            DefaultViewBag();
            if ( Request.QueryString["id"] == "qwerty")
                return Redirect(Url.Action("Index", "Admin")); 
            else return View(repository.Drinks);
        }
        [HttpPost]
        public ActionResult Index(string SumMoney, string clickonbutton,string clickbuttoncoin,string buttonrestofmoney)
        {
            ViewBag.Title = buttonrestofmoney;
            
            int iSumInController = 0;
            int.TryParse(SumMoney, out iSumInController);
  
            ViewBag.RestOfMoney = 0;
            ViewBag.SumMoney = int.Parse(SumMoney);
            DefaultViewBag();
            if (!string.IsNullOrEmpty(clickbuttoncoin)) SaveCoinInBase(clickbuttoncoin.Split(' ')[0]);
            if (!string.IsNullOrEmpty(clickonbutton))
            {
                Drink Drink = repository.Drinks.FirstOrDefault(d => d.Name == clickonbutton);
                Drink.iCount--;
                repository.SaveProduct(Drink);
                clickonbutton = "";
                clickbuttoncoin = "";
                ViewBag.RestOfMoney = iSumInController - (int)Drink.Price;
                
                ViewBag.SumMoney = ViewBag.RestOfMoney;
            }
            if (!string.IsNullOrEmpty(buttonrestofmoney))
            {
                foreach (KeyValuePair<int, int> item in CalculateChange((int.Parse(buttonrestofmoney.Split(' ')[0]))))
                {
                    switch (item.Key)
                    {
                        case 1:
                            SaveCoinInBase("One", -item.Value);
                            break;
                        case 2:
                            SaveCoinInBase("Two", -item.Value);
                            break;
                        case 5:
                            SaveCoinInBase("Five", -item.Value);
                            break;
                        case 10:
                            SaveCoinInBase("Ten", -item.Value);
                            break;
                    }
                }
                ViewBag.SumMoney = 0;
            }
            
            return View(repository.Drinks);
        }
        public FileContentResult GetImage(int productId)
        {
            Drink prod = repository.Drinks.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}