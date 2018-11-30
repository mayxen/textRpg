using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Walrus : Enemy
    {
        private void Start()
        {
            Description = "cute Walrus";
            Energy = 15;
            MaxEnerngy = 15;
            Attack = 3;
            Defence = 5;
            Gold = 30;
            Inventory.Add("Tooth");
        }
    }
}
