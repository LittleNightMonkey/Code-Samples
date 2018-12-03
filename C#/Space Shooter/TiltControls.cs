using UnityEngine;

// By Taru Konttinen 2018

public class TiltControls : MonoBehaviour
{
    public Rigidbody2D rb;
    bool dead;
    bool gameHasBegun;
    Vector3 neutralAccel;
    public float speed;
    Vector3 projectileOffset;

    public GameObject gameManager;
    public GameObject projectile;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileOffset = new Vector3(0, 0.8f, 1);
    }

    void Update()
    {
        // Set neutral acceleration for the first time and start game on touch
        if(!gameHasBegun && Input.touchCount > 0)
        {
            SetNeutralAccel();
            gameHasBegun = true;
        }

        // If not dead, take input and move accordingly
        if (!dead)
        {
            Vector3 accel = Input.acceleration;

            // Only y-axis controls are affected by the neutral acceleration
            float shipYAccel = accel.y - neutralAccel.y;

            // Neutral position has screen facing downwards - adjust y in case the screen is turned upwards
            if (neutralAccel.y < 0 && neutralAccel.z > 0 || neutralAccel.y > 0 && neutralAccel.z > 0)
            {
                if (accel.y < 0 && accel.z < 0 || accel.y > 0 && accel.z < 0)
                {
                    shipYAccel = -1 - (Mathf.Abs(-1 - accel.y)) - neutralAccel.y;
                }
            }

            // Neutral position has screen facing upwards - adjust y in case the screen is turned downwards
            else if (neutralAccel.y < 0 && neutralAccel.z < 0|| neutralAccel.y > 0 && neutralAccel.z < 0)
            {
                if (accel.y < 0 && accel.z > 0 || accel.y > 0 && accel.z > 0)
                {
                    shipYAccel = -1 - (Mathf.Abs(-1 - accel.y)) - neutralAccel.y;
                }
            }

            rb.velocity = new Vector3(accel.x * speed, shipYAccel * speed, 0);

            if (Input.touchCount > 0)
            {
                ResetNeutralAccel();
            }
        }
    }

    // Die upon colliding with anything tagged "Planet"
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Planet")
        {
            CancelInvoke();
            dead = true;
            GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            gameManager.GetComponent<gamemanager>().PlayerDead();
        }
    }

    private void Shoot()
    {
        Instantiate(projectile, transform.position + projectileOffset, Quaternion.identity);
    }

    // Set neutral acceleration values
    public void SetNeutralAccel()
    {
        neutralAccel = Input.acceleration;
        gameManager.GetComponent<gamemanager>().AccelOK();
        InvokeRepeating("Shoot", 0.01f, 0.2f);
    }

    // Reset neutral acceleration values
    public void ResetNeutralAccel()
    {
        neutralAccel = Input.acceleration;
    }
}