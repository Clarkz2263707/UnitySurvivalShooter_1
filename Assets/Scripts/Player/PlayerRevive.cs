using UnityEngine;

public class PlayerRevive : MonoBehaviour
{
    public int reviveCost = 100;
    private bool canRevive = true;
    private PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Start()
    {
        
    }

    void Update()
    {
      
        if (playerHealth != null && IsPlayerDead() && canRevive)
        {
            

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (ScoreManager.score >= reviveCost)
                {
                    ScoreManager.score -= reviveCost;
                    ScoreManager.instance.ShowScore();
                    RevivePlayer();
                    canRevive = false;
                }
                else
                {
                    Debug.Log("Not enough points to revive!");
                }
            }
        }
    }

    private bool IsPlayerDead()
    {
        var isDeadField = typeof(PlayerHealth).GetField("isDead", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (bool)isDeadField.GetValue(playerHealth);
    }

    private void RevivePlayer()
    {
      
        playerHealth.health = playerHealth.maxHealth;
        playerHealth.HealthSlider.value = playerHealth.health;

       
        var isDeadField = typeof(PlayerHealth).GetField("isDead", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        isDeadField.SetValue(playerHealth, false);

        playerHealth.GetComponent<PlayerMovement>().enabled = true;
        playerHealth.GetComponentInChildren<PlayerShooting>().enabled = true;

        
    }
}
