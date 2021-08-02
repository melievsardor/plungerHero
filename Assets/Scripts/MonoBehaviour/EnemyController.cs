using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private PlayerController playerController;

    private Animator animator;

    
    private Material[] materials;

    private bool isPlay = false;
    private bool isDeath = false;
    private bool isColor = false;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        
        animator = GetComponent<Animator>();

        var sphereCollider = GetComponentsInChildren<SphereCollider>();
        materials = new Material[sphereCollider.Length];
        for(int i  = 0; i < sphereCollider.Length; i++)
        {
            materials[i] = sphereCollider[i].GetComponent<MeshRenderer>().material;
        }
            
    }

    public void SetAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    public void SetPlay()
    {
        SetAnimation("walk");
        isPlay = true;
    }

    private void Update()
    {
        if (isDeath || !isPlay)
            return;

        transform.Translate(Vector3.forward * Time.deltaTime * 1 / speed );

        if( Vector3.Distance(transform.position, playerController.transform.position)  < 4f && !isColor)
        {
            isColor = true;
            SetColor();
        }
        else if (Vector3.Distance(transform.position, playerController.transform.position) < 2.5f)
        {
            playerController.IsDeath = true;
            isPlay = false;
            SetAnimation("idle");
        }
    }

    private void SetColor()
    {
        foreach (var m in materials)
            m.color = Color.cyan;
    }


}
