using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextRPG
{
    
    public class Encounter : MonoBehaviour
    {
        public Enemy Enemy { get; set; }
        [SerializeField]
        public Player player;
        [SerializeField]
        Button[] dynamicControls;
        
        public delegate void OnEnemyDieHandler();
        public static OnEnemyDieHandler OnEnemyDie;


        void Start()
        {
            OnEnemyDie += Loot; //estamos añadiendo metodos al evento   

        }

        public void ResetDynamicControls()
        {
            foreach (Button button in dynamicControls)
            {
                button.interactable = false;
            }
        }

        public void StartCombat()
        {
            
            this.Enemy = player.Room.Enemy;
            ResetDynamicControls();
            dynamicControls[0].interactable = true; //activar atacar
            dynamicControls[1].interactable = true; //activar flee
            UIController.OnEnemyUpdate(this.Enemy);

        }

        public void StartChest()
        {
            ResetDynamicControls();
            dynamicControls[3].interactable = true;
        }

        public void StartExit()
        {
            ResetDynamicControls();
            dynamicControls[2].interactable = true;
        }

        public void OpenChest()
        {
            Chest chest = player.Room.Chest;
            player.Room.Empty =true;
            if (chest.Trap)
            {
                player.TakeDamage(5*Player.Floor);
                Journal.Instance.Log(string.Format("<color=#59ffa1>It was a trap, you took {0} damage</color>", 5 * Player.Floor));
            }
            else if(chest.Heal)
            {
                Journal.Instance.Log(string.Format("<color=#59ffa1>It was a heal, you heal {0} damage</color>", 5 * Player.Floor));
                player.TakeDamage(-5*Player.Floor);
            }
            else if (chest.Enemy)
            {
                player.Room.Enemy = chest.Enemy;
                Journal.Instance.Log(string.Format("<color=#59ffa1>Oh no! The chest contained a {0}</color>", chest.Enemy.Description));
                player.Room.Chest = null;
                player.Room.Empty = false;
                
                player.Investigate();
            }
            else
            {
                
                player.Gold += chest.Gold;
                player.AddItem(chest.Item);
                Journal.Instance.Log(string.Format("<color= #ffe556ff>You found : {0} and {1} Gold</color>", chest.Item,chest.Gold));
                UIController.OnPlayerInventoryChange(player);
                UIController.OnPlayerStatChange(player);
            }
            
            player.Room.Chest = null;
            UIController.OnPlayerUpdateActivate(player);
            dynamicControls[3].interactable = false;
        }

        public void Attack()
        {
            int playerDamageAmount = CalculateDamage(player.Attack, Enemy.Defense);
            int enemyDamageAmount = CalculateDamage(Enemy.Attack,player.Defense);
            Journal.Instance.Log(string.Format("You attacked, dealing <color=#c62525> <b>{0}</b></color> damage!", playerDamageAmount < 0 ? 0 : playerDamageAmount));
            
            if (Enemy.Energy-playerDamageAmount > 0)
            {
                Journal.Instance.Log(string.Format("The enemy attacked, dealing <color=#c62525><b>{0}</b></color> damage!", enemyDamageAmount < 0 ? 0 : enemyDamageAmount));
                player.TakeDamage(enemyDamageAmount < 0 ? 0 : enemyDamageAmount);
            }
            Enemy.TakeDamage(playerDamageAmount);
        }

        int CalculateDamage(int attack, int defense)
        {
            return (int)(attack - (5*Player.Floor* ((defense * 100)/((5*Player.Floor)*100f))));
        }

        public void Flee()
        {
            Journal.Instance.Empty();
            int enemyDamageAmount = Random.Range(0,((Enemy.Attack - player.Defense) < 0 ? 0 : Enemy.Attack - player.Defense)+1);
            player.TakeDamage(enemyDamageAmount);
            UIController.OnEnemyUpdate(null);
            Journal.Instance.Log(string.Format("<color=#59ffa1>You flee the combat but you take <b>{0}</b> damage!</color>", enemyDamageAmount));
            player.Room.Enemy = null;
            player.Room.Empty = true;
            player.Investigate();
            ResetDynamicControls();
            
        }

        public void ExitFloor()
        {
            Journal.Instance.Empty();
            StartCoroutine(player.world.GenerateFloor());
            Player.Floor += 1;
            Journal.Instance.Log(string.Format("<color=#baba4a>You found an exit to another floor. Floor:{0} Now the enemies are stronger</color>", Player.Floor));
            EnemyDataBase.Instance.UpdateEnemies();
            ResetDynamicControls();
            Map.Instance.ResetColors();
            Map.Instance.ChangeColor(player.RoomIndex, Color.red);
            player.Room.Exit = false;
            player.Room.Empty = true;
            player.TakeDamage(-20);
            
        }

        public void Loot()
        {
            Journal.Instance.Empty();
            player.AddItem(this.Enemy.Inventory[0]);//cogemos el objeto que da el objeto
            player.Gold += this.Enemy.Gold;
            UIController.OnPlayerUpdateActivate(player);
            player.Room.Enemy = null;
            player.Room.Empty = true;
            Journal.Instance.Log(string.Format("<color=#59ffa1> You've slain {0}. Searching the carcass, you find a {1} and {2} gold! </color>", Enemy.Description, Enemy.Inventory[0], Enemy.Gold));
            this.Enemy = null;
            player.Investigate();
            UIController.OnEnemyUpdate(this.Enemy);
            ResetDynamicControls();
        }
    }
}
