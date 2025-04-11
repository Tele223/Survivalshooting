using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject ammoBoxPrefab;           // Prefab �ͧ Ammo Box
    public Transform[] spawnPoints;            // �ش��������� spawn

    public float spawnInterval = 10f;          // �ء����Թҷ���������Դ����

    void Start()
    {
        InvokeRepeating("SpawnAmmoBox", 2f, spawnInterval);
    }

    void SpawnAmmoBox()
    {
        if (spawnPoints.Length == 0 || ammoBoxPrefab == null)
        {
            Debug.LogWarning("SpawnPoints ���� AmmoBoxPrefab �ѧ�������� Inspector!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(ammoBoxPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
