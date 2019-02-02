using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MovementDebugData : MonoBehaviour
{

    public TextMeshProUGUI currentTime; 
    public TextMeshProUGUI nextJumpThresh;

    public void SetCurrentTime(float value)
    {
        currentTime.SetText(value.ToString());
    }
    public void SetNextJump(float value)
    {
        nextJumpThresh.SetText(value.ToString());
    }
    
}
