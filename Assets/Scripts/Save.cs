using UnityEngine;

public struct Save
{

    public bool Azerty;

    public int NextLevel;


    public static Save Load()
    {
        return new()
        {
            Azerty = PlayerPrefs.GetInt("settings.azerty", 0) != 0,
            NextLevel = PlayerPrefs.GetInt("save.level", 0),
        };
    }

    public void SaveDisk()
    {
        PlayerPrefs.SetInt("settings.azerty", Azerty ? 1 : 0);
        PlayerPrefs.SetInt("save.level", NextLevel);
        PlayerPrefs.Save();
    }
}
