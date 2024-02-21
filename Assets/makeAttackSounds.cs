using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeAttackSounds : MonoBehaviour
{
    public AudioClip[] enemyAttackClips;
    public AudioClip[] gameObjectScreamClips;
    public float proximityThreshold = 5f;
    private AudioSource gameObjectAudioSource;
    private bool isPlayingGameObjectAudio = false;
    private Dictionary<GameObject, float> enemyAudioCooldown = new Dictionary<GameObject, float>();
    private float audioCooldown = 1f; // 3 seconds delay

    void Start()
    {
        gameObjectAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < proximityThreshold)
            {
                PlayEnemyAttackAudio(enemy);
            }

            if (distance < proximityThreshold && !isPlayingGameObjectAudio)
            {
                PlayGameObjectScreamAudio();
                isPlayingGameObjectAudio = true;
                float gameObjectClipDuration = gameObjectAudioSource.clip.length;
                Invoke("ResetIsPlayingGameObjectAudio", gameObjectClipDuration);
            }
        }
    }

    void PlayEnemyAttackAudio(GameObject enemy)
    {
        if (!enemyAudioCooldown.ContainsKey(enemy) || Time.time - enemyAudioCooldown[enemy] >= audioCooldown)
        {
            AudioSource enemyAudioSource = enemy.GetComponent<AudioSource>();
            if (enemyAudioSource == null)
            {
                enemyAudioSource = enemy.AddComponent<AudioSource>();
                enemyAudioSource.volume = 2.0f; // Unity clamps this to max 1.0
            }

            int randomIndex = Random.Range(0, enemyAttackClips.Length);
            AudioClip randomClip = enemyAttackClips[randomIndex];
            enemyAudioSource.clip = randomClip;
            enemyAudioSource.Play();
            enemyAudioCooldown[enemy] = Time.time;
        }
    }

    void PlayGameObjectScreamAudio()
    {
        int randomIndex = Random.Range(0, gameObjectScreamClips.Length);
        AudioClip randomClip = gameObjectScreamClips[randomIndex];
        gameObjectAudioSource.clip = randomClip;
        gameObjectAudioSource.Play();
    }

    void ResetIsPlayingGameObjectAudio()
    {
        isPlayingGameObjectAudio = false;
    }
}
