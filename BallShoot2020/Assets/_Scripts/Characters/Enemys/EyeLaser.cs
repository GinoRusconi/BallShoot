using UnityEngine;

public class EyeLaser : EnemyCore
{
    protected Transform _PlayerTransform;
    protected float currentVelocity = 0f;
    protected Vector2 normalWallColision;

    protected bool _IsTouchingWall = false;
    protected bool _IsReposition = false;
    protected override void Awake()
    {
        base.Awake();
        _PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        //Physics.Raycast(transform.position, transform.forward);


    }

    protected void FixedUpdate()
    {
        if (_IsTouchingWall)
        {
            LookAtSmooth(_PlayerTransform.position);
        }
        else
        {
            SetRotation(_RigidBody2D.velocity.normalized);
        }
    }

    protected void LookAtSmooth(Vector2 target)
    {
        myDirection = target - new Vector2(transform.position.x, transform.position.y);
        Quaternion to = Quaternion.FromToRotation(Vector3.right, myDirection);
        Quaternion from = transform.rotation;

        transform.rotation = Quaternion.Slerp(from, to, Time.deltaTime);

        Debug.DrawRay(transform.position, transform.right * 10, Color.green);
    }

    private void StopMovement()
    {
        _RigidBody2D.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            StopMovement();
            normalWallColision = collision.GetContact(0).normal.normalized;
            SetRotation(normalWallColision);
            Debug.Log(normalWallColision);
            _IsTouchingWall = true;
        }
    }


}
