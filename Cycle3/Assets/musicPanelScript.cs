using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPanelScript : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

 

    public void ShowMusicInfo()
    {
        anim = GetComponent<Animator>();
        anim.Play("MusicPlayinganim");
        Invoke("HideMusicInfo", 5f);
    }
    private void HideMusicInfo()
    {
        anim.Play("MusicPlayAnimDissapear");
    }
}
