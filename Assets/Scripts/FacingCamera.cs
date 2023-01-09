using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    Transform[] children;
    private void Start()
    {
        children = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        for(int i = 0; i < children.Length; i++)
        {
            children[i].rotation = Camera.main.transform.rotation;
        }
    }
}
