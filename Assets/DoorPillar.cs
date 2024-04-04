using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPillar : MonoBehaviour
{
    
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private float moveTime;
    [SerializeField] private Transform pillar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        StopAllCoroutines();
        StartCoroutine(Move(pillar.transform.localPosition, targetPos, false));
    }

    public void OpenOnce()
    {
        StopAllCoroutines();
        StartCoroutine(Move(pillar.transform.localPosition, targetPos, true));
    }

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(Move(pillar.transform.localPosition, Vector3.zero, false));
    }

    public IEnumerator Move(Vector3 from, Vector3 to, bool destroyWhenFinished)
    {
        float time = 0f;
        // if is opening
        if (to == targetPos)
        {
            time = Utilities.Vector3InverseLerp(targetPos, Vector3.zero, pillar.transform.localPosition);
        }
        else
        {
            time = Utilities.Vector3InverseLerp(Vector3.zero, targetPos, pillar.transform.localPosition);
        }

        time *= moveTime;
        
        float elapsed = 0f;
        while (elapsed < time)
        {
            pillar.localPosition = Vector3.Lerp(from, to, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        pillar.localPosition = Vector3.Lerp(from, to, 1f); // Complete lerp
        
        if(destroyWhenFinished) Destroy(gameObject);
    }
}
