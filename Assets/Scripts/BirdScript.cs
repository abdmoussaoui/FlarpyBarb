using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength = 20f;
    public LogicScript logic;
    public bool birdIsAlive = true;
    private Camera gameCamera;

    private void Awake()
    {
        if (myRigidbody == null) myRigidbody = GetComponent<Rigidbody2D>();
        if (logic == null) logic = FindFirstObjectByType<LogicScript>();
        gameCamera = Camera.main;
    }

    private void Update()
    {
        if (!birdIsAlive || (logic != null && logic.IsGameOver)) return;
        bool flap = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
        if (Input.touchCount > 0) flap |= Input.GetTouch(0).phase == TouchPhase.Began;
        if (flap)
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
            if (logic != null) logic.PlayFlapSound();
        }
        if (gameCamera != null)
        {
            float height = gameCamera.WorldToViewportPoint(transform.position).y;
            if (height < -0.1f || height > 1.1f) Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) => Die();

    private void Die()
    {
        if (!birdIsAlive) return;
        birdIsAlive = false;
        if (logic != null) logic.gameOver();
    }
}
