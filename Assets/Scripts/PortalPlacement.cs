using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(CameraMove))]
public class PortalPlacement : MonoBehaviour
{
    public InputActionReference headRotationReference;

    [Header("Hand Select Actions")]
    public InputActionReference leftTriggerInputActionReference;
    public InputActionReference rightTriggerInputActionReference;

    [Header("Controllers")]
    public ActionBasedController rightController;
    public ActionBasedController leftController;

    private float _leftTriggerValue;
    private float _rightTriggerValue;

    private Vector3 _rightHandPosition;
    private Vector3 _rightHandForward;

    private Vector3 _leftHandPosition;
    private Vector3 _leftHandForward;

    [SerializeField]
    private PortalPair portals;

    [SerializeField]
    private LayerMask layerMask;

    private void Update()
    {
        
        _rightHandForward = rightController.transform.forward;
        _rightHandPosition = rightController.transform.position;
        _leftHandForward = leftController.transform.forward;
        _leftHandPosition = leftController.transform.position;

        // Check if the triggers are being pressed by the user
        _leftTriggerValue = leftTriggerInputActionReference.action.ReadValue<float>();
        _rightTriggerValue = rightTriggerInputActionReference.action.ReadValue<float>();

        // Try shooting the right hand portal when pointing at a surface where portals can land
        RaycastHit rightHandHit;
        if (Physics.Raycast(_rightHandPosition, _rightHandForward, out rightHandHit, Mathf.Infinity, layerMask))
        {
            if (_rightTriggerValue == 1)
            {
                FirePortal(1, _rightHandPosition, _rightHandForward, rightHandHit, 250.0f);
            }
        }

        // Try shooting the left hand portal when pointing at a surface where portals can land
        RaycastHit leftHandHit;
        if (Physics.Raycast(_leftHandPosition, _leftHandForward, out leftHandHit, Mathf.Infinity, layerMask))
        {
            if (_leftTriggerValue == 1)
            {
                FirePortal(0, _rightHandPosition, _rightHandForward, leftHandHit, 250.0f);
            }
        }
    }

    private void FirePortal(int portalID, Vector3 pos, Vector3 dir, RaycastHit hit, float distance)
    {
        //Physics.Raycast(pos, dir, out hit, distance, layerMask);
        if(hit.collider != null)
        {
            // If we shoot a portal, recursively fire through the portal.
            //if (hit.collider.tag == "Portal")
            //{
            //    var inPortal = hit.collider.GetComponent<Portal>();

            //    if(inPortal == null)
            //    {
            //        return;
            //    }

            //    var outPortal = inPortal.OtherPortal;

            //    // Update position of raycast origin with small offset.
            //    Vector3 relativePos = inPortal.transform.InverseTransformPoint(hit.point + dir);
            //    relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
            //    pos = outPortal.transform.TransformPoint(relativePos);

            //    // Update direction of raycast.
            //    Vector3 relativeDir = inPortal.transform.InverseTransformDirection(dir);
            //    relativeDir = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeDir;
            //    dir = outPortal.transform.TransformDirection(relativeDir);

            //    distance -= Vector3.Distance(pos, hit.point);

            //    //FirePortal(portalID, pos, dir, distance);

            //    return;
            //}

            // Orient the portal according to camera look direction and surface direction.
            var cameraRotation = headRotationReference.action.ReadValue<Quaternion>();
            var portalRight = cameraRotation * Vector3.right;
            
            if(Mathf.Abs(portalRight.x) >= Mathf.Abs(portalRight.z))
            {
                portalRight = (portalRight.x >= 0) ? Vector3.right : -Vector3.right;
            }
            else
            {
                portalRight = (portalRight.z >= 0) ? Vector3.forward : -Vector3.forward;
            }

            var portalForward = -hit.normal;
            var portalUp = -Vector3.Cross(portalRight, portalForward);

            var portalRotation = Quaternion.LookRotation(portalForward, portalUp);
            
            // Attempt to place the portal.
            bool wasPlaced = portals.Portals[portalID].PlacePortal(hit.collider, hit.point, portalRotation);

            //if(wasPlaced)
            //{
            //    crosshair.SetPortalPlaced(portalID, true);
            //}
        }
    }
}
