using System;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : HasAudio
{
    public DicePreview Preview;

    public Dice Dice;
    public ushort Amount_1 = 0;
    public ushort Amount_2 = 0;
    public ushort Amount_3 = 0;
    public ushort Amount_4 = 0;
    public ushort Amount_5 = 0;
    public ushort Amount_6 = 0;

    public int Remaining => spawnNumbers.Count;

    public List<Collider> Colliders = new();

    private readonly Queue<DicePreview> spawnNumbers = new();


    override protected void Start()
    {
        base.Start();
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
                Play();
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
        Dice.DiceNumber? number = GetNext();
        if (number.HasValue)
        {
            Dice dice = Instantiate(Dice);
            dice.Number = number.Value;
            dice.transform.parent = transform;
            dice.transform.localPosition = Vector3.zero;

            OnTriggerEnter(dice.GetCollider());
        }
        return number;
    }

    private Dice.DiceNumber? GetNext()
    {
        if (spawnNumbers.TryDequeue(out DicePreview preview))
        {
            Destroy(preview.gameObject);
            return preview.Number;
        }
        return null;
    }

    private void AddDicePreview(Dice.DiceNumber number)
    {
        DicePreview preview = Instantiate(Preview);
        preview.transform.parent = transform;
        preview.transform.localPosition = Vector3.zero;
        preview.Number = number;
        spawnNumbers.Enqueue(preview);
    }

    private void ClearDicePreview()
    {
        while (spawnNumbers.TryDequeue(out DicePreview preview))
            Destroy(preview.gameObject);
    }

    public void Spawn()
    {
        ClearDicePreview();
        for (ushort i = 0; i < Amount_1; ++i)
            AddDicePreview(Dice.DiceNumber.ONE);
        for (ushort i = 0; i < Amount_2; ++i)
            AddDicePreview(Dice.DiceNumber.TWO);
        for (ushort i = 0; i < Amount_3; ++i)
            AddDicePreview(Dice.DiceNumber.THREE);
        for (ushort i = 0; i < Amount_4; ++i)
            AddDicePreview(Dice.DiceNumber.FOUR);
        for (ushort i = 0; i < Amount_5; ++i)
            AddDicePreview(Dice.DiceNumber.FIVE);
        for (ushort i = 0; i < Amount_6; ++i)
            AddDicePreview(Dice.DiceNumber.SIX);

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
        DicePreview[] previews = spawnNumbers.ToArray();

        float y = 0;
        for (int i = 0; i < previews.Length; ++i)
        {
            Vector3 pos = previews[i].transform.localPosition;
            y += previews[i].transform.localScale.y;
            pos.y = y;
            previews[i].transform.localPosition = pos;
        }
    }

    override protected void OnDestroy()
    {
        base.OnDestroy();
        EventSystem.OnReset -= OnSceneReset;
    }
}
