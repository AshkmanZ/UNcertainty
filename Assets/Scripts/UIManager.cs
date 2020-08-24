using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    bool paused;

    public Canvas pauseMenu;
    public Image bossHealth;

    public static UIManager instance;

    private void Start()
    {
        instance = this;
        paused = false;
        pauseMenu.enabled = false;
        Time.timeScale = 1;

    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Pause();
        }
    }
    public void Pause()
    {
        paused = !paused;

        pauseMenu.enabled = true;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        paused = false;
        pauseMenu.enabled = false;
        Time.timeScale = 1;

    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
