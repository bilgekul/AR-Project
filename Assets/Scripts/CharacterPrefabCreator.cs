using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CharacterPrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Vector3 prefabOffset;

    private GameObject dog;
    private ARTrackedImageManager aRTrackedImageManager;

    private void OnEnable()
    {
        aRTrackedImageManager = gameObject.GetComponent<ARTrackedImageManager>();
        aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach(ARTrackedImage image in obj.added)
        {
            dog = Instantiate(characterPrefab, image.transform);
            dog.transform.position += prefabOffset;
        }
    }
}
