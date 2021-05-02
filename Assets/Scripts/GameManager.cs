using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.BoarShroom.Golf
{
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
