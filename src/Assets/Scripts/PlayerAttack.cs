﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float attackWaitTime, damage, rangedAttackCost;
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] Transform shootingPoint;
    [SerializeField] AudioClip rangedAttackSound;
    [SerializeField] AudioClip[] meleeAttackSound;

    bool canAttack = true;

    Animator anim;
    PlayerMovement playerMovement;
    PlayerHealth playerHealth;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            StartCoroutine(Attack());
        }

        if (Input.GetButtonDown("Fire2") && canAttack)
        {
            StartCoroutine(RangedAttack());
        }
    }

    IEnumerator RangedAttack()
    {
        canAttack = false;
        anim.SetTrigger("ranged");
        Instantiate(fireballPrefab, shootingPoint.position, shootingPoint.rotation);
        AudioSource.PlayClipAtPoint(rangedAttackSound, transform.position);
        //reduce hp
        playerHealth.DecreasePlayerHealth(rangedAttackCost);

        yield return new WaitForSeconds(attackWaitTime);
        canAttack = true;
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("attack");
        canAttack = false;
        AudioSource.PlayClipAtPoint(meleeAttackSound[Random.Range(0, meleeAttackSound.Length)], transform.position);
        yield return new WaitForSeconds(attackWaitTime);
        canAttack = true;
    }

    public float GetPlayerDamage()
    {
        return damage;
    }

    void ActivateSwordCollider()
    {
        transform.Find("Sword").GetComponent<BoxCollider2D>().enabled = true;
    }

    void DeactivateSwordCollider()
    {
        transform.Find("Sword").GetComponent<BoxCollider2D>().enabled = false;
    }
}
