using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using Cinemachine;
using TMPro;

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
    private int numberOfEnemies;
    private int diedEnemiesCount;
    private GoalScript goalScript;
    private EnemyShotgunMove[] shotGunEnemyScripts;
    private EnemyMove[] basicEnemyScripts;
    private EnemyGrenadeMove[] grenadeEnemyScripts;
    private GameObject playerObject;
    private bool useGamepad = true;
    [SerializeField]
    private List<MusicSO> musicPlaylist;
    private int songCount;
    [SerializeField]
    private int currentMusicIndex=0;
    private float currentMusicIndexTime=0;
    private bool musicIsPlaying = true;
    [SerializeField]
    private float songVolume;
    public static GameManager Instance { get => instance; }
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public GoalScript GoalScript { get => goalScript; set => goalScript = value; }
    public GameObject PlayerObject { get => playerObject; set => playerObject = value; }
    public bool UseGamepad { get => useGamepad; set => useGamepad = value; }

    public delegate void OnLevelFailedChange(bool state);
    public static event OnLevelFailedChange FailedEvent;
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
        inputActions.Wacom.NextLevel.performed += ctx => NextLevelCheat();


        //currentMusicIndex = 0;
        
    }
    private void Start()
    {
        songCount = musicPlaylist.Count;
        mainAudio.clip = musicPlaylist[currentMusicIndex].audio;
        songVolume = musicPlaylist[currentMusicIndex].volume;
        mainAudio.volume = songVolume;
        PlayMusic();
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
    private void PlayMusic()
    {
        mainAudio.Play();
        musicIsPlaying = true;
        StartCoroutine("VolumeSlideUp");
    }
    private void PauseMusic()
    {
        StartCoroutine("VolumeSlideDown");
      
    }
    private IEnumerator VolumeSlideUp()
    {
        float volume = mainAudio.volume;
        while(volume<songVolume)
        {
            volume += 0.01f;
            mainAudio.volume = volume;
            yield return new WaitForSeconds(0.01f);
            
        }

        yield return null;
    }
    private IEnumerator VolumeSlideDown()
    {
        float volume = mainAudio.volume;
        while (volume > 0)
        {
            volume -= 0.1f;
            mainAudio.volume = volume;
            yield return new WaitForSeconds(0.01f);
        }
        mainAudio.Pause();
        musicIsPlaying = false;
        yield return null;
    }
    private void NextSong()
    {
        
        if (currentMusicIndex+1 < songCount)
        {
            currentMusicIndex++;
            mainAudio.clip = musicPlaylist[currentMusicIndex].audio;
            PlayMusic();
        }
        else
        {
            currentMusicIndex = 0;
            mainAudio.clip = musicPlaylist[currentMusicIndex].audio;
            PlayMusic();
        }
        currentMusicIndexTime = 0;
        songVolume = musicPlaylist[currentMusicIndex].volume;
        mainAudio.volume = songVolume;
    }
    private void Update()
    {
        if(musicIsPlaying)
        {
            currentMusicIndexTime += Time.deltaTime;
            if(currentMusicIndexTime>mainAudio.clip.length)
            {
                
                musicIsPlaying = false;
                NextSong();
            }
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CancelInvoke();
        if (scene.buildIndex != 0)
        {

            MainMenuCanvas.SetActive(false);
            //mainAudio.enabled = true;'
            PlayMusic();


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
            currentLevel = SceneManager.GetActiveScene().buildIndex;
            InvokeRepeating("ImpulseNoise", 2f, 4f);
            levelClearScript = LevelClearedPanelGO.GetComponent<LevelClearScript>();
            levelClearScript.SetLevelText(currentLevel);
            
            numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            diedEnemiesCount = 0;
            shotGunEnemyScripts = FindObjectsOfType<EnemyShotgunMove>();
            basicEnemyScripts = FindObjectsOfType<EnemyMove>();
            grenadeEnemyScripts = FindObjectsOfType<EnemyGrenadeMove>();
            PlayerObject = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            levelStartPanelGO.SetActive(false);
            MainMenuCanvas.SetActive(true);
            LevelFailedPanelGO.SetActive(false);
            LevelClearedPanelGO.SetActive(false);
            InvokeRepeating("ImpulseNoiseMenu", 2f, 4f);
            currentLevel = 0;
            if (musicIsPlaying == false)
            {
                PlayMusic();
            }
        }
        if(levelClearScript)
            levelClearScript.DisableVolume();
    }
    private void NextLevelCheat()
    {
        LoadNewLevel(true);
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
    public void ExplosionImpulseEffect()
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
        wacoomInputs.SetAttackAnimFalse();
        CancelInvoke();
        //mainAudio.enabled = false;
        PauseMusic();
        wacoomInputs.enabled = false;
        LevelClearedPanelGO.SetActive(true);
        levelClearScript.EnableVolume();
        levelClearTimeline.Play();
        for (int i = 0; i < GameOverDisableLists.Count; i++)
        {
            GameOverDisableLists[i].SetActive(false);
        }
    }
    public void SetInputDevice(int value)
    {
        switch(value)
        {
            case 0:
                useGamepad = true;
                break;
            case 1:
                useGamepad = false;
                break;
        }
    }
    public void LevelFailed()
    {
        CancelInvoke();
        CollisionImpulseEffect();
        //mainAudio.enabled = false;
        PauseMusic();
        wacoomInputs.enabled = false;
        LevelFailedPanelGO.SetActive(true);
        StartCoroutine(walkAwayEnemies());
        for (int i = 0; i < GameOverDisableLists.Count; i++)
        {
            GameOverDisableLists[i].SetActive(false);
        }
        FailedEvent?.Invoke(true);
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
        for (int i = 0; i < shotGunEnemyScripts.Length; i++)
        {
            shotGunEnemyScripts[i].PlayerIsDead();
        }
        for (int i = 0; i < basicEnemyScripts.Length; i++)
        {
            basicEnemyScripts[i].PlayerIsDead();
        }
        for (int i = 0; i < grenadeEnemyScripts.Length; i++)
        {
            grenadeEnemyScripts[i].PlayerIsDead();
        }
        LevelFailed();
    }

    public void LoadNewLevel(bool next)
    {
        if (next)
        {
            Scene thisScene = SceneManager.GetActiveScene();
            int nextScene = thisScene.buildIndex + 1;
            //Debug.Log(nextScene);
            //Debug.Log(SceneManager.sceneCount);
            if (SceneManager.sceneCountInBuildSettings<=nextScene)
            {
                nextScene = 0;
            }
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Scene thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.buildIndex);
        }
    }
    public void EnemyDied()
    {
        diedEnemiesCount++;
        if(diedEnemiesCount>= numberOfEnemies)
        {
            goalScript.OpenTheGoal();
        }
    }

    public void SetDamageToPlayer(Vector3 pos)
    {
        wacoomInputs.PlayerGotDriveOver(pos);
    }
}
