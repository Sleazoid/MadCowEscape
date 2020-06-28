using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrail : MonoBehaviour
{
    [SerializeField]
    private float trailDestroyTime = 0.04f;
    List<GameObject> trailParts = new List<GameObject>();
    public Color trailColor;
    void SpawnTrailPart()
    {
        GameObject trailPart = new GameObject("PlayerTrail");
        trailPart.transform.rotation = this.transform.rotation;
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.flipX = true;
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        trailPart.transform.position = transform.position;
        //trailPart.transform.localScale = transform.parent.transform.localScale;
        trailParts.Add(trailPart);
        

        StartCoroutine(FadeTrailPart(trailPartRenderer));
        Destroy(trailPart, trailDestroyTime); // replace 0.5f with needed lifeTime
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        
        //Color color = trailPartRenderer.color;
        //color.a -= 0.5f;
        //color.r -= 0.3f;
        //color.g -= 0.3f;// replace 0.5f with needed alpha decrement
        trailPartRenderer.color = trailColor;

        yield return new WaitForEndOfFrame();
        
    }
    IEnumerator DestroyTrailPart(GameObject trailPart, float delay)
    {
        yield return new WaitForSeconds(delay);

        trailParts.Remove(trailPart);
        Destroy(trailPart);
    }
    //public void FlipTrail()
    //{
    //    if(trailParts.Count != 0)
    //    {
    //        foreach (GameObject trailPart in trailParts)
    //        {
    //            if(trailPart != null)
    //            {
    //                Vector3 trailPartLocalScale = trailPart.transform.localScale;
    //                trailPartLocalScale.x = transform.parent.localScale.x;
    //                trailPart.transform.localScale = transform.parent.localScale;
    //            }
                
    //        }
    //    }
        
    //}
   

}
