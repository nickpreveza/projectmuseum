using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Data")]
    public GameData data;
    public List<GameExhibitData> gameExhibitsData = new List<GameExhibitData>();
    public Dictionary<string, GameExhibitData> games = new Dictionary<string, GameExhibitData>();
    
    [Header("Systems Status")]
    public bool gameReady;

    [Header("Game Status")]
    public bool isPaused;
    public bool itemInspected;
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

    public GameObject activePlayer;
    [SerializeField] GameObject itemInspectSpawnTransform;
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

        for(int i = 0; i < gameExhibitsData.Count; i++)
        {
            games.Add(gameExhibitsData[i].key, gameExhibitsData[i]);
        }
    }

    public GameExhibitData TryFindGameWithKey(string key)
    {
        if (games.ContainsKey(key))
        {
            return games[key];
        }
        else
        {
            return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (itemInspected)
            {
                SetItemInspectState(false);
               
                
                return;
               //UIManager.Instance.ClosePopup();
               // UIManager.Instance.CloseTextInspect();
               //SetItemInspectState(false);
               //vp_Utility.LockCursor = true;
               // UIManager.Instance.popupActive = false;
               // return;
            }

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

    public void SetItemInspectState(bool show, GameObject itemToSpawn = null)
    {
        itemInspectSpawnTransform.SetActive(false);
        itemInspectSpawnTransform.SetActive(true);

        if (itemInspectSpawnTransform.transform.childCount > 0)
        {
            Destroy(itemInspectSpawnTransform.transform.GetChild(0).gameObject);
        }

        if (show && itemToSpawn != null)
        {
            GameObject newItem = Instantiate(itemToSpawn, itemInspectSpawnTransform.transform.position, Quaternion.identity);
            newItem.layer = 31;
            if (newItem.GetComponent<Interactable>() != null)
            {
                newItem.GetComponent<Interactable>().isInteractable = false;
            }
            newItem.transform.SetParent(itemInspectSpawnTransform.transform);
        }

        if (!show)
        {
            PlayerInteraction playerInteraction = activePlayer.GetComponent<PlayerInteraction>();

            if (playerInteraction.itemInHands != null)
            {
                playerInteraction.itemInHands.GetComponent<MeshRenderer>().enabled = true;
            }

            playerInteraction.ForceDrop();
            
        }

        itemInspectSpawnTransform.SetActive(show);
        itemInspected = show;
        SetDOF(show);
        UIManager.Instance.SetCursorVisibility(!show);

        if (UIManager.Instance.popupActive)
        {
            UIManager.Instance.CloseTextInspect();
        }
    }
    
    public void SetDOF(bool dofState)
    {
        DepthOfField dof;
        if (globalVolume.profile.TryGet<DepthOfField>(out dof))
        {
            dof.active = dofState;
        }
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

#if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }

}

[System.Serializable]
public class GameExhibitData
{
    public string key;

    public Sprite coverImage;
    public Sprite gameScreenshot;

    public string name;
    public string developer;
    public string publisher;
    public string releaseDate;
    public string description;

    public Color backgroundColor;
    public bool textOnColorIsDark;

    public bool hasBeenVisited;
    public bool hasBeenCompleted;
    
    //list for publications 

}



