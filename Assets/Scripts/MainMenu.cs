using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (ScoreManager.instance){
            ScoreManager.instance.Reset();
            SceneManager.LoadScene(2);
        }
        else{
            SceneManager.LoadSceneAsync(2);
        }
        
    }

    public void PlayTuto()
    {
        if (ScoreManager.instance){
            ScoreManager.instance.Reset();
            SceneManager.LoadScene(3);
        }
        else{
            SceneManager.LoadSceneAsync(3);
        }
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
