using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DicePreview : MonoBehaviour
{
    public Material Value_1;
    public Material Value_2;
    public Material Value_3;
    public Material Value_4;
    public Material Value_5;
    public Material Value_6;
    private Dice.DiceNumber number = Dice.DiceNumber.ONE;


    public Dice.DiceNumber Number
    {
        get
        {
            return number;
        }
        set
        {
            number = value;
            UpdateTexture();
        }
    }

    private Renderer Renderer;



    // Start is called before the first frame update
    void Start()
    {
        Renderer = GetComponent<Renderer>();
        UpdateTexture();
    }

    private void UpdateTexture()
    {
        if (!Renderer)
            return;
        switch(Number)
        {
            case Dice.DiceNumber.ONE:
                Renderer.material = Value_1;
                break;
            case Dice.DiceNumber.TWO:
                Renderer.material = Value_2;
                break;
            case Dice.DiceNumber.THREE:
                Renderer.material = Value_3;
                break;
            case Dice.DiceNumber.FOUR:
                Renderer.material = Value_4;
                break;
            case Dice.DiceNumber.FIVE:
                Renderer.material = Value_5;
                break;
            case Dice.DiceNumber.SIX:
                Renderer.material = Value_6;
                break;
        }
    }
}
