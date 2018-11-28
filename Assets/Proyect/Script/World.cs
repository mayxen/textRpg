using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class World : MonoBehaviour
    {
        
        public Room[,] Dungeon { get; set; }
        public Vector2 Grid;

        void Awake()
        {
            Dungeon = new Room[(int)Grid.x, (int)Grid.y];
            StartCoroutine(GenerateFloor());
        }

        public IEnumerator GenerateFloor() //es para hacer el metodo pero en varios pasos para que no pete los fps
        {
            Debug.Log("Generating floor");
            for (int x = 0;x < Grid.x;x++)
            {
                for (int y = 0;y < Grid.y;y++)
                {
                    Dungeon[x, y] = new Room//se puede inicializar variables cuando se crea un array
                    {
                        RoomIndex = new Vector2(x, y)
                    };
                }
            }
            yield return new WaitForSeconds(1.4f); //espera 5 segundos antes de cargar la salida
            Debug.Log("Now we have a exit");
            Vector2 exitLocation = new Vector2(Random.Range(0,(int)Grid.x), Random.Range(0, (int)Grid.y));
            Dungeon[(int)exitLocation.x, (int)exitLocation.y].Exit = true;
            Dungeon[(int)exitLocation.x, (int)exitLocation.y].Empty = false;
        }

    }
}
