using DefaultNamespace;
using GameResources;
using UnityEngine.Audio;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private ResourcesController _resourcesController;
    [SerializeField] private float sustainBorder;
    [SerializeField] private float borderToVBadSound;
    [SerializeField] private float pointsToGoodSound;
    [SerializeField] private float pointsToVGoodSound;
    [SerializeField] AudioMixer mixer;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject board;

    private string[] MusicVolume =
    {
        "NeutralVolume",
        "GoodVolume",
        "VGoodVolume",
        "BadVolume",
        "VBadVolume",
        "GameMusicVolume"
    };

    public float sustenValue;
    public float cityPoints;


    private void Awake()
    {
        _gameManager.GameEnded += OnGameEnded;
        _gameManager.GameRestarts += OnGameRestarts;
    }

    private void OnDestroy()
    {
        _gameManager.GameEnded -= OnGameEnded;
        _gameManager.GameRestarts -= OnGameRestarts;
    }

    private void Start()
    {
        RestartMusic();
    }

    private void OnGameRestarts()
    {
        RestartMusic();
    }

    private void OnGameEnded()
    {
        SetSound(-10, "EndingVolume");
        SetSound(-80, MusicVolume[5]);
    }

    private void FixedUpdate()
    {
        cityPoints = _resourcesController.GetResourcePoints(BoardSide.City);
        sustenValue = Mathf.Abs( Mathf.Round(board.transform.eulerAngles.z));

           
            MusicSet(Sustencounter());
          
    }

    int Sustencounter()
    {
        Debug.Log(sustenValue + "tilt");
        Debug.Log(cityPoints + "citypoints");
        if (sustenValue < sustainBorder && cityPoints < pointsToGoodSound)
            return 0; //Neutral
        if (sustenValue < sustainBorder && cityPoints > pointsToGoodSound && cityPoints <pointsToVGoodSound)
            return 1; //Good
        if (sustenValue < sustainBorder && cityPoints > pointsToVGoodSound)
            return 2; //VGood
        if (sustenValue > sustainBorder)
            return 3; // Bad
        if (sustenValue > borderToVBadSound)
            return 4; //VBad

        return 0;
    }

    void MusicSet(int track)
    {
        for (int i = 0; i < MusicVolume.Length - 1; i++)
        {
            SetSound(-80, MusicVolume[i]);
        }

        SetSound(0, MusicVolume[track]);
    }

    void SetSound(float soundLevel, string valueName)
    {
        mixer.SetFloat(valueName, soundLevel);
    }

    private void RestartMusic()
    {
        SetSound(-80, "EndingVolume");
        MusicSet(0);
        SetSound(0, MusicVolume[5]);
    }
}