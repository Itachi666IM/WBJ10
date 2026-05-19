using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject trashPrefab;
    [SerializeField] private float timeBetweenSpawns;
    private float nextSpawnTime;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private void Update()
    {
        if(!GameManager.Instance.isPaused)
        {
            if(Time.time > nextSpawnTime)
            {
                SpawnTrashAtRandomPos();
                nextSpawnTime = Time.time + timeBetweenSpawns;
            }
        }
    }

    private void SpawnTrashAtRandomPos()
    {
        Vector2 spawnPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Instantiate(trashPrefab,spawnPos,Quaternion.identity);
    }
}
