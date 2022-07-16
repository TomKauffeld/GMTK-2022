using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }

    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Up = KeyCode.W;
    public KeyCode Down = KeyCode.S;
    public KeyCode Pause = KeyCode.Space;
    public KeyCode Reset = KeyCode.R;


    private void Start()
    {
        Instance = this;
    }


    public void LoadQuerty()
    {
        Left = KeyCode.A;
        Right = KeyCode.D;
        Up = KeyCode.W;
        Down = KeyCode.S;
        Pause = KeyCode.Space;
        Reset = KeyCode.R;
    }


    public void LoadAzerty()
    {
        Left = KeyCode.Q;
        Right = KeyCode.D;
        Up = KeyCode.Z;
        Down = KeyCode.S;
        Pause = KeyCode.Space;
        Reset = KeyCode.R;
    }
}
