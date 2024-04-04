using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] DoorPillar doorToOpen;
    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private Sprite activatedSprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Trigger?");
            if (other.gameObject.TryGetComponent(out AnimalController animal))
            {
                doorToOpen.OpenOnce();
                visual.sprite = activatedSprite;
                
                // Disable components after activation
                Destroy(GetComponent<BoxCollider2D>());
                Destroy(GetComponent<Rigidbody2D>());
                Destroy(this);
            }
        }
}
