using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Parallax : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] float scroll_Speed = 0.3f;
    [SerializeField] float ratio = 0.3f;
    [SerializeField] GameObject view_Target;

    [SerializeField] bool x_Only =true;

    Tilemap tile_Map;



    void Start()
    {
        tile_Map = GetComponent<Tilemap>();

    }

    // Update is called once per frame
    void Update()
    {
        float new_X_Position =view_Target.transform.position.x * scroll_Speed * ratio;
        float new_Y_Position =view_Target.transform.position.y * scroll_Speed * ratio;

        if (x_Only){
            tile_Map.transform.position=new Vector3(new_X_Position,tile_Map.transform.position.y,tile_Map.transform.position.z);
        }else{
            tile_Map.transform.position=new Vector3(new_X_Position,new_Y_Position,tile_Map.transform.position.z);
        }
        
    }
}
