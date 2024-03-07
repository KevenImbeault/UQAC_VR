using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public float dropDistance = 0.1f; // Distance que la plate va drop
    public Color activatedColor = Color.green; // couleur qu'on change quadn activ�
    public string cubeTag = "CorrectCube"; // Tag pour le cube
    public float exitDelay = 1f; // d�lais avant que la plque remonte et change de couleur

    private Vector3 originalPosition; // position original
    private bool isActivated = false; // pour suivre si la plaque est activ�
    private Renderer renderer; // Le rendu de la plaque pour changer la couleur
    private Rigidbody rb; // le rb de la plaque

    void Start()
    {
        // position orignal
        originalPosition = transform.position;
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
            // on calcule le drop down avec notre variable public drop distance
            Vector3 targetPosition = originalPosition - new Vector3(0, dropDistance, 0);
            // Move the pressure plate to the target position
            rb.MovePosition(targetPosition);
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
        }
        else
        {
            // si false on start le d�lais de reset
            StartCoroutine(ResetPressurePlateDelayed());
        }
    }

    IEnumerator ResetPressurePlateDelayed()
    {
        // On attend le d�lais selon notre variable de d�lais
        yield return new WaitForSeconds(exitDelay);

        // remet la plaque � la position de d�part
        rb.MovePosition(originalPosition);
        // remet le suivi de la plaque � false (comme au d�part)
        isActivated = false;
        // On remet la couleur de d�part
        renderer.material.color = Color.white; // � modifi� selon la couleur souhait�
    }
}


