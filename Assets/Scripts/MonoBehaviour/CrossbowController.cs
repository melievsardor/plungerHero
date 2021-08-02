using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowController : MonoBehaviour
{
    [SerializeField]
    private Transform plungerParentTransform;

    [SerializeField]
    private Transform plungerTransform = null;

    [SerializeField]
    private Transform obiEndTransform = null;

    [SerializeField]
    private Camera mainCamera = null;

    [SerializeField]
    private LayerMask layerMask;

    private PlayerController player;

    private float downPositionY;

    private bool isPlungerTarget;
    private bool isDown;

    private bool isShoot;
    public bool IsShoot { get { return isShoot; } set { isShoot = value; } }

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();

        obiEndTransform.localPosition = new Vector3(-1f, 0f, 0f);
    }

    private void Update()
    {
        if (!isShoot || player.IsDeath)
            return;


        if(Input.GetMouseButtonDown(0))
        {
            downPositionY = Input.mousePosition.y;

            isDown = true;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, layerMask))
        {
            //raycastHit.rigidbody.isKinematic = false;

            if(!isPlungerTarget)
            {
                if (isDown)
                {
                    plungerTransform.parent = raycastHit.transform;

                    var bone = plungerTransform.parent.GetComponent<EnemyBone>();

                    if(bone != null)
                        bone.SetAnimationHit();

                    plungerTransform.localPosition = new Vector3(0f, 0f, 0.5f);
                    isPlungerTarget = true;
                }
            }
            

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isPlungerTarget)
                return;

            float upPositionY = Input.mousePosition.y;

            if (downPositionY - upPositionY >= 100f)
            {

                var eneyBone = plungerTransform.parent.GetComponent<EnemyBone>();

                if (eneyBone != null)
                    eneyBone.SetScattter();

                plungerTransform.parent = plungerParentTransform;

                plungerTransform.localPosition = Vector3.zero;

                plungerTransform.localEulerAngles = Vector3.zero;

                isPlungerTarget = false;
                isDown = false;
            }
            Debug.Log(downPositionY + " | " + upPositionY);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    downPositionY = Input.mousePosition.y;

        //    if (!isPlungerTarget)
        //    {
        //        //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //        if (Physics.Raycast(ray, out raycastHit, layerMask))
        //        {
        //            var temp = raycastHit.point;

        //            Animator animator = raycastHit.transform.GetComponentInParent<Animator>();

        //            if(animator != null)
        //            {
        //                animator.StopPlayback();
        //            }

        //            temp.x = -temp.x;
        //            temp.z = -temp.z;
        //            plungerTransform.localPosition = temp;

        //            isPlungerTarget = true;
        //        }
        //    }

        //}




    }

}
