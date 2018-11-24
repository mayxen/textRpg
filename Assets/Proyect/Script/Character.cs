using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character: MonoBehaviour
{
    public int Energy{ get; set; }
    public int Attack{ get; set; }
    public int Defence{ get; set; }
    public int Gold { get; set; }
    public Vector2 RoomIndex{ get; set; }
    public List<string> Inventory { get; set; }

}
