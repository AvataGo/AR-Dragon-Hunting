using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{ 
    public static int currentEnemyCount = 1;

    EnemyStats enemyStats;
    private int enemyCount = 1 ;
    private bool gameOver = false;
    private float FlyDragonY = -12.5f;

    enum EnemyStats
    {
        UsurperR = 0,
        UsurperG = 1,
        UsurperB = 2,
        UsurperP = 3,
        TerrorBringerR = 4,
        TerrorBringerG = 5,
        TerrorBringerB = 6,
        TerrorBringerP = 7,
        SoulEaterR = 8,
        SoulEaterB = 9,
        SoulEaterG = 10,
        SoulEaterGr = 11      
    }
    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    IEnumerator EnemySpawner()
    {
        while(true)
        {
    #if UNITY_EDITOR
            yield return new WaitForSeconds(1.0f);
    #else
            yield return new WaitForSeconds(6.5f);
    #endif
            updateEnemySpawer(); 

            if(GameObject.FindGameObjectsWithTag("NPC").Length < 1 && !gameOver)
            {
                gameOver = true;
                currentEnemyCount = 0;
                Debug.Log("Game Over");
            }           
        }
    }

    private void updateEnemySpawer()
    {        
        if(enemyCount >= 6)
        {
            return;
        }

        enemyCount ++;
        currentEnemyCount ++;
        //Debug.Log("enemyCount:" + enemyCount);

#if UNITY_EDITOR
        enemyStats = (EnemyStats)Random.Range(0, 1);
#else
        enemyStats = (EnemyStats)Random.Range(0, 12);
#endif
        switch(enemyStats)
        {
#if UNITY_EDITOR
            case EnemyStats.UsurperR:
                GameObject UsurperR = Instantiate(Resources.Load("UsurperR", typeof(GameObject))) as GameObject;
                UsurperR.transform.localPosition = new Vector3(Random.Range(-1,1),                 
                                                                0, 
                                                                Random.Range(10,11));

                break; 
            
            case EnemyStats.UsurperG:
                GameObject UsurperG = Instantiate(Resources.Load("UsurperG", typeof(GameObject))) as GameObject;
                UsurperG.transform.localPosition = new Vector3(Random.Range(-1,1),                 
                                                                0, 
                                                                Random.Range(10,11));
                break;                 
#else 
            case EnemyStats.UsurperR:
                GameObject UsurperR = Instantiate(Resources.Load("UsurperR", typeof(GameObject))) as GameObject;       
                UsurperR.transform.localPosition = new Vector3(Random.Range(-20,20), 
                                                                FlyDragonY, 
                                                                Random.Range(20,50));

                break; 
            
            case EnemyStats.UsurperG:
                GameObject UsurperG = Instantiate(Resources.Load("UsurperG", typeof(GameObject))) as GameObject;
                UsurperG.transform.localPosition = new Vector3(Random.Range(-20,20), 
                                                                FlyDragonY, 
                                                                Random.Range(20,50));
                                                                
                break;
#endif


            case EnemyStats.UsurperB:
                GameObject UsurperB = Instantiate(Resources.Load("UsurperB", typeof(GameObject))) as GameObject;
                UsurperB.transform.localPosition = new Vector3(Random.Range(-20,20), 
                                                                FlyDragonY, 
                                                                Random.Range(20,50));
                break;

           case EnemyStats.UsurperP:
                GameObject UsurperP = Instantiate(Resources.Load("UsurperP", typeof(GameObject))) as GameObject;
                UsurperP.transform.localPosition = new Vector3(Random.Range(-20,20), 
                                                                FlyDragonY, 
                                                                Random.Range(20,50));
                break;

           case EnemyStats.TerrorBringerR:
                GameObject TerrorBringerR = Instantiate(Resources.Load("TerrorBringerR", typeof(GameObject))) as GameObject;
                TerrorBringerR.transform.localPosition = new Vector3(Random.Range(-20,20), 
                                                                FlyDragonY, 
                                                                Random.Range(20,50));
                break;

           case EnemyStats.TerrorBringerB:
                GameObject TerrorBringerB = Instantiate(Resources.Load("TerrorBringerB", typeof(GameObject))) as GameObject;
                TerrorBringerB.transform.localPosition = new Vector3(Random.Range(-20,20), 
                                                                FlyDragonY, 
                                                                Random.Range(20,50));
                break;

           case EnemyStats.TerrorBringerG:
                GameObject TerrorBringerG = Instantiate(Resources.Load("TerrorBringerG", typeof(GameObject))) as GameObject;
                TerrorBringerG.transform.localPosition = new Vector3(Random.Range(-20,20), 
                                                                FlyDragonY, 
                                                                Random.Range(20,50));
                break;

           case EnemyStats.TerrorBringerP:
                GameObject TerrorBringerP = Instantiate(Resources.Load("TerrorBringerP", typeof(GameObject))) as GameObject;
                TerrorBringerP.transform.localPosition = new Vector3(Random.Range(-20,20), 
                                                                FlyDragonY, 
                                                                Random.Range(20,50));
                break;

            case EnemyStats.SoulEaterR:
                GameObject SoulEaterR = Instantiate(Resources.Load("SoulEaterR", typeof(GameObject))) as GameObject;
                SoulEaterR.transform.localPosition = new Vector3(Random.Range(-20,20), 
                                                                FlyDragonY, 
                                                                Random.Range(20,50));
                break;

            case EnemyStats.SoulEaterB:
                GameObject SoulEaterB = Instantiate(Resources.Load("SoulEaterB", typeof(GameObject))) as GameObject;
                SoulEaterB.transform.localPosition = new Vector3(Random.Range(-20,20), 
                                                                FlyDragonY, 
                                                                Random.Range(20,50));
                break;

            case EnemyStats.SoulEaterG:
                GameObject SoulEaterG = Instantiate(Resources.Load("SoulEaterG", typeof(GameObject))) as GameObject;
                SoulEaterG.transform.localPosition = new Vector3(Random.Range(-20,20), 
                                                                FlyDragonY, 
                                                                Random.Range(20,50));
                break;
            case EnemyStats.SoulEaterGr:
                GameObject SoulEaterGr = Instantiate(Resources.Load("SoulEaterGr", typeof(GameObject))) as GameObject;
                SoulEaterGr.transform.localPosition = new Vector3(Random.Range(-20,20), 
                                                                FlyDragonY, 
                                                                Random.Range(20,50));
                break;

            default:
                Debug.Log("Missing enemy spawner");
                break;
        }
    }
}
