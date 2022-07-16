using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DiceFilter : MonoBehaviour, IClickable
{
    public Material Color0 = null;
    public Material Color1 = null;
    public Material Color2 = null;
    public Material Color3 = null;
    public Material Color4 = null;
    public Material Color5 = null;
    public Material Color6 = null;


    public Dice.DiceNumber DefaultNumber = Dice.DiceNumber.ONE;
    private Dice.DiceNumber? number = Dice.DiceNumber.ONE;
    private Collider Collider;
    public Dice.DiceNumber? Number
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
            UpdateCollisions();
            UpdateColors();
        }
    }

    public void UpdateColors()
    {
        if (Number.HasValue)
            GetComponent<Renderer>().material = Number.Value switch
            {
                Dice.DiceNumber.ONE => Color1,
                Dice.DiceNumber.TWO => Color2,
                Dice.DiceNumber.THREE => Color3,
                Dice.DiceNumber.FOUR => Color4,
                Dice.DiceNumber.FIVE => Color5,
                Dice.DiceNumber.SIX => Color6,
                _ => Color0,
            };
        else
            GetComponent<Renderer>().material = Color0;
    }

    public void UpdateCollisions()
    {
        Dice[] dice = FindObjectsOfType<Dice>();
        foreach (Dice d in dice)
            UpdateCollision(d);
    }

    public void UpdateCollision(Dice dice)
    {
        Physics.IgnoreCollision(GetCollider(), dice.GetCollider(), Number.HasValue && dice.Number == Number.Value);
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
        Number = DefaultNumber;
    }

    public void Click()
    {
        Number = (Dice.DiceNumber)(((byte)Number % 6) + 1);
        EventSystem.DiceFilterChange(this);
    }
}
