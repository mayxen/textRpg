using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextRPG
{
    public class Map : MonoBehaviour {
        public Image[,] RoomsMap { get; set; } = new Image[10,10];
        [SerializeField] Image imagePrefab;
        public static Map Instance { get; set; }
        // Use this for initialization

        void Awake () {

            if (Instance != null && Instance != this)
                Destroy(this.gameObject);
            else
                Instance = this;

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Image imageInstance = Instantiate(imagePrefab,this.transform);
                    imageInstance.color = Color.white;
                    RoomsMap[x, y] = imageInstance;
                }
            }
            
	    }
	
	    public void ChangeColor(Vector2 RoomIndex,Color color)
        {
            RoomsMap[(int)RoomIndex.y, (int)RoomIndex.x].color = color;
        }

        public void ResetColors()
        {
            foreach (Image room in RoomsMap)
            {
                room.color = Color.white;
            }
        }
    }
}