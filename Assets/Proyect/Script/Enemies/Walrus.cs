using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Walrus : Enemy
    {
        private void Start()
        {
            armor = ArmorDataBase.Instance.GetArmor(0);
            weapon = WeaponDataBase.Instance.GetWeapon(0);
            Description = "cute Walrus";
            Energy = 10;
            MaxEnerngy = 10;
            Attack = 7;
            Defense = 0;
            Gold = 10;
            
            Velocity = 10;
            Agility = 3;
        }
    }
}
