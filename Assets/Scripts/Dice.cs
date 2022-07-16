using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Dice : MonoBehaviour
{
    public enum DiceNumber : byte
    {
        ONE = 1,
        TWO = 2,
        THREE = 3,
        FOUR = 4,
        FIVE = 5,
        SIX = 6,
    }

    private DiceNumber number = DiceNumber.ONE;
    private Collider Collider;

    public Transform dice;


    public DiceNumber Number
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
            UpdateCollisions();
            UpdateRotation();
        }
    }

    public void UpdateRotation()
    {
        switch (number)
        {
            case DiceNumber.ONE:
                dice.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case DiceNumber.TWO:
                dice.localRotation = Quaternion.Euler(-90, 0, 0);
                break;
            case DiceNumber.THREE:
                dice.localRotation = Quaternion.Euler(90, -90, 0);
                break;
            case DiceNumber.FOUR:
                dice.localRotation = Quaternion.Euler(0, 90, 0);
                break;
            case DiceNumber.FIVE:
                dice.localRotation = Quaternion.Euler(90, 0, 0);
                break;
            case DiceNumber.SIX:
                dice.localRotation = Quaternion.Euler(0, 180, 0);
                break;
        }
    }


    public void UpdateCollisions()
    {
        DiceFilter[] filters = FindObjectsOfType<DiceFilter>();
        foreach (DiceFilter filter in filters)
            filter.UpdateCollision(this);
    }
    public Collider GetCollider()
    {
        if (Collider == null)
            Collider = GetComponent<Collider>();
        return Collider;
    }


    // Start is called before the first frame update
    void Start()
    {
        EventSystem.OnReset += OnSceneReset;
        UpdateRotation();
        UpdateCollisions();
        EventSystem.DiceCreated(this);
    }

    private void Update()
    {
        if (transform.position.y < -200)
            Destroy(gameObject);
    }

    private void OnSceneReset(object source, EventArgs args)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventSystem.OnReset -= OnSceneReset;
        EventSystem.DiceDestroyed(this);
    }
}
