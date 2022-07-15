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

    public DiceNumber number = DiceNumber.ONE;
    public Collider Collider;

    public Transform dice;

    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(number)
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
}
