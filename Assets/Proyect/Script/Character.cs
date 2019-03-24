using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Character : MonoBehaviour
    {
        public int Energy { get; set; }
        public int MaxEnerngy{ get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Gold { get; set; }
        public Vector2 RoomIndex { get; set; }
        public int Agility { get; set; }
        public int Velocity { get; set; }
        public List<Item> Inventory { get; set; } = new List<Item>();

        
        public virtual void TakeDamage(int amount)
        {
            if (Energy-amount >=MaxEnerngy)
                Energy = MaxEnerngy;
            else 
                Energy -= amount;

            if(Energy <= 0)
            {
                Die();
            }

        }

        public virtual void Die()
        {
            Debug.Log("character has died");
        }
    }
    
}

