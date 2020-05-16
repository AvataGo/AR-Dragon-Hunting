using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon = false;
    
    private bool isGunSwitchHand;

    
    //public static Transform currentWeapon;
    public static GunWeapon currentWeapon;
    public static Animator currentWeaponAnim;

    [SerializeField]
    private string currentWeaponType;


    [SerializeField]
    private float changeWeaponDelayTime;
    [SerializeField]
    private float changeWeaponEndDelayTime;

    [SerializeField]
    private GunWeapon[] handGuns;
    [SerializeField]
    private GunWeapon[] autoGuns;

    private Dictionary<string, GunWeapon> handGunDictorary = new Dictionary<string, GunWeapon>();
    private Dictionary<string, GunWeapon> autoGunDictorary = new Dictionary<string, GunWeapon>();

    [SerializeField]
    private HandGunController theHandGunController;
    [SerializeField]
    private AutoGunController theAutoGunController;

    public Button gunSwitchButton;


    // Start is called before the first frame update
    void Start()
    {
        gunSwitchButton.onClick.AddListener(OnGunSwitchButton);

        for (int i = 0; i < handGuns.Length; i++)
        {
            handGunDictorary.Add(handGuns[i].gunWeaponName, handGuns[i]);
        }
        for (int i = 0; i < autoGuns.Length; i++)
        {
            autoGunDictorary.Add(autoGuns[i].gunWeaponName, autoGuns[i]);
        }
        
        isGunSwitchHand = true;
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        isChangeWeapon = true;
        currentWeaponAnim.SetTrigger("Weapon_Out");

        yield return new WaitForSeconds(changeWeaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type, _name);

        yield return new WaitForSeconds(changeWeaponEndDelayTime);

        currentWeaponType = _type;
        isChangeWeapon = false;
    }

    private void CancelPreWeaponAction()
    {
        switch(currentWeaponType)
        {
            case "HandGun":
                theHandGunController.CancelFineSight();
                theHandGunController.CancelReload();
                HandGunController.isActivate = false;
                break;

            case "AutoGun":
                theAutoGunController.CancelFineSight();
                theAutoGunController.CancelReload();
                AutoGunController.isActivate = false;
                break;
        }
    }

    private void WeaponChange(string _type, string _name)
    {
        if(_type == "HandGun")
            theHandGunController.GunWeaponChange(handGunDictorary[_name]);
        else if(_type == "AutoGun")
            theAutoGunController.GunWeaponChange(autoGunDictorary[_name]);
    }

    private void OnGunSwitchButton()
    {
        if(!isChangeWeapon)
        {
            isGunSwitchHand = !isGunSwitchHand;

            if(isGunSwitchHand)
                StartCoroutine(ChangeWeaponCoroutine("HandGun", "HandGun1"));
            else
                StartCoroutine(ChangeWeaponCoroutine("AutoGun", "AutoGun1"));
        }   
    }
}
