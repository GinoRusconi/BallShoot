using System.Collections;
using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    #region Setts And Getters

    # endregion Setts And Getters

    [Header("State Enemy")]
    [SerializeField] protected float speedEnemy = 5f;
    [SerializeField] protected int life = 1;

    [Header("Objects Reference")]
    [SerializeField] protected GameObject graphicEnemy;
    [SerializeField] protected GameObject deadEnemy;

    protected ParticleSystem particleDeadEnemy;
    protected AudioSource _enemyDeadSound;

    protected LayerMask defautLayerMask;
    protected Rigidbody2D _RigidBody2D;

    //movement variables
    protected Vector2 myDirection;

    protected float angle;
    protected float xVelocity;
    protected float yVelocity;

    protected virtual void Awake()
    {
        _RigidBody2D = GetComponent<Rigidbody2D>();
        _enemyDeadSound = GetComponent<AudioSource>();
        particleDeadEnemy = deadEnemy.GetComponent<ParticleSystem>();
        defautLayerMask = LayerMask.NameToLayer("Default");
    }

    protected virtual void OnEnable()
    {
        PrepareToEnable();
        SetVelocityStart();
    }

    protected virtual void Update()
    {
        
        SetRotation(_RigidBody2D.velocity.normalized);
    }

    protected void SetRotation(Vector2 directionToLook)
    {
        myDirection = directionToLook;
        Quaternion to = Quaternion.FromToRotation(Vector3.right, myDirection);
        transform.rotation = to;

        Debug.DrawRay(transform.position, transform.right * 10, Color.green);
    }

    protected void SetVelocityStart()
    {
        xVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        yVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        _RigidBody2D.velocity = new Vector2(xVelocity, yVelocity) * speedEnemy;
    }

    protected void PrepareToEnable()
    {
        deadEnemy.SetActive(false);
        graphicEnemy.SetActive(true);
        gameObject.layer = defautLayerMask;
    }

    public void TakeDamage(int distanceBulletScore)
    {
        life -= 1;
        CheckLife(distanceBulletScore);
    }

    protected void CheckLife(int distanceBulletScore)
    {
        if (life < 1)
        {
            LevelSystem.Current.EnemyDieInLevel(distanceBulletScore);
            StartCoroutine(Die());
        }
    }

    protected IEnumerator Die()
    {
        gameObject.layer = LayerMask.NameToLayer("Dead");
        graphicEnemy.SetActive(false);
        deadEnemy.SetActive(true);
        particleDeadEnemy.Play();
        _enemyDeadSound.Play();

        yield return new WaitForSeconds(2f);
        PoolObjects.Current.DisablesObjectToPool(gameObject);
    }
    

    /*
    //orbita jaja
    //_RigidBody2D.MovePosition((Vector2) transform.position + (Vector2) transform.up * speedEnemy* Time.deltaTime);
    -------------------------------------------------------------------------------------------------------------------

        Vector3 targetFollow = positionPlayer.position - transform.position;
        targetFollow = targetFollow.normalized;
        transform.position = Vector2.MoveTowards(transform.position, positionPlayer.position, speedEnemy * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, positionPlayer.position, speedEnemy * Time.deltaTime);
        transform.up = transform.position - positionPlayer.position;

        Vector2 relative = transform.InverseTransformPoint(positionPlayer.position);
        float angle = Mathf.Atan2(relative.x, relative.y)* Mathf.Rad2Deg;
        _RigidBody2D.MoveRotation(_RigidBody2D.rotation + angle * Time.deltaTime);
        */

    /*
    if (!positionPlayer.gameObject.GetComponent<PlayerCore>()._playerDead)
    {
        rotatedVectorToTarget = Quaternion.Euler(0, 0, 0) * vectorToTarget;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget * Time.fixedDeltaTime);

        //direction = positionPlayer.position - transform.position;
        //transform.right = direction.normalized;

        _RigidBody2D.MovePosition((Vector2)transform.position + (Vector2)vectorToTarget.normalized * speedEnemy * Time.fixedDeltaTime);
    }else
    {
        _RigidBody2D.MovePosition((Vector2)transform.position + Vector2.up * speedEnemy * Time.fixedDeltaTime);
    }*/
}