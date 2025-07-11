using UnityEngine;
using System.Collections;

public class PlayerStabbing : MonoBehaviour
{
    public int stabDamage = 100;
    public float stabRange = 2f;
    public float stabDuration = 0.25f;
    public float stabCooldown = 2f;

    private bool canStab = true;
    private PlayerShooting playerShooting;

    private void Awake()
    {
        playerShooting = GetComponent<PlayerShooting>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canStab)
        {
            StartCoroutine(Stab());
        }
    }

    private IEnumerator Stab()
    {
        canStab = false;

        
        if (playerShooting != null)
            playerShooting.enabled = false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, stabRange))
        {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(stabDamage, hit.point);
            }
        }

       
        yield return new WaitForSeconds(stabDuration);

      
        if (playerShooting != null)
            playerShooting.enabled = true;

        yield return new WaitForSeconds(stabCooldown - stabDuration);

        canStab = true;
    }
}
