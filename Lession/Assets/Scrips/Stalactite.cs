using System.Collections;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
	private Vector3 startPosition;
	private Rigidbody2D rb;
	private bool isFalling = false;

	//Thời gian trước khi thạch nhũ rơi
	public float fallDelay = 2.0f;

	//Thời gian chờ trước khi quay lại vị trí cũ
	public float resetDelay = 1.0f;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		startPosition = transform.position;

		//Giữ yên trước khi rơi
		rb.bodyType = RigidbodyType2D.Kinematic;

		//Tự động rơi mà không cần nhân vật
		StartCoroutine(AutoFallLoop());
	}

	IEnumerator AutoFallLoop()
	{
		while (true)
		{
			//Đợi trước khi rơi
			yield return new WaitForSeconds(fallDelay);

			rb.bodyType = RigidbodyType2D.Dynamic;
			rb.gravityScale = 2.5f;

			//Chờ cho đến khi chạm đất
			yield return new WaitUntil(() => rb.linearVelocity.y == 0);

			//Chờ trước khi reset
			yield return new WaitForSeconds(resetDelay);

			rb.bodyType = RigidbodyType2D.Kinematic;
			rb.linearVelocity = Vector2.zero;

			//Reset vị trí
			transform.position = startPosition;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !isFalling)
		{
			StartCoroutine(Fall());
		}
	}

	IEnumerator Fall()
	{
		isFalling = true;
		yield return new WaitForSeconds(fallDelay);

		//Chuyển sang Dynamic để rơi xuống
		rb.bodyType = RigidbodyType2D.Dynamic;

		//Điều chỉnh trọng lực để rơi nhanh hơn
		rb.gravityScale = 2.5f;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		//Khi chạm đất
		if (collision.gameObject.CompareTag("Ground")) 
		{
			//Chặn mọi lực tác động
			rb.bodyType = RigidbodyType2D.Kinematic;

			//Đảm bảo đứng yên
			rb.linearVelocity = Vector2.zero; 

			StartCoroutine(ResetPosition());
		}
	}

	IEnumerator ResetPosition()
	{
		yield return new WaitForSeconds(resetDelay);

		rb.bodyType = RigidbodyType2D.Kinematic;
		rb.linearVelocity = Vector2.zero;

		//Reset vị trí
		transform.position = startPosition;
		isFalling = false;
	}
}
