using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class EndFlag : MonoBehaviour
{
    public bool isTuto;
    [SerializeField] private int sceneNumber; 
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            if (isTuto){
                ScoreManager.instance.Reset();
            }
            SceneController.instance.LoadScene(sceneNumber); 

        }
    }

}
