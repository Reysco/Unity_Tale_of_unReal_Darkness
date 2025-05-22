using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class HearthDeath : MonoBehaviour
    {
        public Animator anim;

        void Start()
        {
            anim = GetComponent<Animator>();

        }
    }
}
