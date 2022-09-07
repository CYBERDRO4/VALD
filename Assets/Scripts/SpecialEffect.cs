using UnityEngine;

public class SpecialEffect : MonoBehaviour
{
    public float aliveTime = 2;
    private float spawnTime;

    private void Start()
    {
        spawnTime = aliveTime;
    }

    private void Update()
    {
        if (Time.time > aliveTime)
            gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        spawnTime = Time.time + aliveTime;
    }
    private void OnDisable()
    {
        spawnTime = Time.time + aliveTime;
    }




}
