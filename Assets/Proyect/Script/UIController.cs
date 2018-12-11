using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

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
        //la referencia a la base de datos
        DatabaseReference reference;
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

            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    // Create and hold a reference to your FirebaseApp, i.e.
                    //   app = Firebase.FirebaseApp.DefaultInstance;
                    // where app is a Firebase.FirebaseApp property of your application class.

                    // Set a flag here indicating that Firebase is ready to use by your
                    // application.
                }
                else
                {
                    UnityEngine.Debug.LogError(System.String.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
                FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://climb-dungeon.firebaseio.com/");
                reference = FirebaseDatabase.DefaultInstance.RootReference;
            });
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
        
        public void GetScoreFromDatabase(Text laderboard)
        {
            reference.Child("users").Child("1").GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("no entro porque no me sale de los putos huevos, un saludo ivan");
                }
                else if (task.IsCompleted)
                {
                    
                    DataSnapshot snapshot = task.Result;
                    SetScore.instance.SetText("El piso más alto escalado es: " +snapshot.Value);
                }
            });
      
        }

        public void SetScoreToDatabase()
        {
            reference.Child("users").Child("1").GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log(Player.Floor > (int)snapshot.Value);
                    if ((int)snapshot.Value < Player.Floor)
                        reference.Child("users").Child("1").SetValueAsync(Player.Floor);
                }
            });
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
