using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class NeighborPoint : MonoBehaviour
{
    public Vector2 spawnPoint;
    private void Awake()
    {
        Tilemap tile = GetComponentInChildren<Tilemap>();
        Vector3Int size = tile.size;
        if(this.gameObject.name.Contains("S"))
        {
            spawnPoint = new Vector2(this.transform.position.x, this.transform.position.y - size.y);
            GameObject spawn = new GameObject("spawn");
            spawn.transform.parent = this.transform;
            spawn.transform.position = spawnPoint;
        }
        //Vector2 point = this.GetComponent<>
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
