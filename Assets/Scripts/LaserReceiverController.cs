using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceiverController : MonoBehaviour
{
    public bool activated = false;

    public void TriggerLaserHit()
    {
        if(!activated)
        {
            activated = true;
        }
    }

    public void TriggerLaserStop()
    {
        if (activated)
        {
            activated = false;
        }
    }
}
