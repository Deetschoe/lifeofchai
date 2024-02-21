using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeAttackSounds : MonoBehaviour
{
    public AudioClip[] enemyAttackClips;
    public AudioClip[] gameObjectScreamClips;
    public float proximityThreshold = 5f;

    private AudioSource enemyAudioSource;
    private AudioSource gameObjectAudioSource;
    private bool isPlayingEnemyAudio = false;
    private bool isPlayingGameObjectAudio = false;

    void Start()
    {
        // Ensure there is an AudioSource component attached to the GameObject for enemy audio
        enemyAudioSource = GetComponent<AudioSource>();
        if (enemyAudioSource == null)
        {
            enemyAudioSource = gameObject.AddComponent<AudioSource>();
        }

        // Ensure there is an AudioSource component attached to the GameObject for game object audio
        gameObjectAudioSource = gameObject.AddComponent<AudioSource>();

        // Start playing enemy audio when the script is first initialized
        PlayEnemyAttackAudio();
    }

    void Update()
    {
        // Check for nearby "Enemy" objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            // Check proximity to the enemy
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < proximityThreshold && !isPlayingEnemyAudio)
            {
                // Play enemy attack audio on the enemy
                PlayEnemyAttackAudio();

                // Set a flag to prevent playing enemy audio until the current clip finishes
                isPlayingEnemyAudio = true;

                // Invoke a method to reset the flag after the enemy audio clip duration
                float enemyClipDuration = enemyAudioSource.clip.length;
                Invoke("ResetIsPlayingEnemyAudio", enemyClipDuration);
            }

            if (distance < proximityThreshold && !isPlayingGameObjectAudio)
            {
                // Play game object scream audio
                PlayGameObjectScreamAudio();

                // Set a flag to prevent playing game object audio until the current clip finishes
                isPlayingGameObjectAudio = true;

                // Invoke a method to reset the flag after the game object audio clip duration
                float gameObjectClipDuration = gameObjectAudioSource.clip.length;
                Invoke("ResetIsPlayingGameObjectAudio", gameObjectClipDuration);
            }
        }
    }

    void PlayEnemyAttackAudio()
    {
        // Pick a random enemy attack audio clip from the array
        int randomIndex = Random.Range(0, enemyAttackClips.Length);
        AudioClip randomClip = enemyAttackClips[randomIndex];

        // Assign the chosen clip to the enemy AudioSource
        enemyAudioSource.clip = randomClip;

        // Play the enemy audio
        enemyAudioSource.Play();
    }

    void PlayGameObjectScreamAudio()
    {
        // Pick a random game object scream audio clip from the array
        int randomIndex = Random.Range(0, gameObjectScreamClips.Length);
        AudioClip randomClip = gameObjectScreamClips[randomIndex];

        // Assign the chosen clip to the game object AudioSource
        gameObjectAudioSource.clip = randomClip;

        // Play the game object audio
        gameObjectAudioSource.Play();
    }

    void ResetIsPlayingEnemyAudio()
    {
        // Reset the flag to allow playing enemy audio again
        isPlayingEnemyAudio = false;
    }

    void ResetIsPlayingGameObjectAudio()
    {
        // Reset the flag to allow playing game object audio again
        isPlayingGameObjectAudio = false;
    }
}
