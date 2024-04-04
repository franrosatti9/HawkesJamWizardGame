using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformingArea : MonoBehaviour
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
        if (other.TryGetComponent(out PlayerController controller))
        {
            GameManager.Instance.SetLastCheckpoint(transform);
            controller.PlayerAbilities.AllowTransformation(true);
        }
        
        // TODO: ANIMATION WHEN ENTERING AREA, MAYBE WITH LEANTWEEN
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController controller))
        {
            GameManager.Instance.SetLastCheckpoint(transform);
            controller.PlayerAbilities.AllowTransformation(false);
        }
    }
}
