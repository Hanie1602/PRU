using UnityEngine;
using UnityEngine.UI; // Nếu có thanh máu

public class PlayerHealth : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;
	public Slider healthBar; // Nếu có thanh máu UI

	private void Start()
	{
		currentHealth = maxHealth;
		if (healthBar != null)
			healthBar.maxValue = maxHealth;
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			Die();
		}
		if (healthBar != null)
			healthBar.value = currentHealth;
	}

	private void Die()
	{
		Debug.Log("Player chết!");
		// Thêm animation chết, load lại scene, hoặc xử lý game over
	}
}
