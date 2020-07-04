using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    ParticleSystem pSystem;
    List<ParticleCollisionEvent> collisionEvent = new List<ParticleCollisionEvent>();
    public GameObject BloodSplatPrefabCollision;
    public GameObject BackgroundBloodSplatPrefab;
    private AudioSource audio;
    int[] rotations = new int[4] { 0, 90, 180, 270 };
    ParticleSystem.Particle[] particles;
    private bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        pSystem = GetComponent<ParticleSystem>();
        particles  = new ParticleSystem.Particle[pSystem.main.maxParticles];
        InvokeRepeating("SetBloodPixels", 0, 0.07f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
       
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    //IEnumerator ParticleCheckLoop()
    //{

    //}

    void SetBloodPixels()
    {
        if(isOn)
        {
            int numParticlesAlive = pSystem.GetParticles(particles);
            for (int i = 0; i < numParticlesAlive; i++)
            {
                if (particles[i].remainingLifetime < 0.4f)
                {
                    //int randomRot = Random.Range(0, 4);
                    //blood.transform.position = new Vector2(blood.transform.position.x, blood.transform.position.y - Time.deltaTime * 10);
                    if (i % 8 == 0)
                    {

                        Instantiate(BackgroundBloodSplatPrefab, particles[i].position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0), Quaternion.Euler(0, 0, 0), null);

                    }
                }
            }
        }
       
    }
    public void ParticlesOn()
    {
        isOn = true;
        audio.Play();

    }
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(pSystem, other, collisionEvent);
        
        int count = collisionEvent.Count;
        
        for(int i = 0; i < count; i++)
        {
            if(i % 16 == 0)
            {
                int randomRot = Random.Range(0, 4);
                Instantiate(BloodSplatPrefabCollision, collisionEvent[i].intersection + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0), Quaternion.Euler(0, 0, rotations[randomRot]), null);
            }
            
        }
    }
}
