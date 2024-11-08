using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Platformer2d : MonoBehaviour {
    protected Rigidbody2D m_Rigidbody2D;
    protected BoxCollider2D mainCollider;

    [SerializeField] protected float m_Speed = 5f;
    [SerializeField] protected float m_JumpHeight = 8f;
    protected float m_Direction = 0;

    protected bool m_DoubleJump = false;
    [Range(-0.25f, 0.25f), SerializeField] protected float skinWidth = 0f;
    public LayerMask groundLayer;

    void Start() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        mainCollider = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        // Gère les sauts et le double saut
        JumpVelocity(Input.GetKeyDown(KeyCode.Space), true);

        // Contrôles de direction
        m_Direction = Input.GetAxisRaw("Horizontal");

        // Gère la rotation du personnage
        Flip(Mathf.FloorToInt(Mathf.Clamp(m_Direction, -1, 1)));
    }

void FixedUpdate() {
    // Applique le mouvement horizontal, même en l'air
    if (m_Direction != 0) {
        m_Rigidbody2D.velocity = new Vector2(m_Direction * m_Speed, m_Rigidbody2D.velocity.y);
    }
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
        // Double saut
        else if (useDoubleJump && m_DoubleJump && keyPress) {
            Jumping();
            m_DoubleJump = false;
        }
        // Saut contre le mur
        if (!m_DoubleJump && keyPress && CanJumpBack()) {
            m_Rigidbody2D.velocity = new Vector2(m_Direction * 5, m_JumpHeight);
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
            mainCollider = GetComponentInChildren<BoxCollider2D>();
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
