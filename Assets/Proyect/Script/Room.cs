using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TextRPG { 
    public class Room {
        public Chest Chest { get; set; }
        public Enemy Enemy { get; set; }
        public bool Exit { get; set; }
        public bool Empty { get; set; }
        public Vector2 RoomIndex { get; set; }

        public Room(Chest chest, Enemy enemy,bool empty, bool exit)
        {
            this.Chest = chest;
            this.Enemy = enemy;
            this.Exit = exit;
            this.Empty = empty;

            if (Enemy != null)
            {
                this.Enemy.RoomIndex = RoomIndex;
            }
        }

        public Room()
        {
            int roll = Random.Range(0,30);
            if (roll > 0 && roll < 6)
            {
                Enemy = EnemyDataBase.Instance.GetRandomEnemy();
                Enemy.RoomIndex = RoomIndex;
            }
            else if (roll > 15 && roll < 20)
            {
                Chest = new Chest();
            }
            else 
            {
                Empty = true;
            }
        }
    }
}
