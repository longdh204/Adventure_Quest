using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolStatsUI : MonoBehaviour
{
    public UnityEngine.UI.Text statsText;

    void Update()
    {
        if (BulletPool.Instance != null && statsText != null)
        {
            statsText.text = $"Active Bullets: {BulletPool.Instance.GetActiveCount()}\n" +
                           $"Pool Count: {BulletPool.Instance.GetPoolCount()}";
        }
    }
}
