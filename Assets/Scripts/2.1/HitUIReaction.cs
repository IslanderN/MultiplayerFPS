using UnityEngine;
using System.Collections;

public class HitUIReaction : MonoBehaviour
{
    [SerializeField]
    private GameObject HitCrosshair;
    [SerializeField]
    private float timeWait;

    private float time;

    bool isInActiveState = false;

    public void HitReaction()
    {
        HitCrosshair.SetActive(true);
        time = timeWait;
        isInActiveState = true;
    }
    
    void Update()
    {
        if (isInActiveState)
        {
            time -= Time.deltaTime;
            if (time <= 0f)
            {
                HitCrosshair.SetActive(false);
                isInActiveState = false;
            }
        }
    }

}
