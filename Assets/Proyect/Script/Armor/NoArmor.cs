using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class NoArmor : Armor
    {
        // Start is called before the first frame update
        void Awake()
        {
            Id = 0;
            Name = "No Armor";
            Description = "no armor";
            Type = "none";
        }

    }
}
