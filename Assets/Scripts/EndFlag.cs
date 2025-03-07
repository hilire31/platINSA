using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EndFlag : MonoBehaviour
{
    
    [SerializeField] private SceneAsset scene; 
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            SceneController.instance.LoadScene(scene.name); 
        }
    }

}
