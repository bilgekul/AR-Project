using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BarrelGenerator : MonoBehaviour
{
    public GameObject barrelPrefab;
    public float spawnInterval;
    public float moveSpeed;
    public int maxBarrels;
    public float despawnZ;
    private int currentBarrelCount = 0;
    public int healthChangeAmount = 10;
    private HealthManager healthManager;
    private GameController gameController;
    private List<int> _availableNumbers = new List<int>();
    public Text leveltext;

    public AudioClip[] barrelSounds;

    private int currentlevel = 1;
    private void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        InitializeAvailableNumbers();
        StartCoroutine(GenerateBarrels());
    }
    private void InitializeAvailableNumbers()
    {
        for (int i = 0; i < 10; i++)
        {
            _availableNumbers.Add(i);
        }
    }

    private IEnumerator GenerateBarrels()
    {
        while (currentBarrelCount < maxBarrels)
        {
            SpawnBarrel();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnBarrel()
    {

        if (_availableNumbers.Count == 0)
        {

            UpdateLevel();


        }

        int randomIndex = Random.Range(0, _availableNumbers.Count);
        int randomNum = _availableNumbers[randomIndex];

        string barrelName = "Barrel" + randomNum.ToString();
        GameObject newBarrel = Instantiate(barrelPrefab, transform.position, Quaternion.Euler(0f, -90f, 0f));
        newBarrel.tag = "Barrel";
        newBarrel.name = barrelName;
        Rigidbody barrelRigidbody = newBarrel.GetComponent<Rigidbody>();
        barrelRigidbody.velocity = new Vector3(0f, 0f, -moveSpeed);
        currentBarrelCount++;

        StartCoroutine(DestroyBarrelWhenPastDespawnZ(newBarrel));

        GameObject soundObject = new GameObject("BarrelSound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(barrelSounds[randomNum]);
        Destroy(soundObject, barrelSounds[randomNum].length);

        _availableNumbers.RemoveAt(randomIndex);
    }

    private IEnumerator DestroyBarrelWhenPastDespawnZ(GameObject barrel)
    {
        while (barrel != null && barrel.transform.position.z < despawnZ)
        {
            yield return null;
        }

        if (barrel != null)
        {
            int num = int.Parse(barrel.name.Substring(6));
            healthManager.UpdateHealth(healthChangeAmount);
            int imageIndex = num;
            GameObject imageObject = GameObject.Find(imageIndex.ToString());
            if (imageObject != null)
            {
                Image imageComponent = imageObject.GetComponent<Image>();
                imageComponent.enabled = false;
            }
            Destroy(barrel);
            currentBarrelCount--;
          
        }
    }

    public void UpdateLevel()
    {
        currentlevel++;

        leveltext.text = "Level:" + currentlevel;
        moveSpeed -= 0.04f;

        Debug.Log("Level" + leveltext.text +  "-" + "hız:" + moveSpeed);
        ShowNumberImages();
        InitializeAvailableNumbers();
        
    }
    private void ShowNumberImages()
    {
        for (int i = 0; i < 10; i++)
        {
            int imageIndex = i;
            GameObject imageObject = GameObject.Find(imageIndex.ToString());
            if (imageObject != null)
            {
                Image imageComponent = imageObject.GetComponent<Image>();
                imageComponent.enabled = true;
            }
        }
    }

    public void DestroyBarrel(GameObject barrel)
    {
        int num = int.Parse(barrel.name.Substring(6));
        int imageIndex = num;
        GameObject imageObject = GameObject.Find(imageIndex.ToString());
        if (imageObject != null)
        {
            Image imageComponent = imageObject.GetComponent<Image>();
            imageComponent.enabled = false;
        }
        Destroy(barrel);
        currentBarrelCount--;
    }
}




