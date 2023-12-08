using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{

    public Animator animator;
    private BarrelGenerator barrelGenerator;

    private bool canDestroyBarrel = false;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        barrelGenerator = FindObjectOfType<BarrelGenerator>();
    }
 
    public void OnAttackEvent()
    {
        Debug.Log("Fonksiyon cagirildi.");
        if (barrelGenerator == null)
        {
            barrelGenerator = FindObjectOfType<BarrelGenerator>();
            if (barrelGenerator == null)
            {
                Debug.LogError("BarrelGenerator is still null. Make sure it's assigned.");
                return;
            }
        }

        canDestroyBarrel = true;
    }

    private void Update()
    {
        if (canDestroyBarrel)
        {
          
            if (barrelGenerator != null)
            {
                GameObject closestBarrel = FindClosestBarrel();
                if (closestBarrel != null)
                {
                    barrelGenerator.DestroyBarrel(closestBarrel);
                }

                canDestroyBarrel = false;
            }
            else
            {
                Debug.LogError("BarrelGenerator is null. Make sure it's assigned.");
            }
        }
    }

    public GameObject FindClosestBarrel()
    {
        GameObject[] barrels = GameObject.FindGameObjectsWithTag("Barrel");
        if (barrels.Length == 0)
        {
            return null;
        }

        GameObject closestBarrel = barrels[0];
        float closestDistance = Vector3.Distance(transform.position, closestBarrel.transform.position);

        foreach (GameObject barrel in barrels)
        {
            float distance = Vector3.Distance(transform.position, barrel.transform.position);
            if (distance < closestDistance)
            {
                closestBarrel = barrel;
                closestDistance = distance;
            }
        }

        return closestBarrel;
    }
   
}
