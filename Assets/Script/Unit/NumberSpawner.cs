using UnityEngine;

public class NumberSpawner : MonoBehaviour
{
    public GameObject numberPrefab;
    public Transform[] spawnPoints;

    public Sprite[] unitSprites;

    private NumberUnit[] spawnedUnits;

    private void Awake()
    {
        spawnedUnits = new NumberUnit[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject obj = Instantiate(
                numberPrefab,
                spawnPoints[i].position,
                Quaternion.identity,
                transform
            );

            NumberUnit unitScript = obj.GetComponent<NumberUnit>();
            spawnedUnits[i] = unitScript;

            if (unitSprites != null && i < unitSprites.Length)
            {
                unitScript.SetSprite(unitSprites[i]);
            }

            obj.SetActive(false);
        }
    }

    public void SpawnQuestion(int[] numbers)
    {
        int count = Mathf.Min(spawnPoints.Length, numbers.Length);

        for (int i = 0; i < count; i++)
        {
            NumberUnit unit = spawnedUnits[i];

            unit.gameObject.SetActive(true);
            unit.transform.position = spawnPoints[i].position;
            unit.SetValue(numbers[i]);
        }
    }
}