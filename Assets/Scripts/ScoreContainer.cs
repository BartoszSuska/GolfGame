using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.BoarShroom.Golf
{
    public class ScoreContainer : MonoBehaviour
    {
        public int score;
        PlayerMovement player;

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if(GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            }
        }

        void Update()
        {
            if(player)
            {
                score = player.numberOfHits;
            }
        }
    }
}
