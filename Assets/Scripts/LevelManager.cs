using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Level> Levels = new();

    public int Level = 0;

    public Level CurrentLevel { get; private set; } = null;
    public int CurrentLevelIndex { get; private set; } = 0;

    void Start()
    {
        EventSystem.OnDiceDestroy += OnDiceDestroy;
        EventSystem.OnFinished += OnFinished;
        SetLevel(0);
    }

    private void OnFinished(object sender, bool win)
    {
        StartCoroutine(Finished(win));
    }

    private IEnumerator Finished(bool win)
    {
        if (win)
            yield return CurrentLevel.OnWinState();
        else
            yield return CurrentLevel.OnFailState();
        if (win && CurrentLevelIndex < Levels.Count)
            SetLevel(CurrentLevelIndex + 1);
        else if (win)
            SetLevel(0);
        yield return null;
    }

    private void OnDiceDestroy(object sender, Dice e)
    {
        if (FindObjectOfType<Dice>() != null)
            return;

        foreach (DiceSpawner spawner in FindObjectsOfType<DiceSpawner>())
            if (spawner.Remaining > 0)
                return;

        bool win = true;

        foreach (DiceContainer container in FindObjectsOfType<DiceContainer>())
            win &= container.Score == container.Target;

        if (!win)
        {
            GameLogic.Instance.PauseScene();
            GameLogic.Instance.NextReset = true;
        }

        EventSystem.Finished(sender, win);
    }

    public void SetLevel(int level)
    {
        if (level >= 0 && level <= Levels.Count)
        {
            if (CurrentLevel != null)
                Destroy(CurrentLevel.gameObject);
            CurrentLevel = null;
            CurrentLevelIndex = level;

            if (CurrentLevelIndex > 0)
            {
                CurrentLevel = Instantiate(Levels[CurrentLevelIndex - 1]);
                if (GameLogic.Instance)
                    GameLogic.Instance.Restart();
            }
            if (GameLogic.Instance)
            {
                GameLogic.Instance.Save.NextLevel = Mathf.Max(GameLogic.Instance.Save.NextLevel, level);
                GameLogic.Instance.Save.SaveDisk();
            }
            EventSystem.LevelChanged(this);
        }
        Level = CurrentLevelIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (Level != CurrentLevelIndex)
            SetLevel(Level);
    }
}
