using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
	//private Enemy enemy;
	private void Start () 
	{
		//enemy = FindObjectOfType<Enemy>();
	}

	void OnTriggerEnter(Collider col) { 
        if (col.gameObject.tag == "EnemyDragon")
        {
            //GameObject explosion = Instantiate(Resources.Load("Explosion", typeof(GameObject))) as GameObject;
            //explosion.transform.position = transform.position;
            Destroy(col.gameObject);
            //Destroy(explosion,2);
            Destroy(gameObject);     
        }    
    }
}
