using UnityEngine;
using UnityEngine.SceneManagement;

public class MyFarm : MonoBehaviour
{
    [SerializeField] private AudioClip tap;
    public void LoadMenu() {
        SoundManager.instance.PlaySound(tap);
        SceneManager.LoadScene("Menu");
    }
}
