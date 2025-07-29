using UnityEngine;

public class Enemy_BlueBird : Enemy
{
    [Header("Blue Bird Details")]
    [SerializeField] private float travelDisance = 8;
    [SerializeField] private float flyForce = 1.5f;

    private Vector3[] wayPoints = new Vector3[2];
    private int wayIndex = 0;

    private bool inPlayMode;

    protected override void Start()
    {
        base.Start();

        wayPoints[0] = new Vector3(transform.position.x - travelDisance / 2, transform.position.y);
        wayPoints[1] = new Vector3(transform.position.x + travelDisance / 2, transform.position.y);

        inPlayMode = true;

        wayIndex = Random.Range(0, wayPoints.Length);
    }

    protected override void Update()
    {
        base.Update();

        HandleMovement();
    }

    private void FlyUp() => rb.linearVelocity = new Vector2(rb.linearVelocity.x, flyForce);

    private void HandleMovement()
    {
        if (canMove == false)
            return;

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayIndex], moveSpeed * Time.deltaTime);
        HandleFlip(wayPoints[wayIndex].x);

        if (Vector2.Distance(transform.position, wayPoints[wayIndex]) < .1f)
        {
            wayIndex++;

            if(wayIndex >= wayPoints.Length)
                wayIndex = 0;
        }
    }

    protected override void HandleAnimator()
    {
        //
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (inPlayMode == false)
        {
            float distance = travelDisance / 2;

            Vector3 leftPos = new Vector3(transform.position.x - distance, transform.position.y);
            Vector3 rightPos = new Vector3(transform.position.x + distance, transform.position.y);

            Gizmos.DrawLine(leftPos, rightPos);

            Gizmos.DrawWireSphere(leftPos, .5f);
            Gizmos.DrawWireSphere(rightPos, .5f);
        }
        else
        {
            Gizmos.DrawLine(transform.position, wayPoints[0]);
            Gizmos.DrawLine(transform.position, wayPoints[1]);

            Gizmos.DrawWireSphere(wayPoints[0], .5f);
            Gizmos.DrawWireSphere(wayPoints[1], .5f);
        }

    }
}
