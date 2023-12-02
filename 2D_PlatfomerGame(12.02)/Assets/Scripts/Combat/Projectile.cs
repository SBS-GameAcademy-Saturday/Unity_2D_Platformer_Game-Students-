using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
	public int damage = 10;
	public Vector2 moveSpeed = new Vector2(3.0f, 0);
	public Vector2 knockback = Vector2.zero;

	Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
    {
		rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);	   
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Damageable damageable = collision.GetComponent<Damageable>();
		if(damageable != null)
		{
			Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback
				: new Vector2(-knockback.x, knockback.y);
			bool gotHit = damageable.GetHit(damage, deliveredKnockback);
			if(gotHit)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
