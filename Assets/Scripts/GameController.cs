using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

        
        
        
        
        

public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    Rigidbody2D playerRB;
    private bool globalCanSpawn;
    public GameObject corpse;
    private Animator anim;

    float bumperForce = 7;

    //AudioSource bump_sound;

    private void Start(){
        globalCanSpawn = true;
        
        checkPointPos = transform.position;
        playerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Obstacle")){
            Die();
        }
        if (collision.CompareTag("Solid") || collision.CompareTag("Obstacle")){
            anim.SetBool("isJumpingDown",false);
            anim.SetBool("isJumpingUp",false);
            anim.SetBool("isJumpingDown",false);
        }
    
        if (collision.CompareTag("Bumper") && false){
                //bump_sound=GetComponent<AudioSource>();
                Debug.Log("bump");
                ContactPoint2D[] contacts = null;
                collision.GetContacts(contacts);
                var norm = contacts[0].normal;
                playerRB.velocity = Vector2.zero;
                playerRB.AddForce( -1 * norm * bumperForce,  ForceMode2D.Impulse);
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("GameZone")){
            UpdateCanSpawn(false);
            Die();

        }
    }
    public void Die(){
        Respawn(0.25f);
        
    }
    public void UpdateCheckPoint(Vector2 pos){
        checkPointPos=pos;
    }
    void Respawn(float duration){
        playerRB.velocity = new Vector2(0,0);
        
        playerRB.simulated=false;
        anim.SetBool("isDying",true);
        //yield return new WaitForSeconds(2);
        anim.SetBool("isDying",false);
        
        transform.localScale=new Vector3(0,0,0);

        if (globalCanSpawn){
            Instantiate(corpse, transform.position, Quaternion.identity);
        }
        UpdateCanSpawn(false);
        transform.position=checkPointPos;
        transform.localScale=new Vector3(0.5f,0.5f,1);
        playerRB.simulated=true;
        

    }
    public void UpdateCanSpawn(bool canSpawn){
        globalCanSpawn=canSpawn;
    }
    [SerializeField] bool die;
    void OnUpdate(){
        if (die) {
            anim.SetBool("isDying",true);
        }
    }
}
