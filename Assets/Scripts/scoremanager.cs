using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text scoreText;
    
    int score = 0;
    
    private void Awake(){
        if (instance==null){
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        scoreText.text = score.ToString() + " FLAGS";
    }

    public void AddPoint() {
        score++;
        scoreText.text = score.ToString() + " FLAGS";
    }
    public void Reset() {
        score=0;
        scoreText.text = score.ToString() + " FLAGS";
    }
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Si l'utilisateur appuie sur Entrée
        {
            SceneManager.LoadScene(1); // Charge la scène 1
        }
    }

}
