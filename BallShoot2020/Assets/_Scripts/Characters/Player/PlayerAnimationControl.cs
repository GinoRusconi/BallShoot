using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    private PlayerInput _PlayerInput;
    private Animator _Animator;

    void Start()
    {
        _PlayerInput = GetComponentInParent<PlayerInput>();
        _Animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!_PlayerInput.IsImpulse)
        {
            //RotatePlayer();
            RotatePlayerToPoint();
        }
        if (_PlayerInput._IsShoot)
        {
            _Animator.SetTrigger("Shoot");
        }
    }

    private void RotatePlayer()
    {
        Vector3 myLocation = _PlayerInput.transform.position;
        Vector3 targetLocation = _PlayerInput.DirectionClick;
        targetLocation.z = myLocation.z;

        //Vector3 vectorToTarget = myLocation - targetLocation;

        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * targetLocation;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);

    }

    private void RotatePlayerToPoint()
    {
        Vector3 myDirection = _PlayerInput.DirectionClick;
        Quaternion to = Quaternion.FromToRotation(Vector3.right, myDirection);
        transform.rotation = to;

        Debug.DrawRay(transform.position, transform.right * 10, Color.green);
    }



    /*
    private void CalculateAngularRotation()
    {
        Vector3 _vectorAngle = _PlayerInput.StartPositionClick -_PlayerInput.DirectionClick;
        _vectorAngle.Normalize();
        float angle = Mathf.Atan2(_vectorAngle.x, _vectorAngle.y) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, angle);
    }
    */

}
