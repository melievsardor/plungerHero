using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBone : MonoBehaviour
{
    [SerializeField]
    private List<Rigidbody> neighbors = new List<Rigidbody>();

    [SerializeField]
    private string boneName = string.Empty;

    private EnemyController controller;

    private Transform boneParent;

    private void Start()
    {
        boneParent = GameObject.FindWithTag("boneParent").transform;
        controller = GetComponentInParent<EnemyController>();
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
            neighbor.transform.parent = boneParent;
            neighbor.isKinematic = false;
        }
    }



}
