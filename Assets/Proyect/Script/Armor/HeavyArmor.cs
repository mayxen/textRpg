using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class HeavyArmor : Armor
    {
        // Start is called before the first frame update
        void Awake()
        {
            Id = 0;
            Name = "Heavy Armor";
            Description = "Its a heavy armor";
            Type = "heavy armor";
        }

    }
}
