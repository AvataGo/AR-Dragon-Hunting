using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : MonoBehaviour
{
    public string gunWeaponName; // Gun Name

    public GameObject go_MainUI;

    public bool isHandGun;
    public bool isAutoGun;



    //public string gunName; // Name of Gun
    public float range; // bullet's distance
    public float accuracy; // accuracy of gun
    public float fireRate; // continuous fire rate
    public float reloadTime; // reload time

    public int damage; // damage

    public int reloadBulletCount; // count of reload
    public int currentBulletCount; // current bullet
    public int maxBulletCount; // Max carry bullet
    public int carryBulletCount; // current carry bullet


    //public float retroActionForce; // retro force
    //public float retroActionFineSightForce; // fine retro force

    //public Vector3 fineSightOriginPos;

    public Animator anim;

    public ParticleSystem muzzleflash;
    
    [Header("Muzzleflash Light Settings")]
	public Light muzzleflashLight;
	public float lightDuration = 0.02f;    
}
