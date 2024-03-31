using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1WinCheckpoint : MonoBehaviour
{
    public GameObject winScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
