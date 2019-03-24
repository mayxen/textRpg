using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class HealingPotion : Consumable
    {
        public override void UseConsumable(Player player)
        {
            base.UseConsumable(player);
            player.TakeDamage(-10);
        }

        private void Awake()
        {
            Id = 0;
            Name = "healing potion";
            Description = "Its a healing potion which can heal 10 hp";
            Type = "Consumable";
        }
    }
}
