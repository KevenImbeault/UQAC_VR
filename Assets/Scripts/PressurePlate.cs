using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    
    public Color activatedColor = Color.green; // couleur qu'on change quand activé
    public string cubeTag = "CorrectCube"; // Tag pour le cube
    
    private bool isActivated = false; // pour suivre si la plaque est activé
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
        // On vérifie si l'objet a le bon tag et que la plaque n'est pas déjà activé
        if (collision.collider.CompareTag(cubeTag) && !isActivated)
        {
           
            // change notre suivie a true quand la plaque est activé
            isActivated = true;
            // change la couleur en vert
            renderer.material.color = activatedColor;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // On vérifie si l'objet a le bon tag et que la plaque n'est pas déjà activé
        if (collision.collider.CompareTag(cubeTag) && isActivated)
        {
            // remet le suivi de la plaque à false (comme au départ)
            isActivated = false;
            // On remet la couleur de départ
            renderer.material.color = Color.grey; // à modifié selon la couleur souhaité
        }   
    }
}


