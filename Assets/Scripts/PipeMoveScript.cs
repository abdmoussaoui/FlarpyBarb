using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float deadZone = -45f;
    private LogicScript logic;

    private void Awake() => logic = FindFirstObjectByType<LogicScript>();

    private void Update()
    {
        if (logic != null && logic.IsGameOver) return;
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < deadZone) Destroy(gameObject);
    }
}
