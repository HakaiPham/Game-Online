using Fusion;
using TMPro;
using UnityEngine;

public class ScoreUI : NetworkBehaviour
{
    public TextMeshProUGUI p1Text;
    public TextMeshProUGUI p2Text;
    public TextMeshProUGUI p3Text;
    public TextMeshProUGUI p4Text;

    private void Update()
    {
        if (ScoreManager.Instance == null) return;

        // For demo purposes, assume these are mapped to fruits
        p1Text.text = $"Banana: {ScoreManager.Instance.scoreBanana}";
        p2Text.text = $"Apple: {ScoreManager.Instance.scoreApple}";
        p3Text.text = $"Melon: {ScoreManager.Instance.scoreMelon}";
        p4Text.text = $"Cherries: {ScoreManager.Instance.scoreCherries}";
        // Add Kiwi somewhere else or adjust UI if needed
    }
}
