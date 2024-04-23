using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public float launchForce = 1000f; // Ajuster pour modifier la force avec laquel le bloc va être lancer en l'air

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CorrectCube")) // Ajuster selon le tag voulu
        {
            Rigidbody cubeRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Debug.Log(cubeRigidbody);
            if (cubeRigidbody != null)
            {
                Debug.Log("Adding Force");
                cubeRigidbody.AddForce(Vector3.up * launchForce, ForceMode.Impulse); // on ajoute une force vector up selon la variable de launchforce
            }
        }
    }
}
