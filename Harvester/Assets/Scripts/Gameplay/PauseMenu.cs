using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private AudioClip tap;


    // Update is called once per frame

    public void Resume() {
        SoundManager.instance.PlaySound(tap);
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause() {
        SoundManager.instance.PlaySound(tap);
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu() {
        SoundManager.instance.PlaySound(tap);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        SoundManager.instance.PlaySound(tap);
        Application.Quit();
    }

   

}
