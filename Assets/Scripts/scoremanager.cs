using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoremanager : MonoBehaviour
{
    public static scoremanager instance;
    public TMP_Text scoreText;
    
    int score = 0;
    
    private void Awake(){
        instance = this;
    }

    void Start()
    {
        scoreText.text = score.ToString() + " FLAGS";
    }

    // Update is called once per frame
    public void AddPoint() {
        score = score +1;
        scoreText.text = score.ToString() + " FLAGS";
    }
}
