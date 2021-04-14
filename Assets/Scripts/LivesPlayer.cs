using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesPlayer : MonoBehaviour
{
	public int health = 8;

	public void TakeDamage(int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		//CARGAR ESCENA DE DERROTA
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
