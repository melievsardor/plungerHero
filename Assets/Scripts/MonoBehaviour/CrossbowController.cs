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

            if (isPlungerTarget || isDown)
                return;

            isDown = true;

            ArrowForward();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isPlungerTarget)
                return;

            ArrawBackward();
        }


    }

    private void ArrowForward()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 15f, layerMask))
        {
            if(raycastHit.transform.tag == "selectable")
            {
                plungerTransform.parent = raycastHit.transform;

                var bone = plungerTransform.parent.GetComponent<EnemyBone>();

                if (bone != null && !bone.IsDestroy)
                {
                    bone.SetAnimationHit();

                    plungerTransform.localPosition = new Vector3(0f, 0f, 0.5f);
                    isPlungerTarget = true;
                }
            }

            isDown = false;
        }
        else
        {
            EmptySpace();
        }
    }

    private void ArrawBackward()
    {
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
        }

        isDown = false;
    }

    private void EmptySpace()
    {
        var temp = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        temp.z = -1f;

        if (temp.x < 0.45f)
        {
            temp.x = 1 - temp.x;
        }
        else if (temp.x >= 0.45f && temp.x <= 0.55f)
        {
            temp.x = temp.x - 0.5f;
        }
        else
        {
            temp.x = -temp.x;
        }

        if (temp.y >= 0.45f && temp.y <= 0.55f)
        {
            temp.y = 1f;
        }
        else if (temp.y > 0.55f)
        {
            temp.y = temp.y * 2f;
        }

        plungerTransform.localPosition = new Vector3(temp.x, temp.y, temp.z);

        StartCoroutine(WaitArrowBackward());
    }

    private IEnumerator WaitArrowBackward()
    {
        yield return new WaitForSeconds(0.3f);

        plungerTransform.localPosition = Vector3.zero;

        isDown = false;
    }

}
