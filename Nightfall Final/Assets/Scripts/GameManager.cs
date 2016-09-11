using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {

    private const int NONLEVELSCENECOUNT = 2;
    private const int DEFAULTSAVE = 0;
    private const bool DEFAULTPERMADEATH = false;
    private const float DEFAULTDARKNESS = 1F;
    private const float DEFAULTVOLUME = 1F;

    [HideInInspector]
    public GameManager gameManager;
    public PlayerController playerController;
    public bool canPause;

    private float timeScale = 1;
    private float menuBGMTime = 0.0F;
    private int saveStatus = 0;
    private bool permadeath;
    private float batteryLife;
    private bool batteryDied;
    private bool firstDeath;
    private float darkness;
    private float volume;

    void Awake() {
        if (gameManager == null) {
            DontDestroyOnLoad(gameObject);
            gameManager = this;
        } else if (gameManager != this) {
            //Destroy(gameObject);
            Destroy(gameManager);
            DontDestroyOnLoad(gameObject);
            gameManager = this;
        }
        Load();
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/NightfallSaveData.dat");
        GameData data = new GameData();

        data.menuBGMTime = menuBGMTime;
        data.saveStatus = saveStatus;
        data.permadeath = permadeath;
        data.batteryLife = batteryLife;
        data.shakeStart = batteryDied;
        data.firstDeath = firstDeath;
        data.darkness = darkness;
        data.volume = volume;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load() {
        if (File.Exists(Application.persistentDataPath + "/NightfallSaveData.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/NightfallSaveData.dat", FileMode.Open);
            GameData data = (GameData) bf.Deserialize(file);
            file.Close();

            menuBGMTime = data.menuBGMTime;
            saveStatus = data.saveStatus;
            permadeath = data.permadeath;
            batteryLife = data.batteryLife;
            batteryDied = data.shakeStart;
            firstDeath = data.firstDeath;
            darkness = data.darkness;
            volume = data.volume;
        } else {
            menuBGMTime = 0.0F;
            saveStatus = DEFAULTSAVE;
            permadeath = DEFAULTPERMADEATH;
            batteryLife = 1.0F;
            batteryDied = false;
            firstDeath = false;
            darkness = DEFAULTDARKNESS;
            volume = DEFAULTVOLUME;
            Save();
        }
    }

    void Start() {
        Time.timeScale = timeScale;
    }

    void Update() {
        
    }

    public void IncSave() {
        saveStatus++;
        Save();
    }

    public void WipeSave() {
        saveStatus = 0;
        menuBGMTime = 0;
        Save();
    }

    public int GetSave() {
        return saveStatus;
    }

    public void NewGame() {
        saveStatus = 0;
        batteryLife = 1.0F;
        batteryDied = false;
        firstDeath = false;
        menuBGMTime = 0;
        Save();
        StartGame();
    }

    public void Continue() {
        if (saveStatus > 0) {
            StartGame();
        }
    }

    void StartGame() {
        //load stage #
        UnityEngine.SceneManagement.SceneManager.LoadScene(saveStatus + NONLEVELSCENECOUNT);
    }

    public void Settings() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void InitPlayerController(PlayerController playerController) {
        this.playerController = playerController;
    }

    public void SetPaused() {
        playerController.isDead = true;
        Time.timeScale = 0.0F;
    }

    public void SetUnpaused() {
        playerController.isDead = false;
        Time.timeScale = timeScale;
        playerController.Unpaused();
    }

    public float TimeIn {
        get {
            return menuBGMTime;
        }
        set {
            menuBGMTime = value;
            Save();
        }
    }

    public bool Permadeath {
        get {
            return permadeath;
        }
        set {
            permadeath = value;
            Save();
        }
    }

    public float BatteryLife {
        get {
            return batteryLife;
        }
        set {
            batteryLife = value;
            Save();
        }
    }

    public bool BatteryDied {
        get {
            return batteryDied;
        }
        set {
            batteryDied = value;
            Save();
        }
    }

    public bool FirstDeath {
        get {
            return firstDeath;
        }
        set {
            firstDeath = value;
            Save();
        }
    }

    public float Darkness {
        get {
            return darkness;
        }
        set {
            darkness = value;
            Save();
        }
    }

    public float Volume {
        get {
            return volume;
        }
        set {
            volume = value;
            Save();
        }
    }

    //Platformer

    public void RestartLevel() {
        if (permadeath) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu");
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu");
    }

    public void LevelSelect() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_Select");
    }

    public void PlayLevel(int level) {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_" + level);
    }

    public void Controls() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Controls");
    }

    public void MainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu");
    }

    public void ExitGame() {
        Application.Quit();
    }

}

[Serializable]
class GameData {

    public float menuBGMTime = 0.0F;
    public int saveStatus = 0;
    public bool permadeath;
    public float batteryLife;
    public bool shakeStart;
    public bool firstDeath;
    public float darkness;
    public float sound;
    public float volume;

}