using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float timeBetweenAttacks = .5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth EnemyHealth;
    bool playerInRange;
    float timer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        EnemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange && EnemyHealth.CurrentHealth > 0)
        {
            Attack();
        }

        if (playerHealth.health <= 0)
        {
            anim.SetTrigger("PlayerDead");

        }

        void Attack()
        {
            timer = 0f;
            if (playerHealth.health > 0)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}
