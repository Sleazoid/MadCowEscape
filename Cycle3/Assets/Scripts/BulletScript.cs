using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [SerializeField]
    private float destroyTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeleteObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DeleteObject()
    {
        Destroy(this.gameObject);
    }
}
