using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;



public class EndFlag : MonoBehaviour
{
    public bool isTuto;
    [SerializeField] private int sceneNumber; 
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            ScoreManager.instance.AddPoint();
            
            if (isTuto){
                ScoreManager.instance.Reset();
            }
            SceneManager.LoadScene(5);
            SceneController.instance.LoadScene(sceneNumber); 

        }
    }

}
