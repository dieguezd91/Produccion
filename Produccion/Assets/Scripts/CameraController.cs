using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
        public Transform playerTransform;
        public Transform combatTransform;
        public bool inCombat;

        void Update()
        {
            if (inCombat)
            {
                transform.position = combatTransform.position;
                transform.rotation = combatTransform.rotation;
            }
            else
            {
                transform.position = playerTransform.position;
                transform.rotation = playerTransform.rotation;
            }
        }
}
