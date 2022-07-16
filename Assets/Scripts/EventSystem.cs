using System;
using UnityEngine;

public class EventSystem
{
    public static event EventHandler<Tuple<string, object>> OnEvent;
    public static event EventHandler<Tuple<string, float>> OnMessage;

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
        OnEvent?.Invoke(source, new Tuple<string, object>(name, variables));
    }

    public static void DiceSpawned(DiceSpawner spawner, Dice.DiceNumber number)
    {
        OnDiceSpawned?.Invoke(spawner, new Tuple<Dice.DiceNumber, int>(number, spawner.Remaining));
        LaunchEvent(spawner, "dice.spawned", new Tuple<Dice.DiceNumber, int>(number, spawner.Remaining));
    }

    public static void DiceCreated(Dice dice)
    {
        OnDiceCreate?.Invoke(dice, dice);
        LaunchEvent(dice, "dice.created", dice);
    }

    public static void DiceDestroyed(Dice dice)
    {
        OnDiceDestroy?.Invoke(dice, dice);
        LaunchEvent(dice, "dice.destroyed", dice);
    }

    public static void SceneReset(object source)
    {
        OnReset?.Invoke(source, new EventArgs());
        LaunchEvent(source, "scene.reset", new EventArgs());
    }

    public static void Pause(object source)
    {
        OnPause?.Invoke(source, true);
        LaunchEvent(source, "scene.pause", true);
    }

    public static void Start(object source)
    {
        OnStart?.Invoke(source, false);
        LaunchEvent(source, "scene.start", false);
    }

    public static void ScoreChanged(DiceContainer container)
    {
        OnScoreChange?.Invoke(container, container.Target - container.Score);
        LaunchEvent(container, "score.changed", container.Target - container.Score);
    }

    public static void LevelChanged(LevelManager manager)
    {
        OnLevelChange?.Invoke(manager, manager.CurrentLevel);
        LaunchEvent(manager, "scene.changed", manager.CurrentLevel);
    }

    public static void DiceFilterChange(DiceFilter filter)
    {
        OnDiceFilterChanged?.Invoke(filter, filter.Number);
        LaunchEvent(filter, "filter.changed", filter.Number);
    }

    public static void Finished(object source, bool win)
    {
        OnFinished?.Invoke(source, win);
        LaunchEvent(source, "scene.finished", win);
    }
}
