using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Whale : Enemy
    {
        private void Start()
        {
            armor = ArmorDataBase.Instance.GetArmor(0);
            weapon = WeaponDataBase.Instance.GetWeapon(0);
            Description = "BIG Whale";
            Energy = 20;
            MaxEnerngy = 20;
            Attack = 3;
            Defense = 3;
            Gold = 10;
            
            Velocity = 10;
            Agility = 3;
        }
    }
}
