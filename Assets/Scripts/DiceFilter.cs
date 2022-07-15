using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DiceFilter : MonoBehaviour
{
    public Dice.DiceNumber number = Dice.DiceNumber.ONE;
    public Collider Collider;

    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Dice[] dice = FindObjectsOfType<Dice>();
        foreach(Dice d in dice)
        {
            Physics.IgnoreCollision(Collider, d.Collider, d.number == number);
        }
    }
}
