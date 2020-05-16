using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandGunController : GunWeaponController
{
    public static bool isActivate = true;
    
    void Start()
    {
        currentGunWeapon.muzzleflashLight.enabled = false;

        audioSource = GetComponent<AudioSource>();
        theCrossHair = FindObjectOfType<CrossHair>();
        fineSightButton.onClick.AddListener(OnFineSightButton);

        WeaponManager.currentWeapon = currentGunWeapon;
        WeaponManager.currentWeaponAnim = currentGunWeapon.anim;
    }

    void Update()
    {
        if(isActivate)
        {
            AnimationCheck();
            GunFireRateCalc();
            TryFire();     
            TryReload();
        }
    }
    protected override void Shoot()
    {
        if(isFineSightMode)
        {
            currentGunWeapon.anim.Play("AimShoot", 0, 0.0f);
            theCrossHair.FireAnimation();
        }
        else
        {
            currentGunWeapon.anim.Play("Shoot",0,0.0f);
            theCrossHair.FireAnimation();
        }
        
        currentGunWeapon.currentBulletCount--;
        currentFireRate = currentGunWeapon.fireRate;

        Debug.Log("Shoot Sound" + SoundClips.shootSound);
        
        PlaySE(SoundClips.shootSound);

        StartCoroutine(MuzzleFlashLight());

        //Spawn bullet at bullet spawnpoint
        var bullet = (Transform)Instantiate (
            Prefabs.bulletPrefab,
            Spawnpoints.bulletSpawnPoint.transform.position,
            Spawnpoints.bulletSpawnPoint.transform.rotation);

        //Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = 
        bullet.transform.forward * bulletForce;

        //Spawn casing prefab at spawnpoint
        Instantiate (Prefabs.casingPrefab, 
            Spawnpoints.casingSpawnPoint.transform.position, 
            Spawnpoints.casingSpawnPoint.transform.rotation);
        
        Hit();
    }

    public override void GunWeaponChange(GunWeapon _gunWeapon)
    {
        base.GunWeaponChange(_gunWeapon);
        isActivate = true;
    }

}
