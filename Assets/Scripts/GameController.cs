using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    Rigidbody2D playerRB;
    bool globalCanSpawn;
    public GameObject corpse;
    private void Start(){
        globalCanSpawn = true;
        checkPointPos = transform.position;
        playerRB = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Obstacle")){
            Die();
        }
    }

    public void Die(){
        Respawn(2);
    }
    public void UpdateCheckPoint(Vector2 pos){
        checkPointPos=pos;
    }
    void Respawn(float duration){
        playerRB.velocity = new Vector2(0,0);
        playerRB.simulated=false;
        transform.localScale=new Vector3(0,0,0);

        if (globalCanSpawn){
            Instantiate(corpse, transform.position, Quaternion.identity);
        }
        
        StartCoroutine(Wait(duration));
        transform.position=checkPointPos;
        transform.localScale=new Vector3(1,1,1);
        playerRB.simulated=true;


    }
    public void UpdateCanSpawn(bool canSpawn){
        globalCanSpawn=canSpawn;
    }
    IEnumerator Wait(float duration)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(duration);
    }
}
