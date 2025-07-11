using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenShots = 0.5f;
    public float range = 100f;

    float timer;
    Ray shootray;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    AudioSource gunAudio;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = .2f;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
        gunLine = GetComponent<LineRenderer>();
        gunLight = GetComponent<Light>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButton ("Fire1") && timer >= timeBetweenShots)
        {
            Shoot();
        }

        if (timer >= timeBetweenShots * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    void Shoot()
    {
        timer = 0f;
        gunAudio.Play();
        gunLight.enabled = true;
        gunParticles.Stop();
        gunParticles.Play();
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
        shootray.origin = transform.position;
        shootray.direction = transform.forward;

        if (Physics.Raycast(shootray, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyhealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyhealth != null)
            {
                enemyhealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootray.origin + shootray.direction * range);
        }
    }
   public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
       

    }
}
