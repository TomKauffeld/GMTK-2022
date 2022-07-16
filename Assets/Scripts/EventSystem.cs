using System;

public class EventSystem
{
    public static event EventHandler<Tuple<string, float>> OnMessage;

    public static event EventHandler<Dice> OnDiceCreate;
    public static event EventHandler<Dice> OnDiceDestroy;

    public static event EventHandler<int> OnScoreChange;
    public static event EventHandler OnReset;
    public static event EventHandler<bool> OnPause;
    public static event EventHandler<bool> OnStart;
    public static event EventHandler<Level> OnLevelChange;

    public static event EventHandler<Tuple<Dice.DiceNumber, int>> OnDiceSpawned;

    public static event EventHandler<Dice.DiceNumber?> OnDiceFilterChanged;


    public static void DiceCreated(Dice dice)
    {
        OnDiceCreate?.Invoke(dice, dice);
    }

    public static void DiceDestroyed(Dice dice)
    {
        OnDiceDestroy?.Invoke(dice, dice);
    }

    public static void SceneReset(object source)
    {
        OnReset?.Invoke(source, new EventArgs());
    }

    public static void DiceSpawned(DiceSpawner spawner, Dice.DiceNumber number)
    {
        OnDiceSpawned?.Invoke(spawner, new Tuple<Dice.DiceNumber, int>(number, spawner.Remaining));
    }

    public static void Pause(object source)
    {
        OnPause?.Invoke(source, true);
    }

    public static void Start(object source)
    {
        OnStart?.Invoke(source, false);
    }

    public static void ScoreChanged(DiceContainer container)
    {
        OnScoreChange?.Invoke(container, container.Target - container.Score);
    }

    public static void LevelChanged(LevelManager manager)
    {
        OnLevelChange?.Invoke(manager, manager.CurrentLevel);
    }

    public static void DiceFilterChange(DiceFilter filter)
    {
        OnDiceFilterChanged?.Invoke(filter, filter.Number);
    }
}
