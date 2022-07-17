using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonPlay : MonoBehaviour
{
    public Sprite Play;
    public Sprite Pause;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.OnPause += OnPauseChanged;
        EventSystem.OnStart += OnPauseChanged;
        GetComponent<Button>().image.sprite = GameLogic.Paused ? Play : Pause;
    }

    private void OnPauseChanged(object source, bool paused)
    {
        GetComponent<Button>().image.sprite = paused ? Play : Pause;
    }

    private void OnDestroy()
    {
        EventSystem.OnPause -= OnPauseChanged;
        EventSystem.OnStart -= OnPauseChanged;
    }
}
