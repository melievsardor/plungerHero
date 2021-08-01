using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 1 / speed );
    }


}
