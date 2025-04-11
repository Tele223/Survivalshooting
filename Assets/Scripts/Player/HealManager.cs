using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealManager : MonoBehaviour
{
    public GameObject healBoxPrefab;       // Prefab ของกล่อง Heal
    public float spawnInterval = 10f;      // ระยะเวลาการ spawn แต่ละกล่อง
    public Transform[] healSpawnPoints;    // จุดที่สามารถ spawn ได้
    public int maxHealBoxes = 3;           // กล่อง Heal สูงสุดที่มีในฉาก

    private int currentHealBoxCount = 0;

    void Start()
    {
        InvokeRepeating("SpawnHealBox", 2f, spawnInterval); // เริ่ม spawn
    }

    void SpawnHealBox()
    {
        if (currentHealBoxCount >= maxHealBoxes)
            return;

        int index = Random.Range(0, healSpawnPoints.Length);
        Transform spawnPoint = healSpawnPoints[index];

        GameObject healBox = Instantiate(healBoxPrefab, spawnPoint.position, Quaternion.identity);
        currentHealBoxCount++;

        // เมื่อกล่องหาย → ลด count
        HealBox boxScript = healBox.GetComponent<HealBox>();
        if (boxScript != null)
        {
            boxScript.onHealUsed = () => {
                currentHealBoxCount--;
            };
        }
    }
}

