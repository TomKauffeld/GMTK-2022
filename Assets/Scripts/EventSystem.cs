using System;
using UnityEngine;

public class EventSystem
{
    public static event EventHandler<Tuple<string, object>> OnEvent;
    public static event EventHandler<float> OnVolumeChange;

    public static event EventHandler<Dice> OnDiceCreate;
    public static event EventHandler<Dice> OnDiceDestroy;

    public static event EventHandler<int> OnScoreChange;
    public static event EventHandler OnReset;
    public static event EventHandler<bool> OnPause;
    public static event EventHandler<bool> OnStart;
    public static event EventHandler<Level> OnLevelChange;
    public static event EventHandler<bool> OnFinished;

    public static event EventHandler<Tuple<Dice.DiceNumber, int>> OnDiceSpawned;

    public static event EventHandler<Dice.DiceNumber?> OnDiceFilterChanged;

    public static void LaunchEvent(object source, string name, object variables)
    {
        Debug.Log(name);
        OnEvent?.Invoke(source, new Tuple<string, object>(name, variables));
    }

    public static void DiceSpawned(DiceSpawner spawner, Dice.DiceNumber number)
    {
        LaunchEvent(spawner, "dice.spawned", new Tuple<Dice.DiceNumber, int>(number, spawner.Remaining));
        OnDiceSpawned?.Invoke(spawner, new Tuple<Dice.DiceNumber, int>(number, spawner.Remaining));
    }

    public static void DiceCreated(Dice dice)
    {
        LaunchEvent(dice, "dice.created", dice);
        OnDiceCreate?.Invoke(dice, dice);
    }

    public static void DiceDestroyed(Dice dice)
    {
        LaunchEvent(dice, "dice.destroyed", dice);
        OnDiceDestroy?.Invoke(dice, dice);
    }

    public static void SceneReset(object source)
    {
        LaunchEvent(source, "scene.reset", new EventArgs());
        OnReset?.Invoke(source, new EventArgs());
    }

    public static void Pause(object source)
    {
        LaunchEvent(source, "scene.pause", true);
        OnPause?.Invoke(source, true);
    }

    public static void Start(object source)
    {
        LaunchEvent(source, "scene.start", false);
        OnStart?.Invoke(source, false);
    }

    public static void ScoreChanged(DiceContainer container)
    {
        LaunchEvent(container, "score.changed", container.Target - container.Score);
        OnScoreChange?.Invoke(container, container.Target - container.Score);
    }

    public static void LevelChanged(LevelManager manager)
    {
        LaunchEvent(manager, "scene.changed", manager.CurrentLevel);
        OnLevelChange?.Invoke(manager, manager.CurrentLevel);
    }

    public static void DiceFilterChange(DiceFilter filter)
    {
        LaunchEvent(filter, "filter.changed", filter.Number);
        OnDiceFilterChanged?.Invoke(filter, filter.Number);
    }

    public static void Finished(object source, bool win)
    {
        LaunchEvent(source, "scene.finished", win);
        OnFinished?.Invoke(source, win);
    }

    public static void VolumeChanged(object source, float volume)
    {
        LaunchEvent(source, "audio.volume", volume);
        OnVolumeChange?.Invoke(source, volume);
    }
}
