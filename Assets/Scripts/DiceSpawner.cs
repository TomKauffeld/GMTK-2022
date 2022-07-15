using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : MonoBehaviour
{
    public Dice Dice;
    public ushort Amount_1 = 0;
    public ushort Amount_2 = 0;
    public ushort Amount_3 = 0;
    public ushort Amount_4 = 0;
    public ushort Amount_5 = 0;
    public ushort Amount_6 = 0;

    public List<Collider> Colliders = new();

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn(ushort amount, Dice.DiceNumber number)
    {
        for (ushort i = 0; i < amount; ++i)
        {
            while (Colliders.Count > 0)
                yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(1);
            Dice dice = Instantiate(Dice);
            dice.number = number;
            dice.transform.parent = transform;
            dice.transform.localPosition = Vector3.zero;
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return Spawn(Amount_1, Dice.DiceNumber.ONE);
        yield return Spawn(Amount_2, Dice.DiceNumber.TWO);
        yield return Spawn(Amount_3, Dice.DiceNumber.THREE);
        yield return Spawn(Amount_4, Dice.DiceNumber.FOUR);
        yield return Spawn(Amount_5, Dice.DiceNumber.FIVE);
        yield return Spawn(Amount_6, Dice.DiceNumber.SIX);
    }

    public void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        Colliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        Colliders.Remove(other);
    }
}
