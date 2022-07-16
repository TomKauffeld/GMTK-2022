using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceSpawner : MonoBehaviour
{
    public TextMeshPro Display;

    public Dice Dice;
    public ushort Amount_1 = 0;
    public ushort Amount_2 = 0;
    public ushort Amount_3 = 0;
    public ushort Amount_4 = 0;
    public ushort Amount_5 = 0;
    public ushort Amount_6 = 0;

    public int Remaining
    {
        get
        {
            int sum = 0;
            foreach (KeyValuePair<Dice.DiceNumber, ushort> kv in spawnAmount)
                sum += kv.Value;
            return sum;
        }
    }

    public List<Collider> Colliders = new();

    private readonly Dictionary<Dice.DiceNumber, ushort> spawnAmount = new();

    private readonly string format = "Dice 1: {0}\nDice 2: {1}\nDice 3: {2}\nDice 4: {3}\nDice 5: {4}\nDice 6: {5}";

    void Start()
    {
        Spawn();
        EventSystem.OnReset += OnSceneReset;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameLogic.Paused && !HasCollision())
        {
            Dice.DiceNumber? number = SpawnFirst();
            if (number.HasValue)
            {
                SetDisplay();
                EventSystem.DiceSpawned(this, number.Value);
            }
        }
    }

    public void OnSceneReset(object source, EventArgs args)
    {
        Spawn();
    }

    private bool HasCollision()
    {
        if (Colliders.Count < 1)
            return false;
        bool found = false;
        for(int i = Colliders.Count - 1; i >= 0; i--)
        {
            if (!!Colliders[i])
                found = true;
            else
                Colliders.RemoveAt(i);
        }
        return found;
    }

    private Dice.DiceNumber? SpawnFirst()
    {
        foreach(KeyValuePair<Dice.DiceNumber, ushort> v in spawnAmount)
        {
            if (v.Value > 0)
            {
                Dice dice = Instantiate(Dice);
                dice.Number = v.Key;
                dice.transform.parent = transform;
                dice.transform.localPosition = Vector3.zero;
                spawnAmount[v.Key]--;
                OnTriggerEnter(dice.GetCollider());
                return v.Key;
            }
        }
        return null;
    }

    public void Spawn()
    {
        spawnAmount[Dice.DiceNumber.ONE] = Amount_1;
        spawnAmount[Dice.DiceNumber.TWO] = Amount_2;
        spawnAmount[Dice.DiceNumber.THREE] = Amount_3;
        spawnAmount[Dice.DiceNumber.FOUR] = Amount_4;
        spawnAmount[Dice.DiceNumber.FIVE] = Amount_5;
        spawnAmount[Dice.DiceNumber.SIX] = Amount_6;
        SetDisplay();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Colliders.Contains(other))
            Colliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        Colliders.Remove(other);
    }

    private void SetDisplay()
    {
        if (!Display)
            return;
        Display.text = string.Format(format,
            spawnAmount[Dice.DiceNumber.ONE],
            spawnAmount[Dice.DiceNumber.TWO],
            spawnAmount[Dice.DiceNumber.THREE],
            spawnAmount[Dice.DiceNumber.FOUR],
            spawnAmount[Dice.DiceNumber.FIVE],
            spawnAmount[Dice.DiceNumber.SIX]
        );
    }

    private void OnDestroy()
    {
        EventSystem.OnReset -= OnSceneReset;
    }
}
