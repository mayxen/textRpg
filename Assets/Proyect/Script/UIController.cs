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

        void Start()
        {
            OnPlayerStatChange += UpdatePlayerStats;
            OnPlayerInventoryChange += UpdatePlayerInventory;
            OnEnemyUpdate += UpdateEnemyStats;
            
        }

        public void UpdatePlayerStats(Player player)
        {
            playerStatText.text = string.Format("Player: {0} energy, {1} attack,{2} defence, {3} gold, Room: {4}", player.Energy,player.Attack,player.Defence,player.Gold,player.Room.RoomIndex);
        }

        public void UpdatePlayerInventory(Player player)
        {
            playerInventoryText.text = "Items: ";
            foreach (string item in player.Inventory)
            {
                playerInventoryText.text += item+"/ ";
            }
            
        }


        public void UpdateEnemyStats(Enemy enemy)
        {
            if (enemy != null)
                enemyStatsText.text = string.Format("{3}: {0} energy, {1} attack,{2} defence", enemy.Energy, enemy.Attack, enemy.Defence, enemy.Description);
            else
                enemyStatsText.text = "";
        }
    }
}
