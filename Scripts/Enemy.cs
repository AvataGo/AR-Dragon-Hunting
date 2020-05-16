using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyMoveTime = 1.0f;
    public float enemySpeed = 1.5f;
    public float enemyRAngles = 30f;
    public float enemyLAngles = -30f;

    private Animator enemyAnim;
    
    void Start()
        {
            enemyAnim = GetComponent<Animator>();
            StartCoroutine("Move");
        }

        void Update()
        {
            transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);

            if(transform.localPosition.x > 70 || transform.localPosition.x < -70 || 
              transform.localPosition.z > 90 || transform.localPosition.z < -2)
            {
                Destroy(gameObject);
            }            
        }

        public void GetHit()
        {
            Debug.Log("Start Get Hit");
            enemySpeed = 0.0f;
            enemyAnim.SetTrigger("GetHit");
        }

        IEnumerator Move()
        {
            while(true)
            {
                yield return new WaitForSeconds(enemyMoveTime);                

                float randomY = Random.Range(enemyLAngles,enemyRAngles);
                if(transform.localPosition.x > 65 || transform.localPosition.x < -65 || 
                    transform.localPosition.z > 80 || transform.localPosition.z < 5)
                {
                    transform.eulerAngles += new Vector3(0.0f, 180.0f, 0.0f);
                }
                else
                {
                    transform.eulerAngles += new Vector3(0.0f, randomY, 0.0f);
                }
            }
        }
}
