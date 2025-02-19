using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFlag : MonoBehaviour
{
    GameController gameController;
    private void Awake(){
        gameController = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            Debug.Log("éaaa");
            SceneController.instance.NextLevel(); 
        }
    }

}
