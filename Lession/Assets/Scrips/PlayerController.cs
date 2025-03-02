using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] public float moveSpeed = 5f;
	[SerializeField] public float runSpeed = 15f;
	[SerializeField] public float jumpForce = 15f;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private float moveInput;
	private bool isGrounded;
	private Animator animator;
	//private GameManager gameManager;
	private Rigidbody2D rb;
	//private AudioManager audioManager;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		//gameManager = FindAnyObjectByType<GameManager>();
		//audioManager = FindAnyObjectByType<AudioManager>();
	}

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

	}

	void Update()
	{
		//if (gameManager.IsGameOver()) 
		//    return;
		HandleJump();
		HandleMovement();
		HandleAttack();
		UpdateAnimation();
		if (Input.GetKeyDown(KeyCode.Z))
		{
			moveSpeed = runSpeed;
		}
		if (Input.GetKeyUp(KeyCode.Z))
		{
			moveSpeed = 5f;
		}
	}
	private void HandleMovement()
	{
		float moveInput = Input.GetAxis("Horizontal");
		rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

		if (moveInput > 0)
			transform.localScale = new Vector3(4, 4, 4);
		else if (moveInput < 0)
			transform.localScale = new Vector3(-4, 4, 4);
	}

	private void HandleJump()
	{
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			//audioManager.PlayJumpSound();
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
		}
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
	}

	private void HandleAttack()
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			animator.SetTrigger("isAttacking");
		}
	}

	private void UpdateAnimation()
	{
		bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
		bool isJumping = !isGrounded;
		animator.SetBool("isRunning", isRunning);
		animator.SetBool("isJumping", isJumping);
		//animator.SetTrigger("isAttacking");
	}
}