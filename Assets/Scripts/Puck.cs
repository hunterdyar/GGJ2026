using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Puck : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 45);
        }
    }
}