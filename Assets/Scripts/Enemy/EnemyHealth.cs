using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    bool hasSunk = false; // ✅ กันไม่ให้เรียกซ้ำ

    private EnemyManager enemyManager; // ✅ อ้างถึง EnemyManager

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = startingHealth;

        // หา EnemyManager
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead) return;

        enemyAudio.Play();
        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }

    public void StartSinking()
    {
        if (hasSunk) return; // ✅ ป้องกันเรียกซ้ำ
        hasSunk = true;

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;

        ScoreManager.score += scoreValue;

        // ✅ แจ้ง EnemyManager ว่าศัตรูตัวนี้ตายแล้ว
        if (enemyManager != null)
        {
            enemyManager.EnemyKilled();
        }

        Destroy(gameObject, 2f);
    }
}
