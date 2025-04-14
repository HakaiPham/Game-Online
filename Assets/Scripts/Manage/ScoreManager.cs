using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : NetworkBehaviour
{
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
            ResetScores();
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

        UpdateScoreUI();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RpcAddScore(FruitType type)
    {
        switch (type)
        {
            case FruitType.Banana: scoreBanana++; break;
            case FruitType.Apple: scoreApple++; break;
            case FruitType.Melon: scoreMelon++; break;
            case FruitType.Cherries: scoreCherries++; break;
            case FruitType.Kiwi: scoreKiwi++; break;
        }

        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        bananaText.text = scoreBanana.ToString();
        appleText.text = scoreApple.ToString();
        melonText.text = scoreMelon.ToString();
        cherriesText.text = scoreCherries.ToString();
        kiwiText.text = scoreKiwi.ToString();
    }
}
