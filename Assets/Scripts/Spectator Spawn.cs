using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpectatorSpawn : MonoBehaviour
{
    
    public GameObject spectator;
    public GameObject[] spectators;
    private int pulseIndex;
    void Start()
    {
        spectators = new GameObject[90];
        var index = 0;
        pulseIndex = 0;
        for(int i = 0; i < 24; i++)
        {
            spectators[index++] = Instantiate(spectator, new Vector3(-17.25f+i*1.5f, this.gameObject.transform.position.y-0.5f, this.gameObject.transform.position.z-3f), Quaternion.identity, this.gameObject.transform);
        }
        for(int i = 0; i < 23; i++)
        {
            spectators[index++] = Instantiate(spectator, new Vector3(-16.5f+i*1.5f, this.gameObject.transform.position.y+0.2f, this.gameObject.transform.position.z-2f), Quaternion.identity, this.gameObject.transform);
        }
        for(int i = 0; i < 22; i++)
        {
            spectators[index++] = Instantiate(spectator, new Vector3(-15.75f+i*1.5f, this.gameObject.transform.position.y+0.9f, this.gameObject.transform.position.z-1f), Quaternion.identity, this.gameObject.transform);
        }
        for(int i = 0; i < 21; i++)
        {
            spectators[index++] = Instantiate(spectator, new Vector3(-15f+i*1.5f, this.gameObject.transform.position.y+1.6f, this.gameObject.transform.position.z), Quaternion.identity, this.gameObject.transform);
        }
    }

    void Update()
    {
        
    }

    [SerializeField]
    public void DoPulse()
    {
        if(spectators == null || spectators.Length < 1) {
            return;
        }
        if(pulseIndex == 0)
        {
            for(int i = 0; i < 24; i++)
            {
                spectators[i].GetComponentInChildren<PlayableDirector>().Play();
            }
            for(int i = 47; i < 69; i++)
            {
                spectators[i].GetComponentInChildren<PlayableDirector>().Play();
            }
            pulseIndex = 1;
        }
        else
        {
            for(int i = 24; i < 47; i++)
            {
                spectators[i].GetComponentInChildren<PlayableDirector>().Play();
            }
            for(int i = 69; i < spectators.Length; i++)
            {
                spectators[i].GetComponentInChildren<PlayableDirector>().Play();
            }
            pulseIndex = 0;
        }
    }
}
