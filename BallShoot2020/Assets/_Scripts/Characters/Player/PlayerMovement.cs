using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _PlayerInput;
    private Rigidbody2D m_RigidBody;

    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        _PlayerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Impulse();
    }

    private void Impulse()
    {
        if (_PlayerInput.IsImpulse)
        {
            _PlayerInput.IsImpulse = !_PlayerInput.IsImpulse;
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.AddForce(_PlayerInput.DirectionClick * _PlayerInput.MagnitudImpulse, ForceMode2D.Impulse);
        }
    }
}