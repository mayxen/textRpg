using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TextRPG
{
    public class Player : Character{
        
        public static int Floor { get; set; }//tener cuidado porque si hay más de un jugador se peta unity
        public Room Room { get; set; }
        public int StatCost { get; set; }
        public int Turn { get; set; }
        public World world;
        [SerializeField] Encounter enconter;

        private void Awake()
        {
            Floor = 1;
        }
        void Start () {
            Turn = 0;
            MaxEnerngy = 50;
            Energy = 50;
            Attack = 5;
            Defense = 0;
            Gold = 50;
            Velocity = 10;
            Agility = 3;
            StatCost = 10;
            Inventory = new List<Item>();
            RoomIndex = new Vector2(0,0);
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            this.Room.Empty= true;
            enconter.ResetDynamicControls();
            Map.Instance.ChangeColor(RoomIndex,Color.red);

        }

        public void UpdateStats(int Stat)
        {   
            switch (Stat)
            {
                case 0:
                    MaxEnerngy += 10;
                    TakeDamage(-15);
                    Journal.Instance.Log("Now you feel with more energy and restored 5 energy");
                    break;
                case 1:
                    Attack++;
                    Journal.Instance.Log("Now you are stronger");
                    break;
                case 2:
                    Journal.Instance.Log("You learn how to defend yourself better");
                    Defense++;
                    break;       
            }
            Gold -= StatCost;
            StatCost += 10;
            UIController.OnPlayerUpdateDesactivate(this);
        }

        void LateUpdate()
        {
            UIController.OnPlayerStatChange(this);
            UIController.OnPlayerInventoryChange(this);
        }

        public void Move(int direction)
        {
            if (Room.Enemy != null && Room.Empty==false)
            {
                return;
            }
            Turn++;
            if (Turn % 10 == 0)
                Journal.Instance.Empty();

            if (direction == 0 && RoomIndex.y > 0)
            {
                Map.Instance.ChangeColor(RoomIndex, (this.Room.Exit == true)?Color.green: (this.Room.Empty == true)? Color.magenta : Color.yellow );
                Journal.Instance.Log("You move north");
                RoomIndex -= Vector2.up;
                Map.Instance.ChangeColor(RoomIndex,Color.red);
            }
            else if (direction== 1 && RoomIndex.x < (world.Dungeon.GetLength(0) - 1))
            {
                Map.Instance.ChangeColor(RoomIndex, (this.Room.Exit == true) ? Color.green : (this.Room.Empty == true) ? Color.magenta : Color.yellow);
                Journal.Instance.Log("You move East");
                RoomIndex += Vector2.right;
                Map.Instance.ChangeColor(RoomIndex, Color.red);
            }
            else if (direction == 2 && RoomIndex.y < (world.Dungeon.GetLength(1)-1))//cogo el valor 2 de la logitud del Dungeon
            {
                Map.Instance.ChangeColor(RoomIndex, (this.Room.Exit == true) ? Color.green : (this.Room.Empty == true) ? Color.magenta : Color.yellow);
                Journal.Instance.Log("You move south");
                RoomIndex -= Vector2.down;
                Map.Instance.ChangeColor(RoomIndex, Color.red);
            }
            else if(direction == 3 && RoomIndex.x > 0)
            {
                Map.Instance.ChangeColor(RoomIndex, (this.Room.Exit == true) ? Color.green : (this.Room.Empty == true) ? Color.magenta : Color.yellow);
                Journal.Instance.Log("You move West");
                RoomIndex += Vector2.left;
                Map.Instance.ChangeColor(RoomIndex, Color.red);
            }
            if(this.Room.RoomIndex != RoomIndex || RoomIndex.y > (world.Dungeon.GetLength(0) - 1) || RoomIndex.y > (world.Dungeon.GetLength(1) - 1))
                Investigate();
            
        }

        public void Investigate()
        {
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            if (this.Room.Exit)
            {
                enconter.StartExit();
                Journal.Instance.Log("You've found the exit! What would you want to do?");
                
                
            }
            else if (this.Room.Empty)
            {
                Journal.Instance.Log("You find yourself in an empty room");
                enconter.ResetDynamicControls();
                
                
            }
            else if (this.Room.Chest != null)
            {
                enconter.StartChest();
                Journal.Instance.Log("You've found a chest! What would you want to do?");
                
                
            }
            else if (this.Room.Enemy != null)
            {
                Journal.Instance.Log("You are jumped by a " + Room.Enemy.Description + " What would you want to do?");
                enconter.StartCombat();
            }
        }

        public void AddItem(Item item)
        {
            
            //Journal.Instance.Log("You were given item: "+item);
            //Inventory.Add(item);
            UIController.OnPlayerInventoryChange(this);
        }

        public override void TakeDamage(int amount)
        {
            //me falta limitar la vida máxima del jugador
            base.TakeDamage(amount);
            UIController.OnPlayerStatChange(this);
        }

        public override void Die()
        {
            base.Die();
            UIController.instance.SetScoreToDatabase();
            SceneManager.LoadScene("GameOver");
        }

    }
}