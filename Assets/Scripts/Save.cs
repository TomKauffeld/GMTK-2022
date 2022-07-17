using UnityEngine;

public struct Save
{

    public bool Azerty;

    public int NextLevel;

    public float Volume;


    public static Save Load()
    {
        return new()
        {
            Azerty = PlayerPrefs.GetInt("settings.azerty", 0) != 0,
            NextLevel = PlayerPrefs.GetInt("save.level", 0),
            Volume = PlayerPrefs.GetFloat("settings.volume", 1),
        };
    }

    public void SaveDisk()
    {
        PlayerPrefs.SetInt("settings.azerty", Azerty ? 1 : 0);
        PlayerPrefs.SetInt("save.level", NextLevel);
        PlayerPrefs.SetFloat("settings.volume", Volume);
        PlayerPrefs.Save();
    }
}
