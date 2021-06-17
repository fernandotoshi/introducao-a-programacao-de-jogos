using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int score;
    public Text scoreText;
    public Text gameOverText;
    public Text countdownText;

    public float Countdown;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameOverText.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Countdown = Mathf.Clamp(Countdown - Time.deltaTime, 0, 100);
        countdownText.text = "Countdown: " + Mathf.CeilToInt(Countdown);

        if (Countdown <= 0 && !gameOverText.gameObject.activeSelf)
        {
            gameOverText.text = "Game Over!";
            gameOverText.gameObject.SetActive(true);
        }

        if (score == 16 && !gameOverText.gameObject.activeSelf)
        {
            gameOverText.text = "You win!";
            gameOverText.gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 force = new Vector3(x, 0, y);
        force.Normalize();
        force *= speed;

        rb.AddForce(force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            //other.gameObject.SetActive(false); // Deactivate gameObject
            Destroy(other.gameObject); // Destroy gameObject

            score++;
            scoreText.text = "Points: " + score;
        }
    }
}
