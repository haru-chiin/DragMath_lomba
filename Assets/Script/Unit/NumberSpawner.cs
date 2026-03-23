using UnityEngine;

public class NumberSpawner : MonoBehaviour
{
    public GameObject numberPrefab;

    public Transform[] spawnPoints;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject obj =
                Instantiate(
                    numberPrefab,
                    spawnPoints[i].position,
                    Quaternion.identity
                );

            int value = Random.Range(1, 7);

            obj.GetComponent<NumberUnit>()
                .SetValue(value);
        }
    }
}