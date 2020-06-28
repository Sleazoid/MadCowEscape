using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleScript : MonoBehaviour
{
    Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        InvokeRepeating("CheckToDestroy", 5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckToDestroy()
    {
        Vector3 pos = mainCam.WorldToViewportPoint(transform.position);

        if (pos.x < 0.0) Destroy(this.gameObject); ;
        if (1.0 < pos.x) Destroy(this.gameObject);
        if (pos.y < 0.0) Destroy(this.gameObject);
        if (1.0 < pos.y) Destroy(this.gameObject);

    }
}
