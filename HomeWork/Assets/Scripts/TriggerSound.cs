
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    [SerializeField] List<AudioClip> sounds = new List<AudioClip>();
    [SerializeField] List<AudioSource> sources = new List<AudioSource>();

    [SerializeField] List<Teleport> teleports = new List<Teleport>();

    private Collider thisCollider;

    private void OnEnable()
    {
        SubscriptionToEnableCollider();
    }

    private void OnDisable()
    {
        UnsubscrideToEnableCollider();
    }

    private void Start()
    {
        thisCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckAvailableSoundAndSources())
        {
            if (other.CompareTag("Player"))
            {
                int numSounds = Random.Range(0, sounds.Count);
                int numSources = Random.Range(0, sources.Count);

                sources[numSources].PlayOneShot(sounds[numSounds]);

                thisCollider.enabled = false;
            }
        }
    }

    private bool CheckAvailableSoundAndSources()
    {
        bool available = true;
        if (sounds.Count == 0)
        {
            Debug.LogError("Not sounds", this);
            available = false;
        }
        if (sources.Count == 0)
        {
            Debug.LogError("Not sources", this);
            available = false;
        }
        return available;
    }

    private void TurnOnCollider()
    {
        thisCollider.enabled = true;
    }

    private void SubscriptionToEnableCollider()
    {
        foreach (var teleport in teleports)
        {
            teleport.OnCollider += TurnOnCollider;
        }
    }

    private void UnsubscrideToEnableCollider()
    {
        foreach (var teleport in teleports)
        {
            teleport.OnCollider -= TurnOnCollider;
        }
    }
}
