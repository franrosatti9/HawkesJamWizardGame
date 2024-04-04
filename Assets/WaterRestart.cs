using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRestart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            Debug.Log("GLU GLU GLU");
            GameManager.Instance.RestartToCheckpoint();
        }
    }
}
