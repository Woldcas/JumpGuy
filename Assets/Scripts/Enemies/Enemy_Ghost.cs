using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Enemy_Ghost : Enemy
{
    [Header("Ghost Detalis")]
    [SerializeField] private float activeDuration;
    private float activeTimer;
    [Space]
    [SerializeField] private float xMinDistance;
    [SerializeField] private float yMinDistance;
    [SerializeField] private float yMaxDistance;

    private bool isChasing;
    private Transform target;

    protected override void Update()
    {
        base.Update();

        if (isDead)
            return;

        activeTimer -= Time.deltaTime;

        if (isChasing == false && idleTimer < 0)
        {
            StartChase();
        }
        else if (isChasing && activeTimer < 0)
        {
            EndChase();
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        if (canMove == false)
            return;

        HandleFlip(target.position.x);
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void StartChase()
    {
        Player player = PlayerManager.instance.player;

        if (player == null) // playerList.Count <= 0
        {
            EndChase();
            return;
        }

        target = player.transform;

        float xOffSet = Random.Range(0, 100) < 50 ? -1 : 1;
        float yPosition = Random.Range(yMinDistance, yMaxDistance);

        transform.position = target.position + new Vector3(xMinDistance * xOffSet, yPosition);

        activeTimer = activeDuration;
        isChasing = true;
        anim.SetTrigger("appear");
    }

    private void EndChase()
    {
        idleTimer = idleDuration;
        isChasing = false;
        anim.SetTrigger("disappear");
    }

    private void MakeInvisinle()
    {
        sr.color = Color.clear;
        EnableColliders(false);
    }
    private void MakeVisible()
    {
        sr.color = Color.white;
        EnableColliders(true);
    }

    public override void Die()
    {
        base.Die();
        canMove = false;
    }
}
