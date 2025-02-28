using Unity.VisualScripting;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float bounceForce = 100f; // Intensité du rebond

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                
                // Récupère le point de contact et la normale
                ContactPoint2D contact = collision.GetContact(0);
                Vector2 normal = contact.normal; // Vecteur perpendiculaire à la surface du bumper
                // Réinitialise la vitesse pour éviter les conflits
                rb.velocity = Vector2.zero;

                // Applique la force dans la direction de la normale
                rb.AddForce(normal * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
