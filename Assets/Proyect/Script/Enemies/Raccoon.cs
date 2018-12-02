using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Raccoon : Enemy
    {
        private void Start()
        {
            MaxEnerngy = 10;
            Description = "Evil Raccoon";
            Energy = 10;
            Attack = 3;
            Defence = 5;
            Gold = 10;
            Inventory.Add("R");
        }
    }
}
