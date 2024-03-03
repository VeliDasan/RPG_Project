using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematiks
{
    public class CinematiksTrigger : MonoBehaviour
    {

        bool alreadyTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if(alreadyTriggered==false && other.gameObject.tag == "Player")
            {
                alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
                GetComponent<BoxCollider>().enabled = false;
            }
            
        }

    }
}

