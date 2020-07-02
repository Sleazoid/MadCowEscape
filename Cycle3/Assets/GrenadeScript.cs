using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    [SerializeField]
    private GameObject ExplosionPrefab;
    [SerializeField]
    private float timeToExplode;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", timeToExplode);
    }
    private void OnDestroy()
    {
        CancelInvoke();
    }
    private void Explode()
    {
        GameObject explo = Instantiate(ExplosionPrefab, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
