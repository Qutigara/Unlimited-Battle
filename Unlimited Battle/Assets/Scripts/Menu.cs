using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public bool isMenuShown;
    public GameObject menu;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuManager()
    {
        if (isMenuShown == false)
        {
            menu.SetActive(true);
            isMenuShown = true;
        }
        else
        {
            menu.SetActive(false);
            isMenuShown = false;
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }


}
