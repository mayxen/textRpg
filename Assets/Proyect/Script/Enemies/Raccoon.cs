using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Raccoon : Enemy
    {
        private void Start()
        {
            MaxEnerngy = 5;
            Description = "Evil Raccoon";
            Energy = 5;
            Attack = 2;
            Defense = 4;
            Gold = 10;
            Inventory.Add("R");
        }
    }
}
