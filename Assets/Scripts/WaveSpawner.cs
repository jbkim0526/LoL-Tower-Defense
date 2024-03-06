using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // TEXT를 사용가능하게함.
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{

    public static int EnemiesKilled = 0;


    public Round[] rounds;

    public Transform spawnPoint;
    public float timeBetweenRounds = 1f;





    [Header ("Shop Canvas Elements")] 
    public Text WelcomeText;
    public Image WelcomImage;
    public Text waveCountdownText;
    public Text MinionDueText;
    public Image MinionDueImage;
    public Text MinionSponText;
    public Image MinionSponImage;

    [Header ("WaveSpawner")]
    public float countdown = 40f;
    private int waveIndex = 0;
    private int roundIndex = 0;
    private int TotalNum;
    

    [Header ("Audio")]
    public AudioClip WelcomeAudio;
    public AudioClip MinionSpawnAudio;
    public AudioClip MinionDueAudio;
    private AudioSource source;
    private float volLowRange = 0.05f;
    private float volHighRange = 0.1f;

    public SceneFader sceneFader;
    private float vol = 2f;
    private Round round;


    private void Start()
    {

        source = GetComponent<AudioSource>();
        StartCoroutine(Welcome());
    }



    void Update()
    {

        
        if (EnemiesKilled < TotalNum)          
        {
            return;

        }

        if (countdown <= 0f)    // countdown이 모두 되면 이부분을 실행하고 return. 
        {


            if(roundIndex == rounds.Length)
            {
                Time.timeScale = 4f;
                sceneFader.Fadeto("EndingCredit");

            }

            /* 초기화 부분. */
            EnemiesKilled = 0;

            
            StartCoroutine(PlayRound());
            PlayerStats.Money += 1000;
            countdown = timeBetweenRounds;
            
            


            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.0}", countdown);
;    }







    IEnumerator PlayRound()
    {

        if(roundIndex == rounds.Length)
        {

            timeBetweenRounds = 45;
        }
        PlayerStats.Rounds++;
        Round round = rounds[roundIndex];
        TotalNum = getTotalEnemyNum(round);
        StartCoroutine(SpawnRound(round));
        roundIndex++;
        source.PlayOneShot(MinionSpawnAudio, vol);
        MinionSponText.enabled = true;
        MinionSponImage.enabled = true;

        yield return new WaitForSeconds(5f);
        MinionSponText.enabled = false;
        MinionSponImage.enabled = false;

    }






    IEnumerator SpawnRound(Round round)      //Round에 해당하는 모든 wave들을 spawn
    {
        waveIndex = 0;
        int wavelength = round.waves.Length;
        for (int i = 0; i < wavelength; i++)
        {
            StartCoroutine(SpawnWave(round.waves[waveIndex]));
            yield return new WaitForSeconds(round.timeBetweenWaves);
            waveIndex++;


            if (waveIndex == wavelength)
            {
                yield return new WaitForSeconds(3f);
            }

        }
        


    }



    IEnumerator SpawnWave(Wave wave)             // round 에 있는 모든 wave를 spawn.
    {

        for (int i = 0; i < wave.count; i++)        // 1번째에 한마리 두번째에 두마리...
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate);    //나중에 변수로 지정하는게 좋음. 
        }


    }


    void SpawnEnemy(GameObject enemy)
    {   
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        
    }


    int getTotalEnemyNum(Round round)
    {
        int temp = 0;

        for (int i = 0; i < round.waves.Length; i++)
        {
            temp += round.waves[i].count;
        }


        return temp;
    }

    IEnumerator Welcome()
    {
        MinionDueText.enabled = false;
        MinionDueImage.enabled = false;
        MinionSponText.enabled = false;
        MinionSponImage.enabled = false;

        

        source.PlayOneShot(WelcomeAudio, vol);


        yield return new WaitForSeconds(5f);   //35~ 
        WelcomeText.enabled = false;
        WelcomImage.enabled = false;
        yield return new WaitForSeconds(5f); //30~35
        MinionDueText.enabled = true;
        MinionDueImage.enabled = true;
        source.PlayOneShot(MinionDueAudio, vol);
        yield return new WaitForSeconds(5f); //25~30
        MinionDueText.enabled = false;
        MinionDueImage.enabled = false;

        yield return new WaitForSeconds(25f);
       
    }



}
