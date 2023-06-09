﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

//Модель для работы с монетами
namespace test_task_web.Models
{
    public class Coin
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int CoinID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string SNameCoin { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string SNameNumberCoin { get; set; }
        public int iCountCoin { get; set; }
        public bool BDontCoin { get; set; }
    }
}