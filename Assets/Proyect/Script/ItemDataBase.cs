using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class ItemDataBase : MonoBehaviour
    {
        public List<string> Items { get; set; } = new List<string>();
        public static ItemDataBase Instance{ get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this) //si no existe o si existe y es otro que no sea el que ya se creó
                Destroy(this.gameObject);
            else
                Instance = this;

            Items.Add("Emerald Slipper");
            Items.Add("Empty bottle");
            Items.Add("The real nothing");
        }
    }
}


