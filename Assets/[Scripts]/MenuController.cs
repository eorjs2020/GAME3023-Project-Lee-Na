using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject PauseMenu;

    private void Update()
    {
        if(PauseMenu != null)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }


    public void Resume()
    {
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
    }

    public void NewGame()
    {
        DataBase.Instance.NewGame();
        SceneManager.LoadScene("MainScene");
    }

    public void Save()
    {
        DataBase.Instance.SaveGame();
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
    }

    public void Continue()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Load()
    {
        DataBase.Instance.LoadGame();
        DataBase.Instance.LoadPlayer();
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
        SoundManager.Instance.StopMusic();
        Time.timeScale = 1.0f;
    }
  
}
    

