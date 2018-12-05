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
        public int  Turn{ get; set; }
        public delegate void OnEnemyDieHandler();
        public static OnEnemyDieHandler OnEnemyDie;


        void Start()
        {
            OnEnemyDie += Loot; //estamos añadiendo metodos al evento   
            Turn = 0;
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
            if (chest.Trap)
            {
                player.TakeDamage(5);
                Journal.Instance.Log(string.Format("<color=#59ffa1>It was a trap, you took 5 damage</color>"));
            }
            else if(chest.Heal)
            {
                Journal.Instance.Log(string.Format("<color=#59ffa1>It was a heal, you heal 5 energy</color>"));
                player.TakeDamage(-5);
            }
            else if (chest.Enemy)
            {
                Journal.Instance.Log(string.Format("<color=#59ffa1>Oh no! The chest contained a monster</color>"));
                player.Room.Enemy = chest.Enemy;
                player.Room.Chest = null;
                player.Investigate();
            }
            else
            {
                player.Gold += chest.Gold;
                player.AddItem(chest.Item);
                Journal.Instance.Log(string.Format("<color= #ffe556ff>You found : {0}and  {1}g", chest.Item,chest.Gold));
                UIController.OnPlayerInventoryChange(player);
                UIController.OnPlayerStatChange(player);
            }
            player.UpdateStats();
            player.Room.Chest = null;
            player.Room.Empty = true;
            dynamicControls[3].interactable = false;
        }

        public void Attack()
        {
            int playerDamageAmount =Random.Range((player.Attack - Enemy.Defence)-10 > 0 ? (player.Attack - Enemy.Defence) - 10 : 0, (player.Attack - Enemy.Defence)<0 ? 0 : player.Attack - Enemy.Defence+1);
            int enemyDamageAmount =Random.Range((Enemy.Attack - player.Defence) - 10 > 0 ? (Enemy.Attack - player.Defence) - 10 : 0, (Enemy.Attack - player.Defence) < 0 ? 0 : Enemy.Attack - player.Defence+1);
            Journal.Instance.Log(string.Format("<color=#59ffa1>You attacked, dealing <b>{0}</b> damage!</color>", playerDamageAmount));
            Journal.Instance.Log(string.Format("<color=#59ffa1>The enemy attacked, dealing <b>{0}</b> damage!</color>", enemyDamageAmount));
            player.TakeDamage(enemyDamageAmount);
            Enemy.TakeDamage(playerDamageAmount);
        }

        public void Flee()
        {
            Journal.Instance.Empty();
            int enemyDamageAmount = Random.Range(0,((Enemy.Attack - player.Defence) < 0 ? 0 : Enemy.Attack - player.Defence)+1);
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
            player.Floor += 1;
            Journal.Instance.Log(string.Format("<color=#baba4a>You found an exit to another floor. Floor:{0} Now the enemies are stronger</color>", player.Floor));
            EnemyDataBase.Instance.UpdateEnemies();
            ResetDynamicControls();
            Map.Instance.ResetColors();
            Map.Instance.ChangeColor(player.RoomIndex, Color.red);
            player.Room.Exit = false;
            
        }

        public void Loot()
        {
            Journal.Instance.Empty();
            player.AddItem(this.Enemy.Inventory[0]);//cogemos el objeto que da el objeto
            player.Gold += this.Enemy.Gold;
            player.UpdateStats();
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
