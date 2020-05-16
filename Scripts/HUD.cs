using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private HandGunController theHandGunController;

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
        currentGun = theHandGunController.GetWeaponGun();
        testsBullet[0].text = currentGun.carryBulletCount.ToString();
        testsBullet[1].text = currentGun.reloadBulletCount.ToString();
        testsBullet[2].text = currentGun.currentBulletCount.ToString();
    }
}
