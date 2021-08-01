using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Animator animator;

    private bool isDeath = false;

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
        if (isDeath || !GameManager.Instance.IsPlay)
            return;

        transform.Translate(Vector3.forward * Time.deltaTime * 1 / speed );
    }


}
