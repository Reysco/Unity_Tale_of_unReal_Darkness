using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bardent
{
    public class MovingPlataform : MonoBehaviour
    {
        public float speed;
        Vector3 targetPos;

        public GameObject ways;
        public Transform[] wayPoints;
        int pointIndex;
        int pointCount;
        int direction = 1;

        public float waitDuration;

        public bool isActivated;

        private void Awake()
        {
            wayPoints = new Transform[ways.transform.childCount];
            for(int i = 0; i< ways.gameObject.transform.childCount; i++)
            {
                wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
            }
        }

        private void Start()
        {
            pointIndex = 1;
            pointCount = wayPoints.Length;
            targetPos = wayPoints[1].transform.position;    
            
        }

        private void Update()
        {

            if(isActivated) //só ira se mexer se essa condição estiver ativa
            {
                if (Vector2.Distance(transform.position, targetPos) < 0.05f)
                {
                    NextPoint();
                }

                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
        }

        void NextPoint()
        {
            transform.position = targetPos;

            if(pointIndex == pointCount - 1)
            {
                direction = -1;
            }

            if(pointIndex == 0)
            {
                direction = 1;
            }

            pointIndex += direction;
            targetPos = wayPoints[pointIndex].transform.position;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
            {
                collision.transform.parent = this.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
            {
                collision.transform.parent = null;
            }
        }
    }
}
