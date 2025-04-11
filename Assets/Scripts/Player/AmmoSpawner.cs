using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject ammoBoxPrefab;           // Prefab ของ Ammo Box
    public Transform[] spawnPoints;            // จุดที่ใช้สุ่ม spawn

    public float spawnInterval = 10f;          // ทุกกี่วินาทีให้สุ่มเกิดใหม่

    void Start()
    {
        InvokeRepeating("SpawnAmmoBox", 2f, spawnInterval);
    }

    void SpawnAmmoBox()
    {
        if (spawnPoints.Length == 0 || ammoBoxPrefab == null)
        {
            Debug.LogWarning("SpawnPoints หรือ AmmoBoxPrefab ยังไม่ได้เซ็ตใน Inspector!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(ammoBoxPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
