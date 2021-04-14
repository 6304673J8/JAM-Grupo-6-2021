using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBite : MonoBehaviour
{
	public int biteDamage = 1;
	public int enragedbiteDamage = 2;

	public Vector3 biteOffset;
	public float biteRange = 1f;
	public LayerMask biteMask;

	public void Bite()
	{
		Vector3 pos = transform.position;
		pos += transform.right * biteOffset.x;
		pos += transform.up * biteOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, biteRange, biteMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<LivesPlayer>().TakeDamage(biteDamage);
			colInfo.GetComponent<PlayerController>().BossAttacked();
		}
	}
	
	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * biteOffset.x;
		pos += transform.up * biteOffset.y;

		Gizmos.DrawWireSphere(pos, biteRange);
	}
}
