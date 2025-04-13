using UnityEngine;
using Fusion;
using TMPro;

public class Fruit : NetworkBehaviour
{
    public bool isMelon;
    public bool isBanana;
    public bool isApple;
    public bool isCherries;
    public bool isKiwi;

    ScoreManager scoreManager;
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isMelon)
            {
                scoreManager.ScoreMelon();
            }
            if (isBanana)
            {
                scoreManager.ScoreBanana();
            }
            if (isApple)
            {
                Debug.Log("Đã ăn");
                scoreManager.ScoreApple();
            }
            if (isKiwi)
            {
                scoreManager.ScoreKiwi();
            }
            else if (isCherries)
            {
                scoreManager.ScoreCherries();
            }
        }
    }
}
