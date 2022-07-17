using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    public Coroutine Coroutine = null;
    public bool wait = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>().value = GameLogic.Volume;
        GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);
    }

    private void OnEnable()
    {
        GetComponent<Slider>().value = GameLogic.Volume;
    }

    public void OnValueChanged(float value)
    {
        GameLogic.Volume = value;
        PlaySound();
    }

    private void PlaySound()
    {
        wait = true;
        if (Coroutine == null)
            Coroutine = StartCoroutine(PlaySingleSound());
    }

    private IEnumerator PlaySingleSound()
    {
        while (wait)
        {
            wait = false;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        GameLogic.Instance.SoundClick(true);
        yield return new WaitWhile(() => GameLogic.Instance.IsPlaying);
        Coroutine = null;
    }
}
