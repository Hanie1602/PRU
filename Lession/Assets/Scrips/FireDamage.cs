using UnityEngine;
using System.Collections;

public class FireDamage : MonoBehaviour
{
	[SerializeField] private int damage = 10; //Sát thương mỗi lần chạm
	[SerializeField] private float damageCooldown = 1.5f; //Thời gian miễn sát thương
	private bool canTakeDamage = true; //Kiểm tra xem có thể bị sát thương không

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && canTakeDamage)
		{
			PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
			if (playerHealth != null)
			{
				//playerHealth.TakeDamage(damage);
				StartCoroutine(InvincibilityCooldown(collision.gameObject));
			}
		}
	}

	IEnumerator InvincibilityCooldown(GameObject player)
	{
		canTakeDamage = false;
		StartCoroutine(BlinkEffect(player)); // Nhấp nháy khi bị sát thương
		yield return new WaitForSeconds(damageCooldown);
		canTakeDamage = true;
	}

	IEnumerator BlinkEffect(GameObject player)
	{
		SpriteRenderer sprite = player.GetComponent<SpriteRenderer>();
		if (sprite != null)
		{
			for (int i = 0; i < 5; i++) // Nhấp nháy 5 lần
			{
				sprite.enabled = !sprite.enabled;
				yield return new WaitForSeconds(0.1f);
			}
			sprite.enabled = true; // Đảm bảo nhân vật hiển thị lại
		}
	}
}
