using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float parallaxEffectX = 0.5f; // Réglable pour ajuster la vitesse du fond en Y
    public float parallaxEffectY = 0.05f; // Réglable pour ajuster la vitesse du fond en Y
    private Transform player;
    private Vector3 lastPlayerPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        float deltaX = player.position.x - lastPlayerPosition.x;
        float deltaY = player.position.y - lastPlayerPosition.y;
        transform.position += new Vector3(deltaX * parallaxEffectX, deltaY * parallaxEffectY, 0);
        lastPlayerPosition = player.position;
    }
}
