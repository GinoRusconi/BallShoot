using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Score;
    public Animator _levelClear;

    // Start is called before the first frame update
    public void LevelClearAnimation()
    {
        _levelClear.SetTrigger("LevelClear");
    }

    public void NextLevel()
    {
        LevelSystem.Current.CurrentLevel();
    }

    public void ScoreUp(int scorePoint)
    {
        scorePoint = Mathf.Clamp(scorePoint, 1, 10);
        int myIntScore;
        int.TryParse(_Score.text, out myIntScore);
        myIntScore += scorePoint;
        _Score.text = myIntScore.ToString();
    }
}