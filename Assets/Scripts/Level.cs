using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator OnWinState()
    {
        yield return new WaitForSecondsRealtime(1f);
        yield return null;
    }

    public IEnumerator OnFailState()
    {
        yield return null;
    }
}
