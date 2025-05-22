using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class DontDestroyCheckPoint : MonoBehaviour
    {
        private static DontDestroyCheckPoint instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
