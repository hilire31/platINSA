using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FinalScorescene : MonoBehaviour
{
    
    public static FinalScorescene instance;
    public TMP_Text scoreText;
    
    int score;
    
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

        void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.Return)) // Si l'utilisateur appuie sur Entrée
        {
            SceneManager.LoadScene(0); // Charge la scène 1
        }
    }
}



