using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public Transform player; // Le joueur
    public float parallaxEffect = 0.5f; // Facteur de parallax
    public float scrollSpeed = 0.1f; // Vitesse du défilement infini

    private Vector3 lastPlayerPosition;
    private Renderer bgRenderer;
    private float textureOffsetX = 0;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        lastPlayerPosition = player.position;
        bgRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Mouvement du joueur pour l'effet de parallax
        float deltaX = (player.position.x - lastPlayerPosition.x) * parallaxEffect;
        transform.position += new Vector3(deltaX, 0, 0);
        lastPlayerPosition = player.position;

        // Scroll infini du background
        textureOffsetX += deltaX * scrollSpeed;
        bgRenderer.material.mainTextureOffset = new Vector2(textureOffsetX, 0);
    }
}
