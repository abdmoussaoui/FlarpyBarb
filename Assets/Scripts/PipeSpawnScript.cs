using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe;
    [Min(0.1f)] public float spawnRate = 2f;
    [Min(0f)] public float heightOffset = 10f;
    private float timer;
    private LogicScript logic;

    private void Start()
    {
        logic = FindFirstObjectByType<LogicScript>();
        if (pipe == null)
        {
            Debug.LogError("Assign a pipe prefab to the pipe spawner.", this);
            enabled = false;
            return;
        }
        spawnPipe();
    }

    private void Update()
    {
        if (logic != null && logic.IsGameOver) return;
        timer += Time.deltaTime;
        float interval = Mathf.Max(0.1f, spawnRate);
        if (timer >= interval)
        {
            timer %= interval;
            spawnPipe();
        }
    }

    private void spawnPipe()
    {
        Vector3 position = transform.position;
        position.y += Random.Range(-heightOffset, heightOffset);
        Instantiate(pipe, position, transform.rotation);
    }
}
