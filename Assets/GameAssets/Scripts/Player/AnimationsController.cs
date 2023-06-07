using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationsController : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(float dir)
    {
        animator.SetFloat("MoveDir", dir);
    }

    public void Strafe(float dir)
    {
        animator.SetFloat("StrafeDir", dir);
    }
    public void Run(bool en)
    {
        animator.SetBool("IsRun", en);
    }
    public void Jump()
    {
        animator.SetTrigger("Jump");
    }
    public void Shoot()
    {
        animator.SetTrigger("Shoot");
    }
    public void SetWeapon(int indx)
    {
        animator.SetInteger("WeaponIndx", indx);
    }

}
