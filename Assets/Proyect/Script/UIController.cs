using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextRPG
{
    public class UIController : MonoBehaviour {
        [SerializeField]
        public Text playerStatText,enemyStatsText,playerInventoryText;
        [SerializeField]
        Image updateStat;
        
        public delegate void OnPlayerUpdateHandler(Player player);
        public static OnPlayerUpdateHandler OnPlayerStatChange;
        public static OnPlayerUpdateHandler OnPlayerInventoryChange;
        public static OnPlayerUpdateHandler OnPlayerUpdateActivate;
        public static OnPlayerUpdateHandler OnPlayerUpdateDesactivate;

        public delegate void OnEnemyUpdateHandler(Enemy enemy);
        public static OnEnemyUpdateHandler OnEnemyUpdate;

        public delegate void OnPlayerUpdateStatHandler();
        public static UIController instance = null;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(transform.gameObject);
            }
                
            DontDestroyOnLoad(transform.gameObject);
            GetObjectsNeeded();


        }
        public void GetObjectsNeeded()
        {
            playerStatText = GameObject.Find("PlayerText").GetComponent<Text>();
            enemyStatsText = GameObject.Find("EnemyText").GetComponent<Text>();
            playerInventoryText = GameObject.Find("Inventory").GetComponent<Text>();
            if(GameObject.Find("UpdateStatButtons"))
                updateStat = GameObject.Find("UpdateStatButtons").GetComponent<Image>();
            if(updateStat != null)
                updateStat.gameObject.SetActive(false);
        }
        void Start()
        {
            OnPlayerStatChange += UpdatePlayerStats;
            OnPlayerInventoryChange += UpdatePlayerInventory;
            OnEnemyUpdate += UpdateEnemyStats;
            OnPlayerUpdateActivate += ActivateUpdateStat;
            OnPlayerUpdateDesactivate += DesactivateUpdateStat;
        }

        public void UpdatePlayerStats(Player player)
        {
            
            playerStatText.text = string.Format("Player: {0} energy, {1} attack,{2} Defense, {3} gold, Room: {4}",player.Energy,player.Attack,player.Defense,player.Gold,player.Room.RoomIndex);
        }

        public void UpdatePlayerInventory(Player player)
        {
            playerInventoryText.text = "En desarrollo crack ";
            //foreach (string item in player.Inventory)
            //{
            //    playerInventoryText.text += item+"/ ";
            //}
            
        }


        public void UpdateEnemyStats(Enemy enemy)
        {
            if (enemy != null)
                enemyStatsText.text = string.Format("{3}:\n{0} energy, {1} attack,{2} Defense", enemy.Energy, enemy.Attack, enemy.Defense, enemy.Description);
            else
                enemyStatsText.text = "";
        }

        public void ActivateUpdateStat(Player player)
        {
            if (player.Gold - player.StatCost >= 0)
            {
                updateStat.gameObject.SetActive(true);

                updateStat.GetComponentsInChildren<Text>()[0].text = string.Format("Increase 10 your maximun energy and restore 5 energy\n cost {0}",player.StatCost);
                updateStat.GetComponentsInChildren<Text>()[1].text = string.Format("Increase 1 your attack\n cost {0}", player.StatCost);
                updateStat.GetComponentsInChildren<Text>()[2].text = string.Format("Increase 1 your defense\n cost {0}", player.StatCost);


            }
            
        }

        public void DesactivateUpdateStat(Player player)
        {
            updateStat.gameObject.SetActive(false);
        }
    }
}
