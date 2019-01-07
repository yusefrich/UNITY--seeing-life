using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleController : MonoBehaviour
{

    public void SetTimeScale(float value)
    {
        float timeScaleValue = Mathf.Clamp(value, 0, 1);
        Time.timeScale = timeScaleValue;
        
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }
}
