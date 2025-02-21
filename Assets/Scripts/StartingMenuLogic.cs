using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartingMenuLogic : MonoBehaviour
{
    public UIDocument StartMenu;
    // Start is called before the first frame update
    void Start()
    {
        StartMenu.rootVisualElement[0].Q<GroupBox>("StartingMenu").Q<Button>("Exit").clicked += Application.Quit;
        StartMenu.rootVisualElement[0].Q<GroupBox>("StartingMenu").Q<Button>("Play").clicked += ()=>SceneManager.LoadScene(1);
    }
}
