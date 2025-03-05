using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void CreditsGame()
    {
        SceneManager.LoadSceneAsync(5);
    }

    public void PlayTuto()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void Son()
    {
        AudioListener.volume = 1;
      
    }

        public void Mute()

    {
        AudioListener.volume = 1;
      
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
