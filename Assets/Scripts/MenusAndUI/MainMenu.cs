using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject continueButton;
    public GameObject mainUI;
    void Start()
    {
        mainUI.SetActive(false);
        string path = Application.persistentDataPath + "/player.saveFile";
        if (File.Exists(path))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }
    public void NewGame()
    {
        mainUI.SetActive(true);
        SceneManager.LoadScene("OutsideTrials");
    }

    public void ContinueGame()
    {
        mainUI.SetActive(true);
        GameManager.instance.LoadPlayer();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
