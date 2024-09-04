using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;
    public GameObject[] bombPrefabs;

    [Range(0f, 1f)]
    public float bombChance = 0.5f;

    public int numFruits = 2;
    public int numBombs = 2;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;

    private int currentExpectedFruitIndex = 0;
    private bool gameIsOver = false;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while (!gameIsOver)
        {
            for (int i = 0; i < numFruits; i++)
            {
                SpawnObject(fruitPrefabs[i], i);
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            }

            for (int i = 0; i < numBombs; i++)
            {
                if (Random.value < bombChance)
                {
                    SpawnObject(bombPrefabs[Random.Range(0, bombPrefabs.Length)], -1);
                    yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
                }
            }
        }
    }

    private void SpawnObject(GameObject prefab, int index)
    {
        Vector3 position = new Vector3();
        position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

        GameObject obj = Instantiate(prefab, position, rotation);
        Destroy(obj, maxLifetime);

        float force = Random.Range(minForce, maxForce);
        obj.GetComponent<Rigidbody>().AddForce(obj.transform.up * force, ForceMode.Impulse);

        Fruit fruitScript = obj.GetComponent<Fruit>();
        if (fruitScript != null)
        {
            fruitScript.SetSpawner(this);
            fruitScript.fruitIndex = index;
        }
    }

    public bool CheckFruitOrder(int fruitIndex)
    {
        if (gameIsOver) return false;

        if (fruitIndex == currentExpectedFruitIndex)
        {
            currentExpectedFruitIndex = (currentExpectedFruitIndex + 1) % numFruits;
            return true;
        }
        else
        {
            gameIsOver = true;
            Debug.Log("Fin de partie : Fruit tranché incorrect.");
            FindObjectOfType<GameManager>().GameOver();
            return false;
        }
    }
}
