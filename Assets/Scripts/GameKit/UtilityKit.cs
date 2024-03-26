//using UnityEngine;

using System;

public class UtilityKit // : MonoBehaviour
{
    // 유틸성 메서드를 모으려고 함

    /// <summary>
    /// Unix timestamp
    /// </summary>
    /// <returns>Unix timestamp</returns>
    public static float GetCurrentTime()
    {
        return (float)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }

}
