using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Com.BoarShroom.Golf
{
    public class EndScreen : MonoBehaviour
    {

        ScoreContainer scoreContainer;

        void Awake()
        {
            TMP_Text text = GetComponent<TMP_Text>();
            if (GameObject.FindGameObjectWithTag("ScoreContainer"))
            {
                scoreContainer = GameObject.FindGameObjectWithTag("ScoreContainer").GetComponent<ScoreContainer>();
            }
            text.text = "Congratulations you won in only " + scoreContainer.score + " moves. \nTry to beat this score!";
        }
    }
}
