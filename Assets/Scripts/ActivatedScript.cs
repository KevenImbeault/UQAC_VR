using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedScript : MonoBehaviour
{
    public void onGoalCompleted()
    {
        this.gameObject.SetActive(false);
    }

    public void onGoalFailed()
    {
        this.gameObject.SetActive(true);
    }
}
