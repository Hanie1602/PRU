using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed = 4f;
	public float jumpForce = 7f;  //Lực nhảy

	private Vector2 moveInput;
	private Rigidbody2D rb;
	private bool isGrounded;

	public Transform groundCheck;  //Điểm kiểm tra đất
	public float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;  //Layer của mặt đất

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		moveInput.x = Input.GetAxis("Horizontal");

		if (moveInput.x != 0)
		{
			transform.localScale = new Vector3(moveInput.x > 0 ? 4 : -4, 4, 0);
		}

		//Kiểm tra nhân vật đang đứng trên mặt đất
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

		//Kiểm tra phím K có được nhận không
		if (Input.GetKeyDown(KeyCode.K))
		{
			Debug.Log("K pressed!");
		}

		//Kiểm tra nhảy
		if (Input.GetKeyDown(KeyCode.K) && isGrounded)
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
			Debug.Log("Jump!");
		}
	}

	void FixedUpdate()
	{
		rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
	}

}
