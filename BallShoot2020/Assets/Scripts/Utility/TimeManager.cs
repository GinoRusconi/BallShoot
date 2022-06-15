using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float slowDownFactor = 0.05f;
    private bool IsSlowTime = false;
    private float defaultTimeScale;
    private float defaultFixedDeltaTime;

    private void Start()
    {
        defaultTimeScale = Time.timeScale;
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void DoSlowMotion()
    {
        if (!IsSlowTime)
        {
            IsSlowTime = !IsSlowTime;
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
        }
    }

    public void ResetSlowMotion()
    {
        if (IsSlowTime)
        {
            Time.timeScale = defaultTimeScale;
            IsSlowTime = !IsSlowTime;
        }
    }

    /*void Update()
   {
       ////Funciona para volver al tiempo normal lentamente, hay que subir la variable a global.
       //public float slowDownDuration = 2f;
       //Time.timeScale += (1f / slowDownDuration) * Time.unscaledDeltaTime;
       //Time.fixedDeltaTime += (0.01f / slowDownDuration) * Time.unscaledDeltaTime;
       //Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
       //Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0f, 0.01f);
   }*/
}