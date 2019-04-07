using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TextRPG
{

    public class ArmorDataBase : MonoBehaviour
    {
        public static ArmorDataBase Instance { get; set; }
        public List<Armor> Armors { get; private set; } = new List<Armor>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this.gameObject);

            else
                Instance = this;
            foreach (Armor armor in GetComponents<Armor>())
            {
                Armors.Add(armor);
                Debug.Log(armor.Name);

            }

        }

        public Armor GetArmor(int id)
        {
            return Armors.Find(i=> i.Id == id);
        }

        public Armor GetArmor(string name)
        {
            return Armors.Find(i => i.Name == name);
        }
    }
}
