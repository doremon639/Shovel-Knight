using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
public class ExitGame : MonoBehaviour
{
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        Application.Quit(); 
#endif
    }
}