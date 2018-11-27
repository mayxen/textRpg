using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Player : Character{
        public int Floor { get; set; }
        public Room Room { get; set; }
        [SerializeField] World world;

                                           // Use this for initialization
        void Start () {
            Floor = 0;
            Energy = 30;
            Attack = 10;
            Defence = 5;
            Gold = 0;
            Inventory = new List<string>();
            RoomIndex = new Vector2(2,2);
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            this.Room.Empty= true;
        }

        public void Move(int direction)
        {
            if (this.Room.Enemy)
            {
                return;
            }

            if (direction == 0 && RoomIndex.y > 0)
            {
                Journal.instance.Log("You move north");
                RoomIndex -= Vector2.up;
            }
            else if (direction== 1 && RoomIndex.y < (world.Dungeon.GetLength(0) - 1))
            {
                Journal.instance.Log("You move East");
                RoomIndex += Vector2.right;
            }
            else if (direction == 2 && RoomIndex.y < (world.Dungeon.GetLength(1)-1))//cogo el valor 2 de la logitud del Dungeon
            {
                Journal.instance.Log("You move south");
                RoomIndex -= Vector2.down;
            }
            else if(direction == 3 && RoomIndex.x > 0)
            {
                Journal.instance.Log("You move West");
                RoomIndex += Vector2.left;
            }
            if(this.Room.RoomIndex != RoomIndex)
                Investigate();
        }

        public void Investigate()
        {
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            if (this.Room.Empty)
            {
                Journal.instance.Log("You find yourself in an empty room");
            }
            else if (this.Room.Chest!= null)
            {
                Journal.instance.Log("You've found a chest! What would you want to do?");
            }
            else if (this.Room.Enemy!= null)
            {
                Journal.instance.Log("You are jumped by a "+Room.Enemy.Description + " What would you want to do?");
            }
            else if (this.Room.Exit)
            {
                Journal.instance.Log("You've found the exit! What would you want to do?");
            }
        }

        public void AddItem(string item)
        {
            Journal.instance.Log("You were given item: "+item);
            Inventory.Add(item);
        }

        public override void TakeDamage(int amount)
        {
            Debug.Log("Player takeDamage.");
            base.TakeDamage(amount);
        }

        public override void Die()
        {
            Debug.Log("Player Die.");
            base.Die();
        }

    }
}