using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    Jumping_power float = 16f;
    private bool Bonne_direction = true;

    [Serialize_Field] private Rigidbody2D rb;
    [Serialize_Field] private Transform Collision_sol;
    [Serialize_Field] private LayerMask Sol_Layer;

// MAJ frame par frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); 

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, Jumping_Power);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        Flip();
    }

    private void FixedIpdate()
    {
        rb.velocity = new Vector2(horizontal*speed, rb.velocity.y);     
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(Collision_sol.position, 0.2f, Sol_Layer);
    }
    private void Flip()
    {
        is (Bonne_direction && horizontal < 0f || !Bonne_direction && horizontal > 0f)
        {
            Bonne_direction = !Bonne_direction;
            Vector3 localScale = tranform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}