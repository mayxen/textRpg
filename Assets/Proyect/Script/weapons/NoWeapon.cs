using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class NoWeapon : Weapon
    {
        private void Awake()
        {
            Attack = 0;
            Id = 0;
            Name = "Noweapon";
            Description = "no weapon";
            Type = "none";
        }


    }
}
