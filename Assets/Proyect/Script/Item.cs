using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Item : MonoBehaviour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //en objetos de uso será en plan poción o cosas así, pero para armas y armaduras será un tipo en especifico
        public string Type { get; set; }
    }
}

