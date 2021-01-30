using UnityEngine;
using System.Collections;

// Adapted from https://www.zesix.com/2015/02/23/unity-musicmanager-c/
// Different version from the one available at https://github.com/Zesix/Impulse
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;            // Singleton
    [SerializeField]
    private float volume;
    public float Volume
    {
        get
        {
            return source.volume;
        }
        set
        {
            volume = value;
            source.volume = value;
        }
    }

    public MusicPlaylist Playlist;
    public bool Shuffle;

    public RepeatMode Repeat;
    public float FadeDuration;
    public bool PlayOnAwake;

    private AudioSource source;
    private int _counter = 0;


    void Start()
    {
        // If there is no instance of this class, set it.
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject); // Don't destroy this object
            Instance = this;
        }
        else
        {
            Debug.LogError("There is already a Music Manager in the scene.");
            GameObject.Destroy(this);
        }

        // create a game object and add an AudioSource to it, to play music on
        source = gameObject.GetComponent<AudioSource>();
        source.name = "MusicAudioSource";
        source.playOnAwake = false;

        _counter = GetNewTrack();

        if (FadeDuration > 0)
            source.volume = 0f;
        else
            Volume = volume;
        if (Playlist == null)
            return;
        if (Playlist.MusicList.Count > 0) { 
            source.clip = Playlist.MusicList[_counter];
        }
        else
        {
            Debug.LogError("There are no music in the list");
        }


        if (PlayOnAwake)
            Play();
    }

    public void Play()
    {
        StartCoroutine(PlayMusicList());
    }

    public void Stop(bool fade)
    {
        StopAllCoroutines();
        if (fade)
            StartCoroutine(StopWithFade());
        else
            source.Stop();
    }

    public void Next()
    {
        source.Stop();
    }

    public void ChangePlaylist(MusicPlaylist list)
    {
        Playlist = list;
        _counter = 0;
        StopAllCoroutines();
        StartCoroutine(ChangePlaylistE());
    }

    private IEnumerator ChangePlaylistE()
    {
        if (source.isPlaying)
            yield return StartCoroutine(StopWithFade());
        StartCoroutine(PlayMusicList());
    }

    private IEnumerator StopWithFade()
    {
        if (FadeDuration > 0)
        {
            float lerpValue = 0f;
            while (lerpValue < 1f)
            {
                lerpValue += Time.deltaTime / FadeDuration;
                source.volume = Mathf.Lerp(volume, 0f, lerpValue);
                yield return null;
            }
        }
        source.Stop();
    }

    public void PlaySong(AudioClip song)
    {
        StartCoroutine(PlaySongE(song));
    }

    private IEnumerator PlaySongE(AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        StartCoroutine(FadeIn());
        source.Play();
        while (source.isPlaying)
        {
            if (source.clip.length - source.time <= FadeDuration)
            {
                yield return StartCoroutine(FadeOut());
            }
            yield return null;
        }
    }

    
    private IEnumerator PlayMusicList()
    {
        while (true)
        {
            yield return StartCoroutine(PlaySongE(Playlist.MusicList[_counter]));
            if (RepeatMode.Track == Repeat)
            {
            }
            else if (Shuffle)
            {
                int newTrack = GetNewTrack();
                while (newTrack == _counter)
                {
                    newTrack = GetNewTrack();
                }
                _counter = newTrack;

            }
            else
            {
                _counter++;
                if (_counter >= Playlist.MusicList.Count - 1)
                {
                    if (Repeat == RepeatMode.Playlist)
                        _counter = 0;
                    else
                        yield break;
                }
            }
        }
    }

    private IEnumerator FadeOut()
    {
        if (FadeDuration > 0f)
        {
            float startTime = source.clip.length - FadeDuration;
            float lerpValue = 0f;
            while (lerpValue < 1f)
            {
                lerpValue = Mathf.InverseLerp(startTime, source.clip.length, source.time);
                source.volume = Mathf.Lerp(volume, 0f, lerpValue);
                yield return null;
            }
        }
        else
        {
            yield break;
        }
    }

    private IEnumerator FadeIn()
    {
        if (FadeDuration > 0f)
        {
            float lerpValue = 0f;
            while (lerpValue < 1f)
            {
                lerpValue = Mathf.InverseLerp(0f, FadeDuration, source.time);
                source.volume = Mathf.Lerp(0f, volume, lerpValue);
                yield return null;
            }
        }
        else
        {
            yield break;
        }
    }

    private int GetNewTrack()
    {
        return Random.Range(0, Playlist.MusicList.Count);
    }
}
public enum RepeatMode
{
    Playlist,
    Track,
    None
}