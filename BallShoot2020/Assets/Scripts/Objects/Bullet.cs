using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("State Bullet")]
    [SerializeField] private float SpeedBullet;
    private Vector2 startBulletDistance;
    private Vector2 endBulletDistance;

    private bool IsHiting;
    private Rigidbody2D m_RigidBody;

    public void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (m_RigidBody != null)
        {
            m_RigidBody.WakeUp();
            m_RigidBody.velocity = -1 * SpeedBullet * transform.right;
            startBulletDistance = transform.position;
            IsHiting = false;
        }
    }

    private void Start()
    {
        SetStartMove();
    }

    private void SetStartMove()
    {
        m_RigidBody.velocity = -1 * SpeedBullet * transform.right;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy") && !IsHiting)
        {
            
            IsHiting = true;
            EnemyCore enemyCore = collider.GetComponent<EnemyCore>();
            int scoreDistanceBulletRound = DistanceBullet();
            enemyCore.TakeDamage(scoreDistanceBulletRound);
            
            DisabledBullet();
        }

        if (collider.CompareTag("Wall")&& !IsHiting)
        {
            DisabledBullet();
        }
    }

    private int DistanceBullet()
    {
        endBulletDistance = transform.position;
        float scoreDisctanceBullet = Vector2.Distance(startBulletDistance, endBulletDistance);
        int scoreDistanceBulletRound = Mathf.FloorToInt(scoreDisctanceBullet);
        return scoreDistanceBulletRound;
    }

    private void DisabledBullet()
    {
        PoolObjects.Current.DisablesObjectToPool(gameObject);
    }
}