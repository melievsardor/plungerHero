using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private List<IEnemyBone> bones = new List<IEnemyBone>();

    private PlayerController playerController;

    private Level level;

    private Animator animator;

    private Material[] materials;

    private bool isPlay = false;
    private bool isDeath = false;
    private bool isColor = false;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        
        animator = GetComponent<Animator>();

        level = FindObjectOfType<Level>();

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

        if (triggerName == "dying")
        {
            isDeath = true;
            level.NextGround();
        }
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
            playerController.SetDeath();
            isPlay = false;
            SetAnimation("idle");
        }
    }

    private void SetColor()
    {
        foreach (var m in materials)
            m.color = Color.cyan;
    }


    public void AddBone(IEnemyBone bone)
    {
        bones.Add(bone);
    }

    public void RemoveBone(IEnemyBone bone)
    {
        bone.DestroyBone();
        bones.Remove(bone);

    }

    public bool HasBone(IEnemyBone bone)
    {
        return bones.Contains(bone);
    }

}
