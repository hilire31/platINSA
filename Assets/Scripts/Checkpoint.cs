using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameController gameController;
    private void Awake(){
        gameController = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            gameController.UpdateCheckPoint(transform.position);
            gameController.UpdateCanSpawn(false);   
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            gameController.UpdateCanSpawn(true);
        }
    }
}
