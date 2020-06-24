//#define PLAY_MUSIC
using UnityEngine;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{
	static AudioController _instance;
	AudioListener mAudioListener;
    Camera mainCamera;
    GameObject prefab;
    private string[] mPoolInitName = new string[] { "Bomb", "Circle", "Click", "Die", "Release", "Score1", "Score2", "Stop", "Touch" };

    private float bgmVolume = 1f;
    private float soundVolume = 1f;

    public static AudioController Instance {
		get {
			if (!_instance) {
				_instance = GameObject.FindObjectOfType (typeof(AudioController)) as AudioController;
				if (!_instance) {
					GameObject am = new GameObject ("AudioController");
					_instance = am.AddComponent (typeof(AudioController)) as AudioController;
					_instance.mAudioListener = am.AddComponent<AudioListener> ();
				}
			}
			return _instance;
		}
	}
    
	void Awake ()
	{
        DontDestroyOnLoad(this);
    }

    Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    AudioClip GetSound(string filename)
    {
        AudioClip audio = null;
        if (sounds.TryGetValue(filename, out audio))
        {
            return audio;
        }
        audio = Resources.Load<AudioClip>(filename);
        sounds.Add(filename, audio);
        return audio;
    }

    public string bgmPlaying;
    AudioSource mBgMusic;
    public AudioSource PlayBackgroundMusic(string musicName, bool loop)
    {
        AudioClip bgMusic = GetSound("Sounds/" + musicName);
        if (bgMusic != null)
        {
            if (mBgMusic == null)
            {
                GameObject musicObj = new GameObject("BgMusic");
                musicObj.transform.parent = transform;
                mBgMusic = musicObj.AddComponent<AudioSource>();
            }
            if (mBgMusic != null && mBgMusic.enabled)
            {
                mBgMusic.clip = bgMusic;
                mBgMusic.loop = loop;
                mBgMusic.pitch = 1f;
                mBgMusic.volume = bgmVolume;
                mBgMusic.Play();
                bgmPlaying = musicName;
                return mBgMusic;
            }
        }
        else
        {
            Debug.Log("@@ can't load music " + musicName);
        }
        return null;
    }

    public AudioSource PlayUISound(string soundname)
    {
        AudioClip audio = GetSound("Sounds/" + soundname);
        return PlaySound(audio, soundVolume);
    }

    AudioSource mUISound;
    AudioSource PlaySound(AudioClip clip, float volume)
    {
        if (mUISound == null)
        {
            GameObject soundObj = new GameObject("UISound");
            soundObj.transform.parent = transform;
            mUISound = soundObj.AddComponent<AudioSource>();
        }
        mUISound.pitch = 1f;
        mUISound.PlayOneShot(clip, volume);
        return mUISound;
    }
}
