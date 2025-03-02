using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ToxicZone : MonoBehaviour
{
	public GameObject gameOverUI;
	private bool isGameOver = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") && !isGameOver)
		{
			StartCoroutine(DeathSequence(collision.gameObject));
		}
	}

	IEnumerator DeathSequence(GameObject player)
	{
		isGameOver = true; 

		//Vô hiệu hóa điều khiển nhân vật
		if (player.GetComponent<PlayerController>() != null)
		{
			player.GetComponent<PlayerController>().enabled = false;
		}

		//Tắt trọng lực để không rơi xuyên qua
		if (player.GetComponent<Rigidbody2D>() != null)
		{
			player.GetComponent<Rigidbody2D>().gravityScale = 0;
			player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero; //Dừng di chuyển
		}

		//Hiển thị toàn bộ Game Over UI
		if (gameOverUI != null)
		{
			gameOverUI.SetActive(true);

			//Gọi hiệu ứng Fade In cho "Game Over"
			GameObject gameOverText = gameOverUI.transform.Find("GameOverText").gameObject;
			if (gameOverText != null)
			{
				gameOverText.GetComponent<Animator>().SetTrigger("FadeIn");
			}
		}

		//Chờ cho đến khi người chơi bấm Enter
		yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

		//Load lại màn chơi
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}