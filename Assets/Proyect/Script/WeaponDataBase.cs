using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{

    public class WeaponDataBase : MonoBehaviour
    {
        public static WeaponDataBase Instance { get; set; }
        public List<Weapon> Weapons{ get; private set; } = new List<Weapon>();
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this.gameObject);

            else
                Instance = this;
            foreach (Weapon weapon in GetComponents<Weapon>())
            {
                Weapons.Add(weapon);
                Debug.Log(weapon.Name);
            }
        }

        public Weapon GetWeapon(int id)
        {
            return Weapons.Find(i => i.Id == id);
        }

        public Weapon GetWeapon(string name)
        {
            return Weapons.Find(i => i.Name == name);
        }
    }
}
