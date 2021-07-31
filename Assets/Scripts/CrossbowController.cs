using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowController : MonoBehaviour
{
    [SerializeField]
    private Transform plungerTransform = null;

    [SerializeField]
    private Transform obiEndTransform = null;

    [SerializeField]
    private Camera mainCamera = null;

    [SerializeField]
    private LayerMask layerMask;

    private float downPositionY;

    private bool isPlungerTarget;

    private void Start()
    {
        obiEndTransform.localPosition = new Vector3(1f, 0f, 0f);
    }

    private void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            downPositionY = Input.mousePosition.y;

            if (!isPlungerTarget)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit raycastHit, layerMask))
                {
                    var temp = raycastHit.point;

                    Animator animator = raycastHit.transform.GetComponentInParent<Animator>();

                    if(animator != null)
                    {
                        animator.StopPlayback();
                    }

                    temp.x = -temp.x;
                    temp.z = -temp.z;
                    plungerTransform.localPosition = temp;

                    isPlungerTarget = true;
                }
            }

        }


        if(Input.GetMouseButtonUp(0))
        {
            if (!isPlungerTarget)
                return;

            float upPositionY = Input.mousePosition.y;

            if(downPositionY - upPositionY >= 100f)
            {
                plungerTransform.localPosition = Vector3.zero;
                isPlungerTarget = false;
            }
            Debug.Log(downPositionY + " | " + upPositionY);
        }

    }

}
