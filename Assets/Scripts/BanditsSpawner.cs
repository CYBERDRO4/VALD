using UnityEngine;
using System.Collections.Generic;
namespace Nicholas
{
    public class BanditsSpawner : MonoBehaviour
    {
        [SerializeField] private int spawnTimes; // Сколько спавнов всего произведет спавнер 
        [SerializeField] private List<GameObject> banditsPrefs; // Префабы для спавна
        [SerializeField] private float spawnRate; // Частота спавнов
        [SerializeField] private float spawnBanditsCountMin; // Сколько бандитов спавнятся за раз минимум
        [SerializeField] private float spawnBanditsCountMax; // Сколько бандитов спавнятся за раз максимум
        private bool triggered;
        private float nextSpawnTime = 0;

        private void FixedUpdate()
        {
            if(triggered && Time.time > nextSpawnTime && spawnTimes > 0)
            {
                Spawn();
                nextSpawnTime = Time.time + spawnRate;
                spawnTimes--;
            }   
        }
        private void Spawn()
        {
            for (int i = 0; i < Random.Range(spawnBanditsCountMin, spawnBanditsCountMax); i++)
            {
                Instantiate(banditsPrefs[Random.Range(0, banditsPrefs.Count)],transform.position, Quaternion.identity);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            triggered = true;
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            triggered = false;
        }

    }
}
