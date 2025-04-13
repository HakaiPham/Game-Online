using UnityEngine;
using Fusion;
using TMPro;

public class ScoreManager : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(IncreateScore))]
    [SerializeField] private int soluongMelon { get; set; }
    [Networked, OnChangedRender(nameof(IncreateScore))]
    [SerializeField] private int soluongApple { get; set; }
    [Networked, OnChangedRender(nameof(IncreateScore))]
    [SerializeField] private int soluongBanana { get; set; }
    [Networked, OnChangedRender(nameof(IncreateScore))]
    [SerializeField] private int soluongKiwi { get; set; }
    [Networked, OnChangedRender(nameof(IncreateScore))]
    [SerializeField] private int soluongCherries { get; set; }


    public TextMeshProUGUI appleText;
    public TextMeshProUGUI bananaText;
    public TextMeshProUGUI MelonText;
    public TextMeshProUGUI CherriesText;
    public TextMeshProUGUI KiwiText;

    private void Start()
    {
    }
    public override void Spawned()
    {
        ResetScores();
        IncreateScore();
    }
    private void ResetScores()
    {
        soluongMelon = 0;
        soluongApple = 0;
        soluongBanana = 0;
        soluongKiwi = 0;
        soluongCherries = 0;
    }
    public void IncreateScore()
    {
        appleText.text = "" + soluongApple;

        bananaText.text = "" + soluongBanana;

        MelonText.text = "" + soluongMelon;

        CherriesText.text = "" + soluongCherries;

        KiwiText.text = "" + soluongKiwi;
    }
    public int ScoreApple()
    {
        return soluongApple+=1;
    }
    public int ScoreBanana()
    {
        return soluongBanana++;
    }
    public int ScoreMelon()
    {
        return soluongMelon++;
    }
    public int ScoreKiwi()
    {
        return soluongKiwi++;
    }
    public int ScoreCherries()
    {
        return soluongCherries++;
    }
}
