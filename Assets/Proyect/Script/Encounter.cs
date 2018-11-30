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
            dynamicControls[0].interactable = true; //activar atacar
            dynamicControls[1].interactable = true; //activar flee
            UIController.OnEnemyUpdate(this.Enemy);

        }

        public void StartChest()
        {
            
            dynamicControls[3].interactable = true;
        }

        public void StartExit()
        {
            dynamicControls[2].interactable = true;
        }

        public void OpenChest()
        {
            Chest chest = player.Room.Chest;
            if (chest.Trap)
            {
                player.TakeDamage(5);
                Journal.Instance.Log("It was a trap, you took 5 damage");
            }
            else if(chest.Heal)
            {
                Journal.Instance.Log("It was a heal, you heal 5 energy");
                player.TakeDamage(-5);
            }
            else if (chest.Enemy)
            {
                Journal.Instance.Log("Oh no! The chest contained a monster");
                player.Room.Enemy = chest.Enemy;
                player.Room.Chest = null;
                player.Investigate();
            }
            else
            {
                player.Gold += chest.Gold;
                player.AddItem(chest.Item);
                Journal.Instance.Log("You found : "+chest.Item + " and <color= #ffe556ff> "+chest.Gold +"g</color>");
                UIController.OnPlayerInventoryChange(player);
                UIController.OnPlayerStatChange(player);
            }
            player.Room.Chest = null;
            dynamicControls[3].interactable = false;
        }

        public void Attack()
        {
            int playerDamageAmount = (int)(Random.value * (player.Attack - Enemy.Defence));
            int enemyDamageAmount = (int)(Random.value * (Enemy.Attack - player.Defence));
            Journal.Instance.Log("<color=#59ffa1>You attacked, dealing <b>"+playerDamageAmount+"</b> damage!</color>");
            Journal.Instance.Log("<color=#59ffa1>The enemy attacked, dealing <b>" + enemyDamageAmount + "</b> damage!</color>");
            player.TakeDamage(enemyDamageAmount);
            Enemy.TakeDamage(playerDamageAmount);
        }

        public void Flee()
        {
            int enemyDamageAmount = (int)(Random.value * (Enemy.Attack - player.Defence*1.5f));
            player.TakeDamage(enemyDamageAmount);
            UIController.OnEnemyUpdate(null);
            Journal.Instance.Log("<color=#59ffa1>You flee the combat but you take <b>" + enemyDamageAmount + "</b> damage!</color>");
            player.Room.Enemy = null;
            player.Investigate();
            ResetDynamicControls();
        }

        public void ExitFloor()
        {
            StartCoroutine(player.world.GenerateFloor());
            player.Floor += 1;
            Journal.Instance.Log("You found an exit to another floor. Floor: " + player.Floor);
            ResetDynamicControls();
            
        }

        public void Loot()
        {
            player.AddItem(this.Enemy.Inventory[0]);//cogemos el objeto que da el objeto
            player.Gold += this.Enemy.Gold;
            player.Room.Enemy = null;
            player.Room.Empty = true;
            Journal.Instance.Log(string.Format("<color=#59ffa1>You've slain {0}. Searching the carcass, you find a {1} and {2} gold!</color>", Enemy.Description, Enemy.Inventory[0], Enemy.Gold));
            this.Enemy = null;
            player.Investigate();
            UIController.OnEnemyUpdate(this.Enemy);
            ResetDynamicControls();
        }
    }
}
