using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextRPG
{
    public class Journal : MonoBehaviour {

        public static Journal Instance { get; internal set; }
        

        [SerializeField]
        Text logText;

        void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this.gameObject);
            else
                Instance = this;
        }

        public void Log(string text)
        {
            logText.text += "\n" + text;
        }

    }
}