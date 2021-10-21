using System.Collections;
using GameResources;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private ResourcesController _resourcesController;
    public UnityEngine.Audio.AudioMixer mixer;
    public bool Good;

    private string[] MusicVolume =
    {
        "NeutralVolume",
        "GoodVolume",
        "VGoodVolume",
        "BadVolume",
        "VBadVolume",
        "MasterVolume"
    };

    public void SetSound(float soundLevel, string valueName)
    {
        mixer.SetFloat(valueName, soundLevel);
    }

    private void Start()
    {
        MusicSet(0);
        SetSound(0, MusicVolume[5]);
    }

    private void FixedUpdate()
    {
        MusicSet(Sustencounter());
    }

    int Sustencounter()
    {
        //need to adjust
        float cityPoints = _resourcesController.GetResourcePoints(DefaultNamespace.BoardSide.City);
        float naturePoints = _resourcesController.GetResourcePoints(DefaultNamespace.BoardSide.Nature);
        float sustenValue = Mathf.Abs(cityPoints - naturePoints);
        Debug.Log(sustenValue);
        if (sustenValue > -1 && sustenValue < 1)
            return 0; //Neutral
        else if (sustenValue < 1 && cityPoints > 10)
            return 1; //Good
        else if (sustenValue < 1 && cityPoints > 30)
            return 2; //VGood
        else if (sustenValue > 1 && sustenValue < 10)
            return 3; // Bad
        else if (sustenValue > 10 && sustenValue < 20)
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
}