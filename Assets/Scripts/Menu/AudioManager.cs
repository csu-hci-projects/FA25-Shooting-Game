using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 🔹 persists across scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayButtonClick(AudioClip clip)
    {
        AudioSource source = GetComponent<AudioSource>();
        if (source != null && clip != null)
            source.PlayOneShot(clip);
    }
}
