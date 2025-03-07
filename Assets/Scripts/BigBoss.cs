using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BigBoss : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform speedPoint;
    [SerializeField] private Transform slowPoint;
    [SerializeField] private float speed = 5f;
    
    [SerializeField] private float boostSpeed = 8f;
    

    private bool isStopped=false;
    private int localDir;
    private Vector3 target;
    private Vector3 originTransform;
    private float speedInit,normalSpeed;
    public float slowedDuration,slowedSpeed;
    private bool isSlowed = true;

    private void Start()
    {
        speedInit=speed;
        target = new Vector3(endPoint.position.x, transform.position.y, transform.position.z);
        localDir = (endPoint.position.x > transform.position.x) ? 1 : -1;
        transform.localScale = new Vector3(localDir * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        originTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        normalSpeed=speedInit;
    }


    void Update()
    {
        if (!isStopped){
            MoveBetweenPoints();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            //SceneController.instance.NextLevel(); load longer level ?
            transform.position=originTransform;
            speed = speedInit;
            
        }
    }
    private void MoveBetweenPoints()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            isStopped=true;
        }
        if (speedPoint!=null){
            if (Mathf.Abs(transform.position.x - speedPoint.position.x) < 0.1f){
                speed = boostSpeed;
                normalSpeed=speed;
            }

        }
        if (slowPoint!=null){
            if (Mathf.Abs(transform.position.x - slowPoint.position.x) < 0.1f){
                if (isSlowed){
                    StartCoroutine(SlowSpeed());
                }
            }

        }
        
        
    }
    private IEnumerator SlowSpeed(){
        
        isSlowed=true;
        speed = slowedSpeed;
        
        yield return new WaitForSeconds(slowedDuration);
        
        speed = normalSpeed;
    }
}
