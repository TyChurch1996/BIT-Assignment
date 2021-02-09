using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform Spawnpoint;

    void OnCollisionEnter2D(Collision2D col)
	{
		if (col.transform.CompareTag("Player"))
		{
			col.transform.position = Spawnpoint.position;

		}

	}
}
