using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLogic : MonoBehaviour
{
	public Transform player;

	public bool isLookingAt = false;

	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isLookingAt)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isLookingAt = false;
		}
		else if (transform.position.x < player.position.x && !isLookingAt)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isLookingAt = true;
		}
	}
}
