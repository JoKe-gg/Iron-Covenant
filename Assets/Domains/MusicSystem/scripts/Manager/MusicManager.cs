using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] private MusicBase music;
    private int _id = 0;
    private float _nextSongStartTime;
    [SerializeField] private AudioSource _effectSource;
    [SerializeField] private AudioSource _musicSource;
    private void Awake()
    {
        if (instance == null)
        {
            _musicSource.loop = false;
            _musicSource.playOnAwake = false; 
            _effectSource.loop = false;
            _effectSource.playOnAwake = false;
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (music.MusicData == null || music.MusicData.Count == 0)
        {
            Destroy(gameObject);
            return;
        }
        PauseManager.instance.OnPauseStatusChanged += OnPauseChanged;
    }
    private void OnPauseChanged(bool value)
    {
        switch (value)
        {
            case true:
                _musicSource.Pause(); 
                break;
            case false:
                _musicSource.UnPause();
                break;
        }
    }
    private void Update()
    {
        if (_nextSongStartTime - Time.time <= 0)
        {
            _musicSource.clip = music.MusicData[_id].Clip;
            _musicSource.Play();
            _nextSongStartTime = Time.time + music.MusicData[_id].Clip.length;
            if (music.MusicData.Count-1 == _id)
            {
                _id = 0;
            }
            else
            {
                _id ++;
            }
        }
    }
    public void PlayEffect(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }
}
