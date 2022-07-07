using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionGame : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            player.Pause();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            player.Restart();
        }
    }
}
