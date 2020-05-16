using UnityEngine;
using UnityEngine.UI;

public class CurrentEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject go_EnemyCountHUD;

    [SerializeField]
    private Text[] txtEnemyCount;

    void Update()
    {
        CheckEnemyCount();        
    }
    
    private void CheckEnemyCount()
    {
        txtEnemyCount[0].text = EnemyController.currentEnemyCount.ToString();
    }
}
