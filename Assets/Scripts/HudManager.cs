using UnityEngine;

public class HudManager : MonoBehaviour
{
    public GameObject GameButtons;
    public GameObject MainMenu;
    public SettingsMenu Settings;
    public GameObject CreditsMenu;

    public LevelManager LevelManager;

    public enum MenuState
    {
        MAIN,
        SETTINGS,
        CREDITS,
    }

    private MenuState State = MenuState.MAIN;

    void Start()
    {
        EventSystem.OnLevelChange += LevelChanged;
        Settings.RequestClose += RequestClose;
        LevelChanged(this, LevelManager.CurrentLevel);
    }

    private void OnDestroy()
    {
        EventSystem.OnLevelChange -= LevelChanged;
        Settings.RequestClose -= RequestClose;
    }

    private void LevelChanged(object sender, Level level)
    {
        GameButtons.SetActive(level != null);
        CloseOthers();
    }

    public void CloseOthers()
    {
        RequestClose(this, new System.EventArgs());
    }

    private void RequestClose(object sender, System.EventArgs e)
    {
        State = MenuState.MAIN;
        UpdateMenus();
    }

    private void UpdateMenus()
    {
        MainMenu.SetActive(LevelManager.CurrentLevel == null && State == MenuState.MAIN);
        CreditsMenu.SetActive(LevelManager.CurrentLevel == null && State == MenuState.CREDITS);
        Settings.gameObject.SetActive(LevelManager.CurrentLevel == null && State == MenuState.SETTINGS);
    }

    public void OpenSettings()
    {
        State = MenuState.SETTINGS;
        UpdateMenus();
    }

    public void OpenCredits()
    {
        State = MenuState.CREDITS;
        UpdateMenus();
    }

    public void OnPlay()
    {
        LevelManager.SetLevel(1);
    }

    public void OnContinue()
    {
        LevelManager.SetLevel(GameLogic.Instance.Save.NextLevel);
    }
}
