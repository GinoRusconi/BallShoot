using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _current;
    public static GameManager Current
    #region Singleton
    {
        get
        {
            if (_current == null)
            {
                _current = GameObject.FindObjectOfType<GameManager>();
            }

            return _current;
        }
    }
    #endregion Singleton

    public HUD _HUD;
    public MenuPause _MenuPause;
    public PlayerCore _PlayerCore;
    public LevelSystem _LevelSystem;


    public void WinLevel(int currentLevel)
    {
        _HUD.LevelClearAnimation();
    }

    public void RestarExtraLife()
    {
        _PlayerCore.PlayerRestartExtraLife();
        _LevelSystem.ExtraLife();
    }


}