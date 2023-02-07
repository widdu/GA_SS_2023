using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public Animator animator;
    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeOut(){
        animator.SetTrigger("FadeOut");
    }
    public void FadeIn(){
        animator.SetTrigger("FadeIn");
    }

}
