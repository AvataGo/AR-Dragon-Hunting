using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDAuto : MonoBehaviour
{
    [SerializeField]
    private AutoGunController theAutoGunController;

    private GunWeapon currentGun;

    [SerializeField]
    private GameObject go_BulletHUD;

    [SerializeField]
    private Text[] testsBullet;

    void Update()
    {
        CheckBullet();        
    }
    
    private void CheckBullet()
    {
        currentGun = theAutoGunController.GetWeaponGun();
        testsBullet[0].text = currentGun.carryBulletCount.ToString();
        testsBullet[1].text = currentGun.reloadBulletCount.ToString();
        testsBullet[2].text = currentGun.currentBulletCount.ToString();
    }
}
