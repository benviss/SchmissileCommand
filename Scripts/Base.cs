﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

  public float baseHealth;
  private float remainingMissiles;
  private float missileSpeed;
  private float fireRate;
  private float missileExplosionRadius;
  private float missileExplosionSpeed;

  public GameObject missilePrefab;
  private float lastFire;

  public void Initialize(float baseHealth, float remainingMissiles, float missileExplosionRadius, float missileExplosionSpeed, float missileSpeed, float fireRate)
  {
    this.baseHealth = baseHealth;
    this.remainingMissiles = remainingMissiles;
    this.missileExplosionRadius = missileExplosionRadius;
    this.missileExplosionSpeed = missileExplosionSpeed;
    this.missileSpeed = missileSpeed;
    this.fireRate = fireRate;
  }

  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

  public void Fire(Vector3 targetPos)
  {
    remainingMissiles--;
    Vector3 difference = targetPos - transform.position;
    difference.Normalize();

    float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

    GameObject missile = (GameObject)Instantiate(
      missilePrefab,
      transform.position,
      Quaternion.Euler(0f, 0f, rotation_z - 90));

    Missile missileComponent = missile.GetComponent<Missile>();
    missileComponent.Initialize(targetPos, missileExplosionRadius, missileExplosionSpeed);

    missile.GetComponent<Rigidbody2D>().velocity = difference * missileSpeed;

    lastFire = Time.time;
  }

  void Hit(float damage)
  {
    baseHealth -= damage;
  }

  public bool CanFire()
  {
    return (remainingMissiles > 0 && lastFire + fireRate < Time.time);
  }

  public bool IsAlive()
  {
    return baseHealth > 0;
  }
}