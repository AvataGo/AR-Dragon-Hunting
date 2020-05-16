using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    //public Vector3 playerPos;
    public int handGunBulletCount;
    public int autoGunBulletCount;
}

public class SaveAndLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTRORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private HandGunController theHandGun;
    private AutoGunController theAutoGun;

    void Start()
    {
        SAVE_DATA_DIRECTRORY = Application.dataPath + "/Saves/";
        
        if(!Directory.Exists(SAVE_DATA_DIRECTRORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTRORY);        
    }

    public void SaveData()
    {
        theHandGun = FindObjectOfType<HandGunController>();
        theAutoGun = FindObjectOfType<AutoGunController>();

        saveData.handGunBulletCount = theHandGun.currentGunWeapon.currentBulletCount;
        saveData.autoGunBulletCount = theAutoGun.currentGunWeapon.currentBulletCount;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTRORY + SAVE_FILENAME, json);

        Debug.Log("Saved");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if(File.Exists(SAVE_DATA_DIRECTRORY + SAVE_FILENAME))
        {
            theHandGun = FindObjectOfType<HandGunController>();
            theAutoGun = FindObjectOfType<AutoGunController>();

            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTRORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            theHandGun.currentGunWeapon.currentBulletCount = saveData.handGunBulletCount;
            theAutoGun.currentGunWeapon.currentBulletCount = saveData.autoGunBulletCount;

            Debug.Log("Loaded");          
        }
        else
            Debug.Log("No Saved Data");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
