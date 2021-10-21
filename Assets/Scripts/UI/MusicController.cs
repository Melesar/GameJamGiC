using System.Collections;
using GameResources;
using System.Collections.Generic;
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
    [SerializeField] AudioMixerSnapshot _firstsnap;
    [SerializeField] AudioMixerSnapshot _secondsnap;


    private string[] MusicVolume =
        {
        "NeutralVolume",
        "GoodVolume",
        "VGoodVolume",
        "BadVolume",
        "VBadVolume",
        "MasterVolume"

    };
    private float sustenValue;
        private float cityPoints;
        private float naturePoints;
    private float prevSustannValue;
    private int lastSound;
        

        private void Start()
        {

             MusicSet(0);
           SetSound(0, MusicVolume[5]);


        }

        private void FixedUpdate()
        {
        cityPoints = _resourcesController.GetResourcePoints(DefaultNamespace.BoardSide.City);
        naturePoints = _resourcesController.GetResourcePoints(DefaultNamespace.BoardSide.Nature);
        sustenValue = Mathf.Abs(cityPoints - naturePoints);


        if(sustenValue!= prevSustannValue)
        {
            Debug.Log(Sustencounter() + "VALLLL");
            MusicSet(Sustencounter());
            prevSustannValue = sustenValue;
        }
        
        }

    int Sustencounter()
    {
        //need to adjust
      
      
        Debug.Log(sustenValue);
       
            if (sustenValue < sustainBorder)
                return 0; //Neutral
            else if (sustenValue < sustainBorder && cityPoints > pointsToGoodSound)
                return 1; //Good
            else if (sustenValue < sustainBorder && cityPoints > pointsToVGoodSound)
                return 2; //VGood
            else if (sustenValue > sustainBorder)
                return 3; // Bad
            else if (sustenValue > borderToVBadSound)
                return 4; //VBad

            return 0;
        
    }

    void MusicSet(int track)
    {
        Debug.Log("music change");
        for (int i = 0; i < MusicVolume.Length-1; i++)
        {
            SetSound(-80, MusicVolume[i]);
            Debug.Log("loop" + i);
        }
        SetSound(0, MusicVolume[track]);
        Debug.Log("end" + track);
    }

    void SetSound(float soundLevel, string valueName)
    {
        mixer.SetFloat(valueName, soundLevel);
        
    }
}



        
