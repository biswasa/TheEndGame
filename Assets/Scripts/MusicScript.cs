//Place in an Empty Game Object in the first scene//
//You'll need to edit some of the Event If statements below//

using UnityEngine;

public class MusicScript : MonoBehaviour {

    static MusicScript instance = null;
    private AudioSource audioSource;
    private int songid;
    public AudioClip titlebgm;
    public AudioClip gameoverbgm;
    public AudioClip levelbgm;
    public AudioClip dangerbgm;
    public AudioClip victoryjingle;
    private AudioClip[] bgm;

    int lastsong;

    // Use this for initialization
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            songid = 0;
            lastsong = -1;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (songid == -1) { return; }
        AudioClip mysong = titlebgm;
        switch (songid)
        {
            case 0: { mysong = titlebgm; break; }
            case 1: { mysong = levelbgm; break; }
            case 2: { mysong = dangerbgm; break; }
            case 3: { mysong = victoryjingle; break; }
            case 4: { mysong = gameoverbgm; break; }
        }

        //Plays the new song id//
        if (songid != lastsong)
        {
            lastsong = songid;
            Debug.Log("Music Loaded:" + mysong);
            if (mysong)
            {
                audioSource.clip = mysong;
                if (songid == 1 || songid == 2)
                    { audioSource.loop = true; }
                else
                    { audioSource.loop = false; }
                audioSource.Play();
            }
        }
        //Specific events//

        //Event: Level Up Jingle Ends//
        if (songid == 3 && audioSource.isPlaying == false)
        {
            songid = 1;
        }
        //Title Screen//
        //If titlescreen object exists...
        else if (Application.loadedLevelName == "MainMenu")
        {
            songid = 0;
        }
        //Game Over//
        //If gameover object exists...
        else if (Application.loadedLevelName == "GameOver")
        {
            songid = 4;
        }
        //In Game Events//
        else
        {
            //Reference the Tower and its stat script//
            TowerManager towerStats = GameObject.FindWithTag("Tower").GetComponent<TowerManager>();
            //Event: Level Up//
            //When the tower reaches a new power level...
            if (towerStats.levelUp)
            {
                songid = 3;
				towerStats.levelUp = false;
            }
            else if (songid != 3)
            { 
                //Event: Level Start / HP Not in Danger//
                //If tower object exists and its hp > 10%
                if (towerStats.getPercentageHealth() > 0.1)
                {
                    songid = 1;
                }
                //Event: HP in Danger//
                //If tower hp < 10%
                else
                {
                    songid = 2;
                }
            }
        }

    }
}
