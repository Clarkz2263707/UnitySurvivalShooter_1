using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerhealth;
    EnemyHealth enemyhealth;
    NavMeshAgent nav;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerhealth = player.GetComponent<PlayerHealth>();
        enemyhealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (enemyhealth.CurrentHealth > 0 && playerhealth.health > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
