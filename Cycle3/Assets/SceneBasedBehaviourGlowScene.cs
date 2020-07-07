using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Tilemaps;
public class SceneBasedBehaviourGlowScene : MonoBehaviour
{
    [SerializeField]
    private TilemapRenderer disableThisTilemap;
    [SerializeField]
    private TilemapRenderer defaultTilemap;
    [SerializeField]
    private Color failedBGColor;
    [SerializeField]
    private Color defaultBGColor;
    private Camera mainCam;
    [SerializeField]
    private List<GameObject> disableWhenFail;
    private PlayableDirector playableDir;
    // Start is called before the first frame update
    void Start()
    {
        playableDir = GetComponent<PlayableDirector>();
        mainCam = Camera.main;
        //mainCam.backgroundColor = defaultBGColor;
        //disableThisTilemap.enabled = true;
        defaultTilemap.enabled = false;
    }

    private void OnEnable()
    {
        GameManager.FailedEvent += ChangeTilemap;
    }
    private void OnDisable()
    {
        GameManager.FailedEvent -= ChangeTilemap;
    }

    private void ChangeTilemap(bool value)
    {
        mainCam.backgroundColor = failedBGColor;
        disableThisTilemap.enabled = false;
        defaultTilemap.enabled = (true);
        playableDir.enabled = false;
        for (int i = 0; i < disableWhenFail.Count; i++)
        {
            disableWhenFail[i].SetActive(false);
        }
    }
}
