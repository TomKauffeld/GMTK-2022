using TMPro;
using UnityEngine;

public class DiceContainer : HasAudio
{
    public TextMeshPro text;
    public int Target = 0;
    public int Score = 0;
    public Color Success = Color.green;
    public Color Failure = Color.red;
    public Color Pending = Color.black;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        EventSystem.OnReset += OnReset;
    }

    private void OnReset(object sender, System.EventArgs e)
    {
        Score = 0;
        EventSystem.ScoreChanged(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (text)
        {
            text.text = string.Format("{0} / {1}", Score, Target);
            text.color = Score == Target ? Success : (Score > Target ? Failure : Pending);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Dice dice))
        {
            Score += (byte)dice.Number;
            Destroy(other.gameObject);
            EventSystem.ScoreChanged(this);
            Play();
        }
    }
}
