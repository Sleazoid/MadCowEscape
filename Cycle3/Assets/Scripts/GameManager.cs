using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    InputActions inputActions;
    [SerializeField]
    private GameObject LevelClearedPanelGO;
    [SerializeField]
    private GameObject LevelFailedPanelGO;
    [SerializeField]
    private AudioSource mainAudio;
    private WacoomInputTopDown wacoomInputs;
    private EnemyMove[] enemyMoves;
    [SerializeField]
    private GameObject MainMenuCanvas;
    [SerializeField]
    private PlayableDirector levelClearTimeline;
    [SerializeField]
    private List<GameObject> GameOverDisableLists;
    private int currentLevel = 0;
    [SerializeField]
    private GameObject levelStartPanelGO;
    private CinemachineImpulseSource impulseSource;
    [SerializeField]
    private CinemachineImpulseSource impulseNoiseSource;
    [SerializeField]
    private CinemachineImpulseSource impulseNoiseSourceMenu;
    private LevelClearScript levelClearScript;
    public static GameManager Instance { get => instance; }
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        inputActions = new InputActions();
        inputActions.Wacom.Quit.performed += ctx => QuitGame();
        inputActions.Wacom.Retry.performed += ctx => Retry();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        inputActions.Enable();
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //inputActions.Disable();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CancelInvoke();
        if (scene.buildIndex != 0)
        {
            
            MainMenuCanvas.SetActive(false);
            mainAudio.enabled = true;
            LevelFailedPanelGO.SetActive(false);
            LevelClearedPanelGO.SetActive(false);
            wacoomInputs = GameObject.FindGameObjectWithTag("Player").GetComponent<WacoomInputTopDown>();
            enemyMoves = FindObjectsOfType<EnemyMove>();
            GameOverDisableLists.Clear();
            GameOverDisableLists.Add(GameObject.Find("MainVolumes"));
            levelStartPanelGO.SetActive(true);
            impulseSource = GetComponent<CinemachineImpulseSource>();
            //for (int i = 0; i < GameOverDisableLists.Count; i++)
            //{
            //    GameOverDisableLists[i].SetActive(true);
            //}
            currentLevel++;
            InvokeRepeating("ImpulseNoise", 2f, 4f);
            levelClearScript = LevelClearedPanelGO.GetComponent<LevelClearScript>();
            levelClearScript.SetLevelText(currentLevel);
        }
        else
        {
            levelStartPanelGO.SetActive(false);
            MainMenuCanvas.SetActive(true);
            LevelFailedPanelGO.SetActive(false);
            LevelClearedPanelGO.SetActive(false);
            InvokeRepeating("ImpulseNoiseMenu", 2f, 4f);
            currentLevel = 0;
            if(mainAudio.enabled==false)
            {
                mainAudio.enabled = true;
            }
        }

    }
    private void ImpulseNoise()
    {
        impulseNoiseSource.GenerateImpulse();
    }
    private void ImpulseNoiseMenu()
    {
        impulseNoiseSourceMenu.GenerateImpulse();
    }
    
    public void CollisionImpulseEffect()
    {
        impulseSource.GenerateImpulse();
    }
    public void QuitGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }
    private void Retry()
    {
        SceneManager.LoadScene(0);
    }


    public void LevelCleared()
    {
        CancelInvoke();
        mainAudio.enabled = false;
        wacoomInputs.enabled = false;
        LevelClearedPanelGO.SetActive(true);
        levelClearTimeline.Play();
        for (int i = 0; i < GameOverDisableLists.Count; i++)
        {
            GameOverDisableLists[i].SetActive(false);
        }
    }

    public void LevelFailed()
    {
        CancelInvoke();
        CollisionImpulseEffect();
        mainAudio.enabled = false;
        wacoomInputs.enabled = false;
        LevelFailedPanelGO.SetActive(true);
        StartCoroutine(walkAwayEnemies());
        for (int i = 0; i < GameOverDisableLists.Count; i++)
        {
            GameOverDisableLists[i].SetActive(false);
        }
    }
    private IEnumerator walkAwayEnemies()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < enemyMoves.Length; i++)
        {
            enemyMoves[i].WalkAway = true;
        }

        yield return null;
    }
    public void PlayerDead()
    {
        LevelFailed();
    }

    public void LoadNewLevel(bool next)
    {
        if (next)
        {
            Scene thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.buildIndex + 1);
        }
        else
        {
            Scene thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.buildIndex);
        }
    }
}
