using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameLogic : MonoBehaviour
{
    public static bool Active { get => Instance != null && !Instance.SettingsPanel.activeInHierarchy; }
    public static bool Paused { get => Instance == null || Instance.Pause; }

    public static GameLogic Instance = null;
   


    public bool Pause = true;
    public Save Save;

    public Settings Settings;
    public AudioClip ClickClip = null;
    public GameObject SettingsPanel;
    private AudioSource AudioSource;
    public LevelManager LevelManager;


    // Start is called before the first frame update
    void Start()
    {
        Save = Save.Load();
        AudioSource = GetComponent<AudioSource>();
        Instance = this;
        Restart();
    }

    public void PlayScene()
    {
        if (SettingsPanel.activeInHierarchy)
            return;
        Pause = false;
        Time.timeScale = 1;
        EventSystem.Start(this);
    }

    public void PauseScene()
    {
        Pause = true;
        Time.timeScale = 0;
        EventSystem.Pause(this);
    }

    public void PlayPause()
    {
        if (Pause)
            PlayScene();
        else
            PauseScene();
    }

    public void OpenSettings()
    {
        if (SettingsPanel.activeInHierarchy)
            SettingsPanel.SetActive(false);
        else
            StartCoroutine(OpenSettingsInternal());
    }

    private IEnumerator OpenSettingsInternal()
    {
        bool pause = Pause;
        PauseScene();
        SettingsPanel.SetActive(true);
        yield return new WaitWhile(() => SettingsPanel.activeInHierarchy);
        if (!pause)
            PlayScene();
    }

    public void SoundClick(bool force = false)
    {
        if (ClickClip != null && (force || !SettingsPanel.activeInHierarchy))
            AudioSource.PlayOneShot(ClickClip);
    }


    public void MainMenu()
    {
        LevelManager.SetLevel(0);
    }


    public void Restart()
    {
        if (SettingsPanel.activeInHierarchy)
            return;
        PauseScene();
        EventSystem.SceneReset(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (SettingsPanel.activeInHierarchy)
            return;
        if (Input.GetKeyDown(Settings.Pause))
            PlayPause();
        if (Input.GetKeyDown(Settings.Reset))
            Restart();
    }
}
