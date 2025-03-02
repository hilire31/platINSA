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
        //scoreText.text = score.ToString() + " FLAGS";
    }

    // Update is called once per frame
    public void AddPoint() {
        score++;
        Debug.Log(score+" flags (debug à supprimer)");
        //scoreText.text = score.ToString() + " FLAGS";
    }
}
