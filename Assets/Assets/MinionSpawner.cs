using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinionSpawner : MonoBehaviour
{

    public float minionMoveSpeed;
    public float superMinionMoveSpeed;

    public GameObject minionPrefab;
    public GameObject superMinionPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 20.0f;
    public int miniondPerWave = 6;
    public int wavesUntilSuperMinion = 3;
    public int waveCount = 0;

    public float delayBetweenMinions;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMinions());
    }

    private IEnumerator SpawnMinions()
    {
        while (true)
        {
            waveCount++;

            if (waveCount % wavesUntilSuperMinion == 0)
            {
                for (int i = 0; i < miniondPerWave - 1; i++)
                {
                    SpawnRegularMinion();
                    yield return new WaitForSeconds(delayBetweenMinions);
                }

                SpawnRegularMinion();
                yield return new WaitForSeconds(delayBetweenMinions);

                SpawnSuperMinion();
                yield return new WaitForSeconds(spawnInterval - delayBetweenMinions * (miniondPerWave - 1) - delayBetweenMinions);
            }
            else
            {
                for(int i = 0;i < miniondPerWave; i++)
                {
                    SpawnRegularMinion();
                    yield return new WaitForSeconds(delayBetweenMinions);
                }

                yield return new WaitForSeconds(spawnInterval - delayBetweenMinions * miniondPerWave);
            }
        }
    }

    private void SpawnRegularMinion()
    {
        Transform SpawnPOint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject minion = Instantiate(minionPrefab, SpawnPOint.position, SpawnPOint.rotation);

        UnityEngine.AI.NavMeshAgent minionAgent = minion.GetComponent<UnityEngine.AI.NavMeshAgent>();
        minionAgent.speed = minionMoveSpeed;
    }

    private void SpawnSuperMinion()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject superMinion = Instantiate(superMinionPrefab, spawnPoint.position, spawnPoint.rotation);

        UnityEngine.AI.NavMeshAgent superMinionAgent = superMinion.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (superMinionAgent != null)
        {
            superMinionAgent.speed = superMinionMoveSpeed;
        }
        else
        {
            Debug.LogError("NavMeshAgent component is missing on superMinionPrefab.");
        }
    }



}
