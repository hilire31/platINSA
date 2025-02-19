using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private Transform player; // Référence au joueur
    [SerializeField] private Vector3 offset = new Vector3(0, 2, 0); // Décalage par rapport au joueur
    [SerializeField] private float smoothSpeed = 0.3f; // Vitesse de transition de la caméra
    void LateUpdate() {
        if (player == null) return;

        // Calcul de la position cible
        Vector3 targetPosition = player.position + offset;
        targetPosition.y = player.position.y/10;

        // Lissage de la transition vers la position cible
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Mise à jour de la position de la caméra
        transform.position = smoothedPosition;
    }

}
