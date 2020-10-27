﻿using System.Collections;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
	public float hp, damage;
	public GameObject soul;
	public bool Souls()
	{
		int rand = Random.Range(0, 4);
		if (rand == 1)
			return true;
		else
			return false;
	}

	Color thisColor;
	private EnemiesList enemiesList;
	void Awake()
	{
		enemiesList = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemiesList>();

		thisColor = GetComponent<Renderer>().material.color;
		PlayerStats p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
	}
	void Update()
	{
		PlayerStats p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

		if (hp <= 0)
		{
			p.Heal(.5f);
			if (Souls() == true)
			{
				Instantiate(soul, transform.position, transform.rotation);
				Destroy(gameObject);
				enemiesList.enemiesAlive.Remove(this.gameObject);
			}
			else
			{
				Destroy(gameObject);
				enemiesList.enemiesAlive.Remove(this.gameObject);
			}

		}
	}
	void OnTriggerEnter(Collider other)
	{
		PlayerStats p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

		switch (other.tag)
		{
			case "ATCK":
				StartCoroutine(TakeDamage(p.damage));
				print(p.damage);
				break;
			case "ATCK2":
				StartCoroutine(TakeDamage(p.damage / 2));
				print(p.damage / 2);
				break;
			case "ATCK3":

				break;
		}
	}
	public IEnumerator TakeDamage(float amount)
	{
		hp -= amount;
		GetComponent<Renderer>().material.color = Color.red;
		yield return new WaitForSeconds(.3f);
		GetComponent<Renderer>().material.color = thisColor;
	}
}