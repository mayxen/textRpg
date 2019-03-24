using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Katana : Weapon
    {
        private void Awake()
        {
            Attack = 5;
            Id = 0;
            Name = "Katana";
            Description = "Its a katana from the guetto";
            Type = "steel";
        }


    }
}
