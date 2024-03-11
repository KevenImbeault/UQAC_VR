using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceiverController : MonoBehaviour
{
    public bool activated = false;
    public Color activatedColor = Color.green; // couleur qu'on change quand activ�
    private Renderer renderer; // Le rendu du receiver pour changer la couleur

    void Start()
    {
        // le render du receiver
        renderer = GetComponent<Renderer>();
    }

    public void TriggerLaserHit()
    {
        if(!activated)
        {
            activated = true;
            // change la couleur en vert
            renderer.material.color = activatedColor;
        }
    }

    public void TriggerLaserStop()
    {
        if (activated)
        {
            activated = false;
            // On remet la couleur de d�part
            renderer.material.color = Color.grey; // � modifi� selon la couleur souhait�
        }
    }
}
