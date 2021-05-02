using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.BoarShroom.Golf
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] Vector3[] waypoints;
        [SerializeField] float speed;
        [SerializeField] float waitTime;
        [SerializeField] int waypointIndex;
        bool move;

        void Start()
        {
            move = true;
        }

        void Update()
        {
            if(move)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex], speed * Time.deltaTime);

                if (transform.position == waypoints[waypointIndex])
                {
                    StartCoroutine(WaitOnTarget());
                }
            }
        }

        IEnumerator WaitOnTarget()
        {
            move = false;
            yield return new WaitForSeconds(waitTime);
            waypointIndex++;

            if (waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }

            move = true;
        }

        void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.tag == "Player")
            {
                col.transform.parent = transform;
                Debug.Log("{}");
            }
        }

        void OnTriggerExit(Collider col)
        {
            if(col.gameObject.tag == "Player")
            {
                col.transform.parent = null;
            }
        }
    }
}
