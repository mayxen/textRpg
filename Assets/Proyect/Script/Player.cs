using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Player : Character{
        public int Floor { get; set; }
        public Room Room { get; set; }
        public World world;
        [SerializeField] Encounter enconter;
                                           // Use this for initialization
        void Start () {
            Floor = 0;
            MaxEnerngy = 30;
            Energy = 30;
            Attack = 10;
            Defence = 2;
            Gold = 0;
            Inventory = new List<string>();
            RoomIndex = new Vector2(2,2);
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            this.Room.Empty= true;
            enconter.ResetDynamicControls();
            UIController.OnPlayerStatChange(this);
            UIController.OnPlayerInventoryChange(this);
        }

        public void Move(int direction)
        {
            if (this.Room.Enemy && !this.Room.Empty)
            {
                return;
            }

            if (direction == 0 && RoomIndex.y > 0)
            {
                Journal.Instance.Log("You move north");
                RoomIndex -= Vector2.up;
            }
            else if (direction== 1 && RoomIndex.y < (world.Dungeon.GetLength(0) - 1))
            {
                Journal.Instance.Log("You move East");
                RoomIndex += Vector2.right;
            }
            else if (direction == 2 && RoomIndex.y < (world.Dungeon.GetLength(1)-1))//cogo el valor 2 de la logitud del Dungeon
            {
                Journal.Instance.Log("You move south");
                RoomIndex -= Vector2.down;
            }
            else if(direction == 3 && RoomIndex.x > 0)
            {
                Journal.Instance.Log("You move West");
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
                Journal.Instance.Log("You find yourself in an empty room");
            }
            else if (this.Room.Chest!= null)
            {
                enconter.StartChest();
                Journal.Instance.Log("You've found a chest! What would you want to do?");
            }
            else if (this.Room.Enemy!= null)
            {
                Journal.Instance.Log("You are jumped by a "+Room.Enemy.Description + " What would you want to do?");
                enconter.StartCombat();
            }
            else if (this.Room.Exit)
            {
                enconter.StartExit();
                Journal.Instance.Log("You've found the exit! What would you want to do?");
            }
        }

        public void AddItem(string item)
        {
            
            Journal.Instance.Log("You were given item: "+item);
            Inventory.Add(item);
            UIController.OnPlayerInventoryChange(this);
        }

        public override void TakeDamage(int amount)
        {
            //me falta limitar la vida máxima del jugador
            base.TakeDamage(amount);
            UIController.OnPlayerStatChange(this);
            Debug.Log("Player takeDamage.");
        }

        public override void Die()
        {
            Debug.Log("Player Die.");
            base.Die();
        }

    }
}