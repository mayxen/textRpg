using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Raccoon : Enemy
    {
        private void Start()
        {
            armor = ArmorDataBase.Instance.GetArmor(0);
            weapon = WeaponDataBase.Instance.GetWeapon(0);
            MaxEnerngy = 5;
            Description = "Evil Raccoon";
            Energy = 5;
            Attack = 2;
            Defense = 4;
            Gold = 10;
            
            Velocity = 10;
            Agility = 3;
        }
    }
}
