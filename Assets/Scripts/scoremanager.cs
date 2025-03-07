using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
}
