using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    
    [Header("Clips")]
    [SerializeField] private AudioClip transformSfx;
    [FormerlySerializedAs("shootSpellSfx")] [SerializeField] private AudioClip castSpellSfx;

    private Dictionary<AllSfx, AudioClip> sfxDictionary = new Dictionary<AllSfx, AudioClip>();
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        sfxDictionary[AllSfx.Transform] = transformSfx;
        sfxDictionary[AllSfx.CastSpell] = castSpellSfx;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AllSfx sfx)
    {
        sfxSource.PlayOneShot(sfxDictionary[sfx]);
    }

    public void PlaySFX(AudioClip sfx)
    {
        sfxSource.PlayOneShot(sfx);   
    }
}

public enum AllSfx
{
    Transform,
    CastSpell
}
