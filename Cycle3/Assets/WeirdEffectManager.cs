using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdEffectManager : MonoBehaviour
{
    [SerializeField]
    private float effectStartTime = 2f;
    [SerializeField]
    private float effectTimingSpeed = 2f;
    private float effectLength = 1f;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("PlayEffect", effectStartTime, effectTimingSpeed);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void PlayEffect()
    {
        anim.Play("weirdEffectVolumeAnim");

    }
}
