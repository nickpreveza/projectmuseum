using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Data")]
    public GameData data;

    [Header("Systems Status")]
    public bool gameReady;

    [Header("Game Status")]
    public bool isPaused;
    public bool menuActive;


    public bool isSinglePlayer;

    [Header("Debug Settings")]
    public bool allowOnlyAIPlayersToPlayer;
    public bool VisualizeAIMoves;
    public bool destroyResourcesToo;
    public bool useRandomSeed;
    public bool allAbilitiesUnlocked;
    public bool startWithALotOfMoney;
    public bool noFog;
    public bool allowShipUpgradeEverywhere;
    public bool createStuff;
    public bool infiniteCurrency;
    //public bool noScenceChanges;
    // public bool noSave;
    // public bool noLoad;

    [Header("PostProcess")]
    public Volume globalVolume;



    [Header("Game Prefabs")]
    public GameObject playerPrefab;
    public GameObject interactionParticle;
    public GameObject dropParticle;
    public GameObject placeParticle;


    [Header("Debug")]
    [SerializeField] GameObject assetTest;

    public Player activePlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        //disables the asset test gameobject to remove test objects through script   
        assetTest?.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused) //previous state
            {
                //return to play
                Time.timeScale = 1;
                vp_Utility.LockCursor = true;
                
                //Cursor.lockState = CursorLockMode.Confined;
                //Cursor.visible = false;
            }
            else
            {
                //pause menu
                Time.timeScale = 0;
                vp_Utility.LockCursor = false;
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;
            }
            
            isPaused = !isPaused;
            UIManager.Instance.PauseChanged();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /*
        if (!menuPassed)
        {
            SetUpPersistentData(false);
        }
        else
        {
            SetUpPersistentData(true);
        }*/

        Time.timeScale = 1;
    }


    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        AudioManager.Instance.PlayTheme(AudioManager.Instance.gameTheme);

        //debug
        UIManager.Instance.OpenGamePanel();
    }

    public void SpawnPlayer()
    {
        //give room to spawn or spawn position
    }


    public void ExhibitEntered()
    {
        //called by event
    }

    public void ExhibitExited()
    {
        //called by event
    }

    public void GameOver()
    {
        //SI_CameraController.Instance.animationsRunning = true;
        // SI_AudioManager.Instance.PlayTheme(Civilizations.None);
        //UIManager.Instance.GameOver(player);
    }


    public void OnDataReady()
    {

    }


    public void AddScore(int playerIndex, int scoreType, int amount)
    {
        //GetPlayerByIndex(playerIndex).AddScore(scoreType, amount);
    }


    public void LoadGame()
    {
        //ItemManager.Instance.InitializeItems();
    }

    public void NewGame()
    {
        //ItemManager.Instance.InitializeItems();
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

}



