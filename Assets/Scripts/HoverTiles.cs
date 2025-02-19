using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class HoverTiles : MonoBehaviour
{
    private Tilemap tilemap;
    public TileBase[] destroyableTiles; // Liste des tiles qui peuvent être détruites
    public float destroyDelay = 2f; // Délai avant de détruire la tile
    public Collider2D playerCollider; // Le collider du joueur

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        if (playerCollider == null)
        {
            Debug.LogError("Le collider du joueur n'est pas assigné !");
        }
    }

    void Update()
    {
        // Vérifie les tiles en collision avec le joueur
        CheckTilesInCollision();
    }

    void CheckTilesInCollision()
    {
        // Récupère la position du joueur et son collider
        Bounds playerBounds = playerCollider.bounds;

        // Parcours toutes les tiles dans la zone du collider du joueur
        Vector3Int min = tilemap.WorldToCell(playerBounds.min);
        Vector3Int max = tilemap.WorldToCell(playerBounds.max);

        for (int x = min.x; x <= max.x; x++)
        {
            for (int y = min.y; y <= max.y; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(tilePos);

                if (tile != null && IsDestroyableTile(tile))
                {
                    StartCoroutine(DestroyTileAfterDelay(tilePos));
                }
            }
        }
    }

    bool IsDestroyableTile(TileBase tile)
    {
        foreach (TileBase destroyTile in destroyableTiles)
        {
            if (tile == destroyTile)
                return true;
        }
        return false;
    }

    IEnumerator DestroyTileAfterDelay(Vector3Int tilePos)
    {
        yield return new WaitForSeconds(destroyDelay);
        tilemap.SetTile(tilePos, null); // Détruit la tile
        Debug.Log("Tile détruite à la position: " + tilePos);
    }
}
