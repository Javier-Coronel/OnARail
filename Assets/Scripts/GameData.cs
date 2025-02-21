using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Cinemachine.CinemachinePathBase;
using UnityEngine.UIElements;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    public CinemachineSmoothPath mainPath;
    public CinemachineDollyCart playerCart;
    public List<GameObject> objectsToDelete;
    public SpawnPosition actualSpawnPosition;
    public Texture2D cursor;
    public UIDocument PauseMenu;
    public bool pauseMenuActivated;
    public AudioMixer audioMixer;
    public AudioSource sfxSource;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start(){
        UnityEngine.Cursor.SetCursor(cursor,Vector2.one*cursor.width/2,CursorMode.ForceSoftware);
        SetSound();
        PauseMenu.enabled=false;
    }
    void Update()
    {
        bool menu = Input.GetKeyDown(KeyCode.Escape);
        if(pauseMenuActivated){
            float musicVolume = PauseMenu.rootVisualElement[0].Q<GroupBox>("StartingMenu").Q<Slider>("MusicVolume").value/100;
            float sfxVolume = PauseMenu.rootVisualElement[0].Q<GroupBox>("StartingMenu").Q<Slider>("SFXVolume").value/100;
            PlayerPrefs.SetFloat("SFXVolume",sfxVolume);
            PlayerPrefs.SetFloat("MusicVolume",musicVolume);
            SetSound();
            if(menu)BackToTheGame();
        }else{
            if(menu)ActivateMenu();
        }
    }
    public void SetSound(){
        audioMixer.SetFloat("SFXVolume",PlayerPrefs.GetFloat("SFXVolume",0.5f)>Mathf.Epsilon?30*Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume",0.5f)):-80);
        Debug.Log(20*Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume",1)));
        audioMixer.SetFloat("MusicVolume",PlayerPrefs.GetFloat("MusicVolume",0.5f)>Mathf.Epsilon?30*Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume",0.5f)):-80);
    }
    public float getPosition(CinemachinePathBase.PositionUnits typeOfUnit)
    {
        CinemachinePathBase.PositionUnits actualUnit = playerCart.m_PositionUnits;
        playerCart.m_PositionUnits = typeOfUnit;
        float position = playerCart.m_Position;
        playerCart.m_PositionUnits = actualUnit;
        return position;
    }
    public void respawn()
    {
        playerCart.m_Position = actualSpawnPosition ? actualSpawnPosition.position:0;
        playerCart.GetComponentInChildren<MovementAndControl>().transform.localPosition=Vector3.up*0.5f;
        deleteTemporalObjects();
    }
    public void deleteTemporalObjects()
    {
        
        while (objectsToDelete.Count>0)
        {
            Destroy(objectsToDelete[0]);
            objectsToDelete.RemoveAt(0);
        }
    }
    public void ActivateMenu()
    {
        pauseMenuActivated = true;
        PauseMenu.enabled = true;
        PauseMenu.rootVisualElement[0].visible = true;
        PauseMenu.rootVisualElement[0].Q<GroupBox>("StartingMenu").visible = true;
        PauseMenu.rootVisualElement[0].Q<GroupBox>("StartingMenu").Q<Button>("Back").clicked += BackToTheGame;
        PauseMenu.rootVisualElement[0].Q<GroupBox>("StartingMenu").Q<Button>("Exit").clicked += ()=>{
                SceneManager.LoadScene(0);
                Destroy(gameObject);
            };
        PauseMenu.rootVisualElement[0].Q<GroupBox>("StartingMenu").Q<Slider>("SFXVolume").value = PlayerPrefs.GetFloat("SFXVolume",0.5f)*100;
        PauseMenu.rootVisualElement[0].Q<GroupBox>("StartingMenu").Q<Slider>("MusicVolume").value=PlayerPrefs.GetFloat("MusicVolume",0.5f)*100;
        Time.timeScale = Mathf.Epsilon;
    }
    public void BackToTheGame(){
        pauseMenuActivated = false;
        PauseMenu.rootVisualElement[0].Q<GroupBox>("StartingMenu").visible = true;
        PauseMenu.rootVisualElement[0].visible=false;
        PauseMenu.enabled = false;
        Time.timeScale = 1;
    }
    public void PlaySound(AudioClip clip, float volume = 1, float pitch = 1){
        sfxSource.volume=volume;
        sfxSource.pitch=pitch;
        sfxSource.clip = clip;
        sfxSource.Play();
    }
}
