using UnityEngine;
using Fusion;
using TMPro;

public class ScoreManager : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Networked, OnChangedRender(nameof(OnChangeScore))]
    private int scoreApple { get; set; }
    [Networked, OnChangedRender(nameof(OnChangeScore))]
    private int scoreBanana { get; set; }
    [Networked, OnChangedRender(nameof(OnChangeScore))]
    private int scoreMelon { get; set; }
    [Networked, OnChangedRender(nameof(OnChangeScore))]
    private int scoreCherries { get; set; }
    [Networked, OnChangedRender(nameof(OnChangeScore))]
    private int scoreKiwi { get; set; }

    public TextMeshProUGUI appleText;
    public TextMeshProUGUI bananaText;
    public TextMeshProUGUI cherriesText;
    public TextMeshProUGUI kiwiText;
    public TextMeshProUGUI melonText;

    public override void Spawned()
    {
        OriginalScore();
        OnChangeScore();
    }
    public void OriginalScore()
    {
        scoreApple = 0;
        scoreBanana = 0;
        scoreMelon = 0;
        scoreCherries = 0;
        scoreKiwi = 0;
        scoreBanana = 0;
    }
    public void OnChangeScore()
    {
        appleText.text = "" + scoreApple;

        bananaText.text = "" + scoreBanana;

        kiwiText.text = "" + scoreKiwi;

        cherriesText.text = "" + scoreCherries;

        melonText.text = "" + scoreMelon;
    }
    public int ScoreApple()
    {
        return scoreApple++;
    }
    public int ScoreBanana()
    {
        return scoreBanana++;
    }
    public int ScoreCherries()
    {
        return scoreCherries++;
    }
    public int ScoreMelon()
    {
        return scoreMelon++;
    }
    public int ScoreKiwi()
    {
        return scoreKiwi++;
    }
}
