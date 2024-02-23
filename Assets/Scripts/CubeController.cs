using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform exitPoint; // The transform representing the exit point on the cube

    private bool HasLaser = false;
    private GameObject newLaser;

    public void TriggerLaserHit()
    {
        if(!HasLaser)
        {
            // Instantiate a new laser prefab at the exit point of the cube
            newLaser = Instantiate(laserPrefab, exitPoint.position, exitPoint.rotation);

            HasLaser = true;
        }

        // Set the direction of the new laser to be the same as the face's normal
        newLaser.transform.forward = exitPoint.forward;
        newLaser.transform.position = exitPoint.position;
    }

    public void TriggerLaserStop()
    {
        if(HasLaser)
        {
            Destroy(newLaser);
            HasLaser = false;
        }
    }
}