using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Chest
    {
        public string Item { get; set; }
        public int Gold { get; set; }
        public bool Trap { get; set; }
        public bool Heal { get; set; }
        public Enemy Enemy { get; set; }

        
        public Chest()
        {
            switch (Random.Range(0, 7))
            {
                case 0:case 1:
                    Trap = true;
                    break;
                case 2:case 3:
                    Heal = true;
                    break;
                case 4:
                    Enemy = EnemyDataBase.Instance.GetRandomEnemy();
                    break;
                default:
                    int itemToAdd = Random.Range(0,ItemDataBase.Instance.Items.Count);
                    Item = ItemDataBase.Instance.Items[itemToAdd];
                    Gold = Random.Range(50,200);
                    break;
            }
            
        }
    }
}
