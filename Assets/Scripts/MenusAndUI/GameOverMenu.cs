using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameOverMenu : MonoBehaviour
{
    public int maxHealth = 5;
   public void LoadGame ()
    {
        //Check for save file. If exists, load save file, otherwise reset the last scene.
        string path = Application.persistentDataPath + "/player.saveFile";
        if (File.Exists(path))
        {
            GameManager.instance.LoadPlayer();
        }
        else
        {
            SceneManager.LoadScene(GameManager.instance.lastScene);
        }
        UIHealthBar.instance.SetValue(maxHealth);
       
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit Game.");
        Application.Quit();
    }
}
