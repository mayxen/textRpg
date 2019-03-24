using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Chest
    {
        public Consumable Item { get; set; }
        public int Gold { get; set; }
        public bool Trap { get; set; }
        public bool Heal { get; set; }
        public Enemy Enemy { get; set; }
        public int MaxGold { get; set; } = 100 * Player.Floor;


        public Chest()
        {
            switch (Random.Range(0, 7))
            {
                case 1:
                    Trap = true;
                    break;
                case 2:case 3:
                    Heal = true;
                    break;
                case 4:
                    Enemy = EnemyDataBase.Instance.GetRandomEnemy();
                    break;
                default:
                    int itemToAdd = Random.Range(0,ItemDataBase.Instance.Consumables.Count);
                    Item = ItemDataBase.Instance.GetConsumable(itemToAdd);
                    Gold = Random.Range(MaxGold-50, MaxGold);
                    break;
            }
            
        }

    }
}
