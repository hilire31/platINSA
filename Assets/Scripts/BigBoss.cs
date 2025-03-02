using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform speedPoint;
    [SerializeField] private float speed = 5f;
    
    [SerializeField] private float boostSpeed = 8f;
    

    private bool isStopped=false;
    private int localDir;
    private Vector3 target;

    private void Start()
    {
        target = new Vector3(endPoint.position.x, transform.position.y, transform.position.z);
        localDir = (endPoint.position.x > transform.position.x) ? 1 : -1;
        transform.localScale = new Vector3(localDir * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        
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
            }

        }
        
    }
}
