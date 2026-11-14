using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    public LogicScript logic;
    private bool scored;

    private void Awake()
    {
        if (logic == null) logic = FindFirstObjectByType<LogicScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BirdScript bird = collision.GetComponentInParent<BirdScript>();
        if (scored || bird == null || !bird.birdIsAlive || logic == null || logic.IsGameOver) return;
        scored = true;
        logic.addScore(1);
    }
}
