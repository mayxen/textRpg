using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Whale : Enemy
    {
        private void Start()
        {
            Description = "BIG Whale";
            Energy = 20;
            MaxEnerngy = 20;
            Attack = 3;
            Defense = 3;
            Gold = 10;
            Inventory.Add("WH");
        }
    }
}
