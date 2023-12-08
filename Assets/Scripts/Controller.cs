using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Animator animator;
    public Button Attack;
    private int barrel_count = 0;
    public GameObject repeatPanel;
    public GameObject barrelGenerator;
   

    private Dictionary<string, bool> animationState = new Dictionary<string, bool>();

    private void Start()
    {
        animator = GetComponent<Animator>();
        animationState.Add("AttackTrigger", false);
        Attack.onClick.AddListener(() => OnAnimationButtonClick("AttackTrigger"));
       
        
    }
    public void OnAnimationButtonClick(string triggerName)
    {
        if (animationState.ContainsKey(triggerName) && !animationState[triggerName])
        {
            animator.SetTrigger(triggerName);
            animationState[triggerName] = true;

            StartCoroutine(ResetAnimationAfterTime(triggerName, animator.GetCurrentAnimatorClipInfo(0)[0].clip.length));
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrel"))
        {
            Destroy(other.gameObject);

            barrel_count++;

            Debug.Log(barrel_count);

            if (barrel_count == 1)
            {
                animator.SetTrigger("GetHitTrigger");

                StartCoroutine(DizzyAnimation(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length));
            }
            else if (barrel_count == 3)
            {   
                barrelGenerator.SetActive(false);
                animator.SetTrigger("DieTrigger");
                StartCoroutine(ShowRepeatPanelAfterDelay(1.5f));
                
            }
            else
            {
               
                animator.SetTrigger("GetHitTrigger");

              
                StartCoroutine(DizzyAnimation(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length));
            }
        }
    }
    private IEnumerator ShowRepeatPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        repeatPanel.SetActive(true);
    }

    private IEnumerator DizzyAnimation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

       
        animator.SetTrigger("StandTrigger");
    }


    private IEnumerator StandAnimation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

    }

    private IEnumerator ResetAnimationAfterTime(string triggerName, float time)
    {
        yield return new WaitForSeconds(time);

        animator.ResetTrigger(triggerName);
        animationState[triggerName] = false;
    }
}

