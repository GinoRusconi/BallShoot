using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Puntos de Clicks")]
    [SerializeField] private Vector3 _StartPositionClick;

    [SerializeField] private Vector3 _DirectionClick;
    [SerializeField] private Vector3 _EndClick;

    #region Setts and Getters

    public Vector3 StartPositionClick { get => _StartPositionClick; set => _StartPositionClick = value; }
    public Vector3 DirectionClick { get => _DirectionClick; set => _DirectionClick = value; }
    public Vector3 EndClick { get => _EndClick; set => _EndClick = value; }
    public float MagnitudImpulse { get => _MagnitudImpulse; set => _MagnitudImpulse = value; }
    public bool IsImpulse { get => _IsPropulse; set => _IsPropulse = value; }

    #endregion Setts and Getters

    [Header("Magnitud del arrastre")]
    [SerializeField] private float _MinImpulse;

    [SerializeField] private float _MaxImpulse;
    public float _MagnitudImpulse;
    private bool _IsPropulse = false;

    [Header("Objetos para referenciar")]
    [SerializeField] private Camera _Camera;

    [SerializeField] private GunCore _Gun;
    private TimeManager _TimeManager;

    public float _MaxTimeReload = 2f;
    public float _TimeToReload = 0f;
    public bool _IsShoot = false;

    public RectTransform _CanvasParent;
    public RectTransform _Joystick;
    public Vector2 _StartJoystickPosition;
   

    private void Awake()
    {
        _TimeManager = GetComponent<TimeManager>();
    }

    private void Start()
    {
        _StartJoystickPosition = _Joystick.anchoredPosition;
    }

    private void Update()
    {
        if(!_IsShoot)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        SetStartPosition(touch);
                        break;

                    case TouchPhase.Moved:
                        _TimeManager.DoSlowMotion();
                        SetMagnitudImpulse(touch);
                        SetDirection(touch);
                        break;

                    case TouchPhase.Ended:
                        EndedAction();
                        break;
                }
            }
        }else
        {
            if (_TimeToReload <= Time.time)
            {
                _IsShoot = !_IsShoot;
            }
        }
        
    }

    private void EndedAction()
    {
        _TimeManager.ResetSlowMotion();
        _TimeToReload = _MaxTimeReload + Time.time;
        _Joystick.anchoredPosition = _StartJoystickPosition;
        IsImpulse = true;
        _IsShoot = true;
        _Gun.Shoot();

        
    }
    private void SetStartPosition(Touch touch)
    {
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_CanvasParent, touch.position, _Camera, out anchoredPos);
        _Joystick.anchoredPosition = anchoredPos;
        StartPositionClick = _Camera.ScreenToViewportPoint(touch.position);
    }

    private void SetDirection(Touch touch)
    {
        EndClick = _Camera.ScreenToViewportPoint(touch.position);

        if (EndClick != StartPositionClick)
        {
            DirectionClick = _Camera.ScreenToViewportPoint(touch.position) - StartPositionClick;
            //DirectionClick = DirectionClick.normalized;
        }
    }

    private void SetMagnitudImpulse(Touch touch)
    {
        MagnitudImpulse = Vector3.Distance(_Camera.ScreenToViewportPoint(touch.position), StartPositionClick);
        MagnitudImpulse = Mathf.Clamp(MagnitudImpulse, _MinImpulse, _MaxImpulse);
    }

    private void OnDisable()
    {
        _Joystick.anchoredPosition = _StartJoystickPosition;
    }
}