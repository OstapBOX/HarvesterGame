using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public void LoadHangar() {
        SceneManager.LoadScene("Hangar");
    }

    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }
}
