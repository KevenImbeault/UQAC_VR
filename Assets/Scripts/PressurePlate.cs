using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    
    public Color activatedColor = Color.green; // couleur qu'on change quand activ�
    public string cubeTag = "CorrectCube"; // Tag pour le cube
    
    private bool isActivated = false; // pour suivre si la plaque est activ�
    private Renderer renderer; // Le rendu de la plaque pour changer la couleur
    private Rigidbody rb; // le rb de la plaque

    void Start()
    {
        // le rb de la plaque
        rb = GetComponent<Rigidbody>();
        // le render de la plaque
        renderer = GetComponent<Renderer>();
   
    }

    void OnCollisionEnter(Collision collision)
    {
        // On v�rifie si l'objet a le bon tag et que la plaque n'est pas d�j� activ�
        if (collision.collider.CompareTag(cubeTag) && !isActivated)
        {
           
            // change notre suivie a true quand la plaque est activ�
            isActivated = true;
            // change la couleur en vert
            renderer.material.color = activatedColor;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // On v�rifie si l'objet a le bon tag et que la plaque n'est pas d�j� activ�
        if (collision.collider.CompareTag(cubeTag) && isActivated)
        {
            // remet le suivi de la plaque � false (comme au d�part)
            isActivated = false;
            // On remet la couleur de d�part
            renderer.material.color = Color.grey; // � modifi� selon la couleur souhait�
        }   
    }
}


