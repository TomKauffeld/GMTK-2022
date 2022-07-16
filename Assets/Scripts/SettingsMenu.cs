using System;
using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public bool Azerty = false;
    public event EventHandler RequestClose;


    public Settings Settings;
    public TextMeshProUGUI Up;
    public TextMeshProUGUI Down;
    public TextMeshProUGUI Left;
    public TextMeshProUGUI Right;
    public TextMeshProUGUI Pause;
    public TextMeshProUGUI Reset;
    public TextMeshProUGUI AzertyButton;

    // Start is called before the first frame update
    void Start()
    {
        if (GameLogic.Instance)
            Azerty = GameLogic.Instance.Save.Azerty;

        if (Azerty)
            Settings.LoadAzerty();
        else
            Settings.LoadQuerty();
    }


    // Update is called once per frame
    void Update()
    {
        if (Up)
            Up.text = Settings.Up.ToString();
        if (Down)
            Down.text = Settings.Down.ToString();
        if (Left)
            Left.text = Settings.Left.ToString();
        if (Right)
            Right.text = Settings.Right.ToString();
        if (Pause)
            Pause.text = Settings.Pause.ToString();
        if (Reset)
            Reset.text = Settings.Reset.ToString();
        if (AzertyButton)
            AzertyButton.text = Azerty ? "AZERTY" : "QWERTY";
    }

    public void Switch()
    {
        Azerty = !Azerty;
        if (Azerty)
            Settings.LoadAzerty();
        else
            Settings.LoadQuerty();

        if (GameLogic.Instance)
        {
            GameLogic.Instance.Save.Azerty = Azerty;
            GameLogic.Instance.Save.SaveDisk();
        }
    }

    public void Close()
    {
        RequestClose?.Invoke(this, new EventArgs());
    }
}
