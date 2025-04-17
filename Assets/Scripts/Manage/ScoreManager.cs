using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : NetworkBehaviour
{
    //Chống trôi biến
    public static ScoreManager Instance { get; private set; }

    [Networked] public int scoreBanana { get; set; }
    [Networked] public int scoreApple { get; set; }
    [Networked] public int scoreMelon { get; set; }
    [Networked] public int scoreCherries { get; set; }
    [Networked] public int scoreKiwi { get; set; }

    [Header("UI")]
    public TextMeshProUGUI bananaText;
    public TextMeshProUGUI appleText;
    public TextMeshProUGUI melonText;
    public TextMeshProUGUI cherriesText;
    public TextMeshProUGUI kiwiText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            LoadScoresFromPrefs();
        }

        UpdateScoreUI();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RpcAddScore(FruitType type)
    {
        switch (type)
        {
            case FruitType.Banana: scoreBanana++; SaveScoreToPrefs(type, scoreBanana); break;
            case FruitType.Apple: scoreApple++; SaveScoreToPrefs(type, scoreApple); break;
            case FruitType.Melon: scoreMelon++; SaveScoreToPrefs(type, scoreMelon); break;
            case FruitType.Cherries: scoreCherries++; SaveScoreToPrefs(type, scoreCherries); break;
            case FruitType.Kiwi: scoreKiwi++; SaveScoreToPrefs(type, scoreKiwi); break;
        }

        UpdateScoreUI();
    }

    public void ResetScores()
    {
        scoreBanana = 0;
        scoreApple = 0;
        scoreMelon = 0;
        scoreCherries = 0;
        scoreKiwi = 0;

        DeleteAllPrefs();
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        bananaText.text = scoreBanana.ToString();
        appleText.text = scoreApple.ToString();
        melonText.text = scoreMelon.ToString();
        cherriesText.text = scoreCherries.ToString();
        kiwiText.text = scoreKiwi.ToString();
    }

    private void SaveScoreToPrefs(FruitType type, int value)
    {
        PlayerPrefs.SetInt($"FruitScore_{type}", value);
        PlayerPrefs.Save();
    }

    private void LoadScoresFromPrefs()
    {
        scoreBanana = PlayerPrefs.GetInt("FruitScore_Banana", 0);
        scoreApple = PlayerPrefs.GetInt("FruitScore_Apple", 0);
        scoreMelon = PlayerPrefs.GetInt("FruitScore_Melon", 0);
        scoreCherries = PlayerPrefs.GetInt("FruitScore_Cherries", 0);
        scoreKiwi = PlayerPrefs.GetInt("FruitScore_Kiwi", 0);
    }

    private void DeleteAllPrefs()
    {
        PlayerPrefs.DeleteKey("FruitScore_Banana");
        PlayerPrefs.DeleteKey("FruitScore_Apple");
        PlayerPrefs.DeleteKey("FruitScore_Melon");
        PlayerPrefs.DeleteKey("FruitScore_Cherries");
        PlayerPrefs.DeleteKey("FruitScore_Kiwi");
        PlayerPrefs.Save();
    }
}
