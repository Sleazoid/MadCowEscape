using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDudeAnimScript : MonoBehaviour
{
    private Vector3 initPos;
    Camera mainCam;
    [SerializeField]
    private float movingSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        initPos = this.transform.position;
        mainCam = Camera.main;
        InvokeRepeating("CheckReSpawn", 5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(transform.right * movingSpeed);
    }
    private void CheckReSpawn()
    {
        Vector3 pos = mainCam.WorldToViewportPoint(transform.position);

       // if (pos.x < 0.0) Destroy(this.gameObject); ;
        if (1.0 < pos.x) ReSpawn();
        if (pos.y < 0.0) ReSpawn();
        if (1.0 < pos.y) ReSpawn();

    }

    private void ReSpawn()
    {
        this.transform.position = initPos;
    }
}
