using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextRPG
{
    public class SetScore : MonoBehaviour
    {
        public Text laderboard;

        public static SetScore instance = null;
        // Use this for initialization
        void Awake()
        {
            instance = this;
            UIController.instance.GetScoreFromDatabase(laderboard);
        }

        public void SetText(string set)
        {
            laderboard.text = set;
        }

    }
}

