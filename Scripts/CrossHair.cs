using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private float gunAccuracy;


    //CrossHair Active or Deactive
    [SerializeField]
    private GameObject go_CrossHairHUD;

    public void FineSightAnimation(bool _flag)
    {
        animator.SetBool("FineSight", _flag);
    }

    public void FireAnimation()
    {
        if(animator.GetBool("FineSight"))
            animator.SetTrigger("FineShoot");
        else
            animator.SetTrigger("Shoot");
    }
    public float GetGunAccuracy()
    {
        if(animator.GetBool("FineSight"))
            gunAccuracy = 0.001f;
        else
            gunAccuracy = 0.08f;

        return gunAccuracy;
    }
}
