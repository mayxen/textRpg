﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Raccoon : Enemy
    {
        private void Start()
        {
            Energy = 10;
            Attack = 5;
            Defence = 3;
            Gold = 20;
            Inventory.Add("Bandit Mask");
        }
    }
}
