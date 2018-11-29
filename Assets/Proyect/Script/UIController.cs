using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextRPG
{
    public class UIController : MonoBehaviour {
        [SerializeField]
        Text playerStatText,enemyStatsText,playerInventoryText;
        public delegate void OnPlayerUpdateHandler(Player player);
        public static OnPlayerUpdateHandler OnPlayerStatChange;
        public static OnPlayerUpdateHandler OnPlayerInventoryChange;
        public delegate void OnEnemyUpdateHandler(Enemy enemy);
        public static OnEnemyUpdateHandler OnEnemyUpdate;
        private void Start()
        {
            OnPlayerStatChange += UpdatePlayerStats;
            OnPlayerInventoryChange += UpdatePlayerInventory;
            OnEnemyUpdate += UpdateEnemyStats;
            playerInventoryText.text = "Items: ";
        }
        public void UpdatePlayerStats(Player player)
        {
            playerStatText.text = string.Format("Player: {0} energy, {1} attack,{2} defence, {3} gold",player.Energy,player.Attack,player.Defence,player.Gold);
        }

        public void UpdatePlayerInventory(Player player)
        {
            foreach (string item in player.Inventory)
            {
                playerInventoryText.text += item+"/ ";
            }
            
        }
        public void UpdateEnemyStats(Enemy enemy)
        {
            if (enemy)
                enemyStatsText.text = string.Format("{3}: {0} energy, {1} attack,{2} defence", enemy.Energy, enemy.Attack, enemy.Defence, enemy.Description);
            else
                enemyStatsText.text = "";
        }
    }
}
