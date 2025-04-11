using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;

    [Header("Enemy Prefabs")]
    public GameObject enemyType1;
    public GameObject enemyType2;
    public GameObject enemyType3;

    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    [Header("Enemy Settings")]
    public int baseMaxEnemies = 10;
    private int currentMaxEnemies;
    private int currentEnemyCount = 0;
    private int killedEnemyCount = 0;

    [Header("Progress Settings")]
    public int requiredKillsToAdvance = 12; // ✅ ใส่ตรงนี้

    void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentMaxEnemies = baseMaxEnemies * (int)Mathf.Pow(2, sceneIndex);

        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f || currentEnemyCount >= currentMaxEnemies)
            return;

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        int enemyTypeIndex = Random.Range(1, 4);
        GameObject enemyToSpawn = null;

        switch (enemyTypeIndex)
        {
            case 1:
                enemyToSpawn = enemyType1;
                break;
            case 2:
                enemyToSpawn = enemyType2;
                break;
            case 3:
                enemyToSpawn = enemyType3;
                break;
        }

        Instantiate(enemyToSpawn, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        currentEnemyCount++;
    }

    public void EnemyKilled()
    {
        killedEnemyCount++;
        Debug.Log($"[EnemyManager] Enemy killed: {killedEnemyCount} / {requiredKillsToAdvance}");

        if (killedEnemyCount >= requiredKillsToAdvance)
        {
            Debug.Log("[EnemyManager] Required kills reached. Loading next scene...");
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes! Game finished!");
        }
    }
}
