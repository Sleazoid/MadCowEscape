using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    [SerializeField]
    private GameObject ExplosionPrefab;
    [SerializeField]
    private float timeToExplode;
    private Vector3 flyToPos;
    [SerializeField]
    private float throwForce = 5f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        flyToPos = GameManager.Instance.PlayerObject.transform.position;
        Vector3 dir = flyToPos - this.transform.position;
        rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(dir.normalized* throwForce, ForceMode2D.Force);
        Invoke("Explode", timeToExplode);
    }
    private void OnDestroy()
    {
        CancelInvoke();
    }
    private void FixedUpdate()
    {
        if(Vector2.Distance(this.flyToPos,this.transform.position)<0.5f)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    private void Explode()
    {
        GameObject explo = Instantiate(ExplosionPrefab, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
