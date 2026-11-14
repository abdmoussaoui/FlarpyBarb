using UnityEngine;

// Audio assets live in Resources so the game is ready without Inspector wiring.
public class GameAudio : MonoBehaviour
{
    [Range(0f, 1f)] public float musicVolume = 0.22f;
    [Range(0f, 1f)] public float effectsVolume = 0.7f;
    private AudioSource music;
    private AudioSource effects;
    private AudioClip flap;
    private AudioClip score;
    private AudioClip crash;

    private void Awake()
    {
        effects = gameObject.AddComponent<AudioSource>();
        effects.playOnAwake = false;
        effects.spatialBlend = 0f;
        music = gameObject.AddComponent<AudioSource>();
        music.playOnAwake = false;
        music.spatialBlend = 0f;
        music.loop = true;
        music.volume = musicVolume;
        flap = Resources.Load<AudioClip>("Audio/flap");
        score = Resources.Load<AudioClip>("Audio/score");
        crash = Resources.Load<AudioClip>("Audio/crash");
        music.clip = Resources.Load<AudioClip>("Audio/music");
        if (music.clip != null) music.Play();
    }

    private void Play(AudioClip clip)
    {
        if (clip != null) effects.PlayOneShot(clip, effectsVolume);
    }

    public void Flap() => Play(flap);
    public void Score() => Play(score);
    public void Crash()
    {
        music.Stop();
        effects.Stop();
        Play(crash);
    }
}
