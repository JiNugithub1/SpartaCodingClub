using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    partial class Item
    {
        public string Name;
        public int Attack;
        public int Defense;
        public string Description;
        public int Price;
        public bool Purchased;
        public bool Equipped;

        public Item(string name, int atk, int def, string desc, int price, bool purchased = false)
        {
            Name = name;
            Attack = atk;
            Defense = def;
            Description = desc;
            Price = price;
            Purchased = purchased;
            Equipped = false;
        }

        public string GetStatText()
        {
            if (Attack > 0) return $"공격력 +{Attack}";
            else if (Defense > 0) return $"방어력 +{Defense}";
            else return "능력치 없음";
        }
    }
}
