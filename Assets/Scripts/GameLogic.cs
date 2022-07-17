using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : HasAudio
{
    public static bool Active { get => Instance != null && !Instance.SettingsPanel.activeInHierarchy; }
    public static bool Paused { get => Instance == null || Instance.Pause; }

    public static GameLogic Instance = null;

    public static float Volume
    {
        get
        {
            return Instance ? Instance.Save.Volume : 1;
        }
        set
        {
            if (!Instance)
                return;
            Instance.Save.Volume = value;
            Instance.Save.SaveDisk();
            EventSystem.VolumeChanged(Instance, value);
        }
    }


    public bool Pause = true;
    public Save Save;

    public bool NextReset = false;

    public Settings Settings;
    public AudioClip ClickClip = null;
    public AudioClip LostClip = null;
    public AudioClip WinClip = null;
    public GameObject SettingsPanel;
    public LevelManager LevelManager;


    // Start is called before the first frame update
    override protected void Start()
    {         
        Save = Save.Load();
        Instance = this;
        base.Start();
        Restart();
        EventSystem.OnFinished += OnFinished;
    }

    private void OnFinished(object sender, bool e)
    {
        if (e)
            PlayOneShot(WinClip);
        else
            PlayOneShot(LostClip);
    }

    public void PlayScene()
    {
        if (SettingsPanel.activeInHierarchy)
            return;
        if (NextReset)
            Restart();
        EventSystem.Start(this);
        Pause = false;
        Time.timeScale = 1;
    }

    public void PauseScene()
    {
        EventSystem.Pause(this);
        Pause = true;
        Time.timeScale = 0;
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
            PlayOneShot(ClickClip);
    }


    public void MainMenu()
    {
        LevelManager.SetLevel(0);
    }


    public void Restart()
    {
        if (SettingsPanel.activeInHierarchy)
            return;
        NextReset = false;
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
