using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Consumable : Item
    {
        public virtual void UseConsumable(Player player)
        {
            Debug.Log("Using Consumable");
        }
    }
}
