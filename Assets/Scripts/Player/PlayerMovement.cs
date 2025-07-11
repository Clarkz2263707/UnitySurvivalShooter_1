using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;

    private bool isDashing = false;
    private float dashTime = 0f;
    private bool canDash = true;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    private Timer dashCooldownTimer;

    private TrailRenderer trailRenderer;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        dashCooldownTimer = GetComponent<Timer>();

      
        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

     
        if (Input.GetKeyDown(KeyCode.T) && canDash)
        {
            StartDash();
        }

        if (isDashing)
        {
            dashTime += Time.fixedDeltaTime;
            if (dashTime >= dashDuration)
            {
                EndDash();
            }
        }

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    private void Move(float h, float v)
    {
        float currentSpeed = isDashing ? dashSpeed : speed;
        movement.Set(h, 0f, v);
        movement = movement.normalized * currentSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorhit;

        if (Physics.Raycast(camRay, out floorhit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorhit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

    private void StartDash()
    {
        isDashing = true;
        dashTime = 0f;
        canDash = false;
        if (dashCooldownTimer != null)
        {
            dashCooldownTimer.timer = 5; 
            StartCoroutine(DashCooldown());
        }
        
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
            trailRenderer.Clear();
        }
    }

    private void EndDash()
    {
        isDashing = false;
      
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }
    }

    private System.Collections.IEnumerator DashCooldown()
    {
        while (dashCooldownTimer.timer > 0)
        {
            yield return null;
        }
        canDash = true;
    }
}
