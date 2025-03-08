using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        ScoreManager.instance.Reset();
        SceneManager.LoadScene(2);
    }

    public void PlayTuto()
    {
        ScoreManager.instance.Reset();
        SceneManager.LoadScene(3);
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
