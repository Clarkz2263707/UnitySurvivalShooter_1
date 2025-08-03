using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerhealth;
    EnemyHealth enemyhealth;
    NavMeshAgent nav;

    public void SetPlayer(GameObject playerObj, PlayerHealth health)
    {
        player = playerObj.transform;
        playerhealth = health;
    }

    private void Awake()
    {
        enemyhealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (enemyhealth.CurrentHealth > 0 && playerhealth != null && playerhealth.health > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
