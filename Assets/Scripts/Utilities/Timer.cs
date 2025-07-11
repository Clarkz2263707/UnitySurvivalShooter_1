using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public int timer; 
    private void Awake()
    {
        Debug.Log("Start Timer");
        timer = 5;
        StartCoroutine(nameof(CountdownTimer));
    }
    private IEnumerator CountdownTimer()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            Debug.Log("Time Left" + timer);
        }
        
        Debug.Log("Timer Completed");
    }
}
