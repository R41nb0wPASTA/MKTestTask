using System;
using System.Collections;
using UnityEngine;

public class AnimationController_Ingosick : MonoBehaviour
{
    [Header("Anim Data")]
    [SerializeField] private IngosickAnimData animData;
    
    [Header("Materials")]
    [SerializeField] private Material ingosick_Hair;
    [SerializeField] private Material ingosick_Body;
    [SerializeField] private Material ingosick_Cloth;
    [SerializeField] private Material ingosick_Door;

    private Animator animator;

    private String opacity = "_Opacity";
    
    private void Awake()
    {
        animator = GetComponent<Animator>();

        SetMatsTransparent();
    }

    public void StartPlaying()
    {
        animator.Play("Step 1", -1);
    }

    public void FadeInChar()
    {
        StartCoroutine(FadeInCoroutine());
    }
    
    public void FadeOutChar()
    {
        StartCoroutine(FadeOutCoroutine());
    }
    
    private IEnumerator FadeInCoroutine()
    {
        AnimatorClipInfo[] animatorClip = animator.GetCurrentAnimatorClipInfo(0);
        float clipLength = animatorClip[0].clip.length;
        float timeToFade = clipLength / animData.fadeInInAnimPart;

        while (ingosick_Hair.GetFloat(opacity) < 0f)
        {
            float fadeAmount = ingosick_Hair.GetFloat(opacity) + animData.fadeInSpeed;

            UpdateMaterialsFade(fadeAmount);
            
            yield return new WaitForSeconds(timeToFade/fadeAmount * Time.fixedDeltaTime);
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] animatorClip = animator.GetCurrentAnimatorClipInfo(0);
        float clipLength = animatorClip[0].clip.length;
        float timeToFade = clipLength - (clipLength * (animationState.normalizedTime % 1));

        while (ingosick_Hair.GetFloat(opacity) > -1f)
        {
            float fadeAmount = ingosick_Hair.GetFloat(opacity) - animData.fadeOutSpeed;

            UpdateMaterialsFade(fadeAmount);

            yield return new WaitForSeconds(timeToFade/fadeAmount * Time.fixedDeltaTime);
        }
    }
    
    private void UpdateMaterialsFade(float fadeAmount)
    {
        if (fadeAmount < -1f)
            fadeAmount = -1f;
        else if (fadeAmount > 0f)
            fadeAmount = 0f;
        
        ingosick_Hair.SetFloat(opacity, fadeAmount);
        
        ingosick_Body.SetFloat(opacity, fadeAmount);
        
        ingosick_Cloth.SetFloat(opacity, fadeAmount);
    }

    private void SetMatsTransparent()
    {
        ingosick_Hair.SetFloat(opacity, -1f);

        ingosick_Body.SetFloat(opacity, -1f);

        ingosick_Cloth.SetFloat(opacity, -1f);

        ingosick_Door.SetFloat(opacity, -1f);
    }

    private void SetDoorVisible()
    {
        ingosick_Door.SetFloat(opacity, 0f);
    }
    
    public void OnARModuleStart(ARModuleWorkStartSignal arModuleWorkStartSignal)
    {
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale *= animData.exportScale;
        arModuleWorkStartSignal.goToPlace = this.gameObject;
    }
}
