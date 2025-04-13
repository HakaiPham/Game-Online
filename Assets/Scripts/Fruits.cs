using UnityEngine;
using Fusion;

public class Fruit : NetworkBehaviour
{
    ScoreManager scoreManager;
    public bool isBanana;
    public bool isApple;
    public bool isMelon;
    public bool isCherries;
    public bool isKiwi;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isBanana)
            {
                scoreManager.ScoreBanana();
            }
            if (isApple)
            {
                scoreManager.ScoreApple();
            }
            if (isMelon)
            {
                scoreManager.ScoreMelon();
            }
            if (isCherries)
            {
                scoreManager.ScoreCherries();
            }
            if (isKiwi)
            {
                scoreManager.ScoreKiwi();
            }
        }
    }
}
