﻿using System.Collections;
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

        public void UpdateEnemies()
        {
            foreach (Enemy enemy in GetComponents<Enemy>())
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        enemy.Attack++;
                        break;
                    case 1:
                        enemy.Defence++;
                        break;
                    case 2:
                        enemy.Energy += 10;
                        enemy.MaxEnerngy += 10;
                        break;
                }
            }
        }
    }
}