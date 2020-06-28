using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    private int openingDirection;
    private bool spawned = false;
    // Start is called before the first frame update
    void Start()
    {

        if(this.name.Contains("North"))
        {
            openingDirection = 1;
        }
        else if (this.name.Contains("West"))
        {
            openingDirection = 2;
        }
        else if (this.name.Contains("South"))
        {
            openingDirection = 3;
        }
        else if (this.name.Contains("East"))
        {
            openingDirection = 4;
        }
        Invoke("Spawn", 0.1f);
    }
    void Spawn()
    {
        //if(spawned)
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
