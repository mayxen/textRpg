using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextRPG
{
    public class Journal : MonoBehaviour {

        public static Journal instance { get; set; }

        [SerializeField]
        Text logText;

        void Awake()
        {
            if (instance != null && instance != this)
                Destroy(this.gameObject);
            else
                instance = this;
        }

        public void Log(string text)
        {
            logText.text += "\n" + text;
        }

    }
}