using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPanelScript : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private ScrollingTextUI scrollingMusicTextScript;

    private bool isShowing = false;
    private int MusicPlayingAnimID;
    private int MusicDissapearingAnimID;
    private void Awake()
    {
        MusicPlayingAnimID = Animator.StringToHash("MusicPlayinganim");
        MusicDissapearingAnimID = Animator.StringToHash("MusicPlayAnimDissapear");
    }
    public void ShowMusicInfo()
    {
     
        if(isShowing)
        {
            CancelInvoke("HideScript");
            CancelInvoke("HideMusicInfo");
        }
    
        scrollingMusicTextScript.enabled = true;
        anim = GetComponent<Animator>();
        anim.Play(MusicPlayingAnimID);
        Invoke("HideMusicInfo", 10f);
        Invoke("HideScript", 13f);
        scrollingMusicTextScript.CheckTextLength();
        isShowing = true;
    }
    public void HideMusicInfo()
    {
        anim.Play(MusicDissapearingAnimID);
        isShowing = false;
    }
    public void HideMusicInfoIfVisible()
    {
        if(isShowing)
        {
            anim.Play(MusicDissapearingAnimID);
        }
       
    }
    private void HideScript()
    {
        scrollingMusicTextScript.enabled = false;
    }
}
