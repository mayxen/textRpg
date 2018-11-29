using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Enemy : Character
    {

        public string Description { get; set; }
        public override void TakeDamage(int amount)
        {
            base.TakeDamage(amount);
            Debug.Log("This also happens but only on enemy! not other characters");
            UIController.OnEnemyUpdate(this);
        }

        public override void Die()
        {
            Encounter.OnEnemyDie();
            Energy = MaxEnerngy;
            Debug.Log("Character died, Was enemy");
        }
    }
}