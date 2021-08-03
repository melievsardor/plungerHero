using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBone : MonoBehaviour, IEnemyBone
{
    [SerializeField]
    private List<EnemyBone> neighbors = new List<EnemyBone>();

    [SerializeField]
    private string boneName = string.Empty;

    private EnemyController controller;

    private Transform boneParent;

    private bool isDestroy;
    public bool IsDestroy { get { return isDestroy; } set { isDestroy = value; } }

    private void Start()
    {
        boneParent = GameObject.FindWithTag("boneParent").transform;
        controller = GetComponentInParent<EnemyController>();

        controller.AddBone(this);
    }

    public void SetAnimationHit()
    {
        controller.SetAnimation("hit");
    }


    public void SetScattter()
    {
        if (boneName != string.Empty)
            controller.SetAnimation(boneName);


        foreach (var neighbor in neighbors)
        {
            if(controller.HasBone(neighbor))
            {
                neighbor.transform.parent = boneParent;
                neighbor.GetComponent<Rigidbody>().isKinematic = false;

                neighbor.IsDestroy = true;

                controller.RemoveBone(neighbor);
            }
           
        }
    }

    public void DestroyBone()
    {
        Destroy(gameObject, 2f);
    }
}
