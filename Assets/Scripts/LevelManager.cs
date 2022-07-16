using System;
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
        SetLevel(0);
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
