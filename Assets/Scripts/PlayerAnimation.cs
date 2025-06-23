using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 15f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isJumping;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Scoring")]
    public int score = 0;
    public Text scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        UpdateScoreUI();
        isJumping = false;
    }

    void Update()
    {
        float moveInput = 0f;

        // Di chuyển bằng phím A/D
        if (Input.GetKey(KeyCode.A))
            moveInput = -1f;
        else if (Input.GetKey(KeyCode.D))
            moveInput = 1f;

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && isJumping)
        {
            isJumping = false;
        }

        // Nhảy bằng phím Space
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            FindObjectOfType<SoundManager>()?.PlayJump();
        }

        // Cập nhật animation
        animator.SetBool("isRunning", moveInput != 0);
        animator.SetBool("isJumping", isJumping);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mushroom"))
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            Debug.Log("Scale sau khi ăn nấm: " + transform.localScale);
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("Coin"))
        {
            score += 4;
            UpdateScoreUI();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Player va chạm với chướng ngại vật!");
            GameController.instance?.GameOver();
            gameObject.SetActive(false);
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Điểm: " + score.ToString();
        }
    }
}
