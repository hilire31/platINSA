using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bat : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool isStatic = false;
    [SerializeField] private bool isHorizontal = true;
    private Vector3 target;
    private int localDir;
    private Animator anim;
    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Les points A et B doivent être assignés dans l'inspecteur.");
            return;
        }
        target = pointB.position;
        anim = GetComponent<Animator>();

        if (!isStatic){
            localDir = (pointB.position.x > pointA.position.x) ? -1 : 1;
        
            if (isHorizontal)
            {
                transform.localScale = new Vector3(localDir * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            transform.localRotation.Set(transform.localRotation.x,transform.localRotation.y, Vector3.Angle(pointA.position, pointB.position),transform.localRotation.w); 
        }
        
    }

    void Update()
    {
        if (!isStatic){
            MoveBetweenPoints();
        }
        
    }

    private void MoveBetweenPoints()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            if (isHorizontal){
                localDir*=-1;
                transform.localScale = new Vector3(localDir * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
            
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            isStatic=true;
            anim.SetBool("isDying",true);
            Destroy(transform.parent.gameObject, 0.5f);
        }
    }
}

