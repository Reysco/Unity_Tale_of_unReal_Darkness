using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class DestroyParry : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            Destroy(gameObject);
        }
    }
}
