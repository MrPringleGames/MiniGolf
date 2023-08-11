using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    public void ChangeToScene1()
    {
        SceneManager.LoadScene(1); // Load scene with index 1
    }
}
