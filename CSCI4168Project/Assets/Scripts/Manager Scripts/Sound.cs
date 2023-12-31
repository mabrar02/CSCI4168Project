using UnityEngine;

/*
 * class represents a sound clip to be used by audio manager
 */
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
    public bool loop;
}
