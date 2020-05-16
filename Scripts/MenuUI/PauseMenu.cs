using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPause = false;
    private bool isHomeButton = false;
    
    [SerializeField] private GameObject go_BaseUI; 
    //[SerializeField] private GameObject go_MainUIBack; 
    
    [SerializeField] private SaveAndLoad theSaveAndLoad;     

    void Update()
    {
        if(isHomeButton)
        {
            isHomeButton = false;
            if(!isPause)
                CallMenu();
            else
                CloseMenu();
        }        
    }

    public void HomeButton()
    {
        isHomeButton = !isHomeButton;
    }
    private void CallMenu()
    {
        isPause = true;
        go_BaseUI.SetActive(true);
        //go_MainUIBack.SetActive(true);
        Time.timeScale = 0f;
    }

    private void CloseMenu()
    {
        isPause = false;
        go_BaseUI.SetActive(false);
        //go_MainUIBack.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ClickSave()
    {
        Debug.Log("Save");
        theSaveAndLoad.SaveData();
        isHomeButton = true;
    }

    public void ClickLoad()
    {
        Debug.Log("Load");
        theSaveAndLoad.LoadData();
        isHomeButton = true;
    }

    public void ClickExit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
