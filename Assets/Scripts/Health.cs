using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public const int maxHealth = 100;
	[SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
	public RectTransform healthbar;

	public void TakeDamage(int amount)
	{
		if (!isServer)
		{
			return;
		}
		currentHealth = currentHealth - amount;
		if (currentHealth <= 0) 
		{
			currentHealth = maxHealth;
			RpcRespawn();
		}


	}

	void OnChangeHealth(int health)
	{
		healthbar.sizeDelta = new Vector2 (health * 2, healthbar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn()
	{
		if(isLocalPlayer)
		{
			transform.position = Vector3.zero;
		}
	}
}
