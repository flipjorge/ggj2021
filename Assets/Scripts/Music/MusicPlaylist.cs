using UnityEngine;
using System.Collections.Generic;

// Available at https://github.com/Zesix/Impulse
/// <summary>
/// List of music items to be played
/// </summary>
public class MusicPlaylist : MonoBehaviour
{
    public bool Shuffle;
    public List<AudioClip> MusicList;
}