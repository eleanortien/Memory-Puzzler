using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.root.gameObject);
    }
    public string currentScene = "OutsideTrails";
    public string lastScene = "OutsideTrails";

    public void Save()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        SaveSystem.Save(player);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.Load();
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentScene"));

    }

    //Does scene switches and keeps track of last scene.
    public void NewScene(string newSceneRoom)
    {
        lastScene = SceneManager.GetActiveScene().name;
        currentScene = newSceneRoom;
        SceneManager.LoadScene(newSceneRoom);

    }

}
