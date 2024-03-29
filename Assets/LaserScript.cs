using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserScript : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask layerMask;
    public float defaultLength = 50;
    public int nbOfReflections = 2;

    private LineRenderer _lineRenderer;
    private RaycastHit hit;

    private Ray ray;
    private Vector3 direction;

    private CubeController cubeController;
    private LaserReceiverController laserReceiverController;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ReflectLaser();
    }

    void ReflectLaser()
    {
        ray = new Ray(transform.position, transform.forward);

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);

        float remainLength = defaultLength;

        for(int i = 0; i < nbOfReflections; i++)
        {
            // Does the ray intersect any objects
            if (Physics.Raycast(ray.origin, ray.direction, out hit, defaultLength, layerMask))
            {
                // Check if the ray hits a cube
                if (hit.collider.CompareTag("Laser Cube"))
                {
                    // Get the CubeController script attached to the cube
                    cubeController = hit.collider.GetComponent<CubeController>();

                    // Trigger the cube's CubeController script
                    if (cubeController != null)
                    {
                        cubeController.TriggerLaserHit();
                    }
                } else if (hit.collider.CompareTag("Laser Receiver")) 
                {
                    laserReceiverController = hit.collider.GetComponent<LaserReceiverController>();

                    if(laserReceiverController != null)
                    {
                        laserReceiverController.TriggerLaserHit();
                    }
                } else
                {
                    if (cubeController != null)
                    {
                        cubeController.TriggerLaserStop();
                        cubeController = null;
                    }

                    if (laserReceiverController != null)
                    {
                        laserReceiverController.TriggerLaserStop();
                        laserReceiverController = null;
                    }
                }
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

                remainLength -= Vector3.Distance(ray.origin, hit.point);

                //ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                
            }
            else
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + (ray.direction * remainLength));
                if (cubeController != null)
                {
                    cubeController.TriggerLaserStop();
                    cubeController = null;
                }

                if (laserReceiverController != null)
                {
                    laserReceiverController.TriggerLaserStop();
                    laserReceiverController = null;
                }
            }
        }
    }

    void NormalLaser()
    {
        _lineRenderer.SetPosition(0, transform.position);

        // Does the ray intersect any objects
        if (Physics.Raycast(transform.position, transform.forward, out hit, defaultLength, layerMask))
        {
            _lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, transform.position + (transform.forward * defaultLength));
        }
    }
}