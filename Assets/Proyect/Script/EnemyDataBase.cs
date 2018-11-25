using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TextRPG
{


    public class EnemyDataBase : MonoBehaviour {

        public List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public static EnemyDataBase Instance { get; set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this.gameObject);
            else
                Instance = this;

            foreach (Enemy enemy in GetComponents<Enemy>())
            {
                Enemies.Add(enemy);
            }
        }

        public Enemy GetRandomEnemy()
        {
            return Enemies[Random.Range(0, Enemies.Count)];
        }
    }
}