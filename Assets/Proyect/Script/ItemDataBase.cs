using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class ItemDataBase : MonoBehaviour
    {
        public List<Consumable> Consumables { get; set; } = new List<Consumable>();
        public static ItemDataBase Instance{ get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this) //si no existe o si existe y es otro que no sea el que ya se creó
                Destroy(this.gameObject);
            else
                Instance = this;

            foreach (Consumable consumable in GetComponents<Consumable>())
            {
                Consumables.Add(consumable);
                Debug.Log(consumable.Name);
            }
        }

        public Consumable GetConsumable(int id)
        {
            return Consumables.Find(i => i.Id == id);
        }

        public Consumable GetConsumable(string name)
        {
            return Consumables.Find(i => i.Name == name);
        }
    }
}


