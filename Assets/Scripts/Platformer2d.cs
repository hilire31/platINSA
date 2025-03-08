using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Platformer2d : MonoBehaviour {
    protected Rigidbody2D m_Rigidbody2D;
    protected CapsuleCollider2D mainCollider;

    [SerializeField] protected float m_Speed = 5f;
    public float normalSpeed = 5f;
    public float boostedSpeed = 8f;
    public float boostDuration = 3f;
    private bool isBoosted = false;
    [SerializeField] protected float m_JumpHeight = 8f;

    [SerializeField] private float fallMultiplier = 2f; // Multiplicateur pour accélérer la chute
    protected float m_Direction = 0;

    protected bool m_DoubleJump = true;
    [Range(-0.25f, 0.25f), SerializeField] protected float skinWidth = 0f;
    public LayerMask groundLayer;
    private Animator anim;

    public InputActionReference MoveAction;
    public InputActionReference JumpAction;
    public InputActionReference ShellAction;
    public InputActionReference QuitAction;

    protected Vector2 directionVector = new(0,0);
    public AudioSource audioSourceJump;
    void Start() {
        anim=GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        mainCollider = GetComponentInChildren<CapsuleCollider2D>();
         
    }

    private void OnEnable()
    {
        MoveAction.action.performed += OnMoveActionPerformed;
        MoveAction.action.canceled += OnMoveActionCanceled;
        MoveAction.action.Enable();

        JumpAction.action.started += OnJumpActionStarted;
        JumpAction.action.Enable();

        ShellAction.action.started += OnShellActionStarted;
        ShellAction.action.Enable();

        QuitAction.action.started += OnQuitActionStarted;
        QuitAction.action.Enable();
    }

    private void OnQuitActionStarted(InputAction.CallbackContext context)
    {
        SceneController.instance.LoadScene(1);
    }

    private void OnShellActionStarted(InputAction.CallbackContext context)
    {
        gameController.Die();
    }

    private void OnMoveActionPerformed(InputAction.CallbackContext context)
    {
        directionVector=context.ReadValue<Vector2>();
        anim.SetBool("isRunning",true);
    }
    private void OnMoveActionCanceled(InputAction.CallbackContext context)
    {
        directionVector=Vector2.zero;
        anim.SetBool("isRunning",false);
    }

    private void OnJumpActionStarted(InputAction.CallbackContext context)
    {
        audioSourceJump.Play();
        JumpVelocity(true, true);
    }

    private void OnDisable()
    {
        MoveAction.action.performed -= OnMoveActionPerformed;
        MoveAction.action.canceled -= OnMoveActionCanceled;
        MoveAction.action.Disable();

        JumpAction.action.started -= OnJumpActionStarted;
        JumpAction.action.Disable();

        ShellAction.action.started -= OnShellActionStarted;
        ShellAction.action.Disable();

        QuitAction.action.started -= OnQuitActionStarted;
        QuitAction.action.Disable();
    }
    GameController gameController;
    private void Awake(){
        gameController = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update() {
        // Gère les sauts et le double saut
        

        // Contrôles de direction
        //directionVector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        
        if (CheckGround()) {
            m_DoubleJump=true;
            anim.SetBool("isJumpingUp",false);
            anim.SetBool("isJumpingDown",false);
        }
        else if (!CheckGround() && m_Rigidbody2D.velocity.y<=0) {
            anim.SetBool("isJumpingDown",true);
            anim.SetBool("isJumpingUp",false);
        }
        else if (!CheckGround() && m_Rigidbody2D.velocity.y>=0) {
            anim.SetBool("isJumpingUp",true);
            anim.SetBool("isJumpingDown",false);
        }
        m_Direction = directionVector[0]; 
        /*
        if (Input.GetAxisRaw("Horizontal")!=0){
            anim.SetBool("isRunning",true);
        }else{
            anim.SetBool("isRunning",false);
        }
        */

        // Gère la rotation du personnage
        Flip(Mathf.FloorToInt(Mathf.Clamp(m_Direction, -1, 1)));

        
        }
    void FixedUpdate() {
        // Inertie pour une transition fluide
        float targetVelocityX = m_Direction * m_Speed;
        float smoothFactor = 0.07f;

        // Utilisation de MoveTowards pour interpoler la vitesse actuelle vers la vitesse cible
        float newVelocityX = Mathf.MoveTowards(m_Rigidbody2D.velocity.x, targetVelocityX, smoothFactor * m_Speed);

        // Applique la nouvelle vitesse avec l'inertie
        m_Rigidbody2D.velocity = new Vector2(newVelocityX, m_Rigidbody2D.velocity.y);
    }

    private void Flip(int f) {
        if (f != 0) {
            transform.localScale = new Vector3(f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    protected void JumpVelocity(bool keyPress, bool useDoubleJump) {
        // Saut normal
        if (CheckGround() && keyPress) {
            m_DoubleJump = true;
            Jumping();
        } 
        else if (useDoubleJump && m_DoubleJump && keyPress) {
            Jumping();
            m_DoubleJump = false;
        }
        // Saut contre le mur
        if (!m_DoubleJump && keyPress && CanJumpBack()) {
            m_Rigidbody2D.velocity = new Vector2(m_Direction * 5, m_JumpHeight);
        }
    }


    private IEnumerator BoostSpeed(){
        isBoosted = true;
        m_Speed = boostedSpeed;
        
        yield return new WaitForSeconds(boostDuration);
        
        m_Speed = normalSpeed;
        isBoosted = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boost") && !isBoosted)
        {
            
            StartCoroutine(BoostSpeed());
            Destroy(other.gameObject); // Supprime l'item de boost après usage
        }
    }
    private void Jumping() {
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpHeight);
    }


    private bool CheckGround() {
        float radius = 0.05f;

        // Vérifie le sol aux trois points de contact (gauche, centre, droite)
        bool isGrounded = Physics2D.Linecast(bottom, bottom + Vector3.down * radius, groundLayer) ||
                          Physics2D.Linecast(bottomLeft, bottomLeft + Vector3.down * radius, groundLayer) ||
                          Physics2D.Linecast(bottomRight, bottomRight + Vector3.down * radius, groundLayer);

        return isGrounded;
    }

    private bool CanJumpBack() {
        Vector3 direction = armLeft + Vector3.left * m_Direction;
        RaycastHit2D hitWall = Physics2D.Linecast(armLeft, direction, groundLayer);

        return hitWall;
    }

    private void OnDrawGizmosSelected() {
        if (mainCollider == null) {
            mainCollider = GetComponentInChildren<CapsuleCollider2D>();
        }

        float radius = 0.01f;

        Gizmos.color = Color.red;
        // Affiche les points de vérification pour le contact avec le sol
        Gizmos.DrawSphere(bottomLeft, radius);
        Gizmos.DrawSphere(bottomRight, radius);
        Gizmos.DrawSphere(bottom, radius);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(bottomLeft, bottomLeft + Vector3.down * radius);
        Gizmos.DrawLine(bottomRight, bottomRight + Vector3.down * radius);
        Gizmos.DrawLine(bottom, bottom + Vector3.down * radius);

        // Affiche les points de vérification pour le saut contre les murs
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(armLeft, radius);
        Gizmos.DrawSphere(armRight, radius);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(armLeft, armLeft + Vector3.left * radius);
        Gizmos.DrawLine(armRight, armRight + Vector3.right * radius);
    }

    // Propriétés pour accéder aux points de vérification
    protected Vector3 armLeft => new Vector3(mainCollider.bounds.min.x, mainCollider.bounds.center.y, 0);
    protected Vector3 armRight => new Vector3(mainCollider.bounds.max.x, mainCollider.bounds.center.y, 0);
    protected Vector3 bottom => new Vector3(mainCollider.bounds.center.x, mainCollider.bounds.min.y, 0);
    protected Vector3 bottomLeft => mainCollider.bounds.min + Vector3.left * skinWidth;
    protected Vector3 bottomRight => new Vector3(mainCollider.bounds.max.x, mainCollider.bounds.min.y, 0) + Vector3.right * skinWidth;
}