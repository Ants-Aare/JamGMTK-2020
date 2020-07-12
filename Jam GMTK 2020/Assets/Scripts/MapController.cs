using GMTKJAM.Machines;
using GMTKJAM.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public TraumaInducer damageShake;
    public AudioSource damageSound;
    public StatBar healthBar;
    public GameObject enemyPrefab;
    public GameObject whaleBulletPrefab;

    public List<Transform> enemyIcons;
    public List<Transform> whaleBullets;

    public Transform whaleIcon;
    public Transform whaleGunIcon;
    public float enemyEngageDist = 0.5f;
    public float enemySpeed = 0.2f;
    public float whaleForwardSpeed = 0.2f;
    public float enemySpawnDelay = 10;

    public float timer = 0;

    public Reactor reactor;

    public float health = 5;

    // Update is called once per frame
    void FixedUpdate()
    {
        whaleGunIcon.localRotation *= Quaternion.Euler(0, 0, 50 * Time.deltaTime);
        timer += Time.deltaTime;

        if (timer >= 10)
        {
            timer = 0;
            Transform prefab = Instantiate(enemyPrefab).transform;
            prefab.SetParent(transform);
            Vector2 insideUnit = Random.insideUnitCircle.normalized * 5;
            prefab.localPosition = new Vector3(insideUnit.x, insideUnit.y, -0.05f);
            enemyIcons.Add(prefab);
            enemySpawnDelay = Mathf.Max(2, enemySpawnDelay - 1);
        }

        bool isBoosting = false;
        if (reactor.UseResource())
        {
            isBoosting = true;
        }

        for (int i = 0; i < enemyIcons.Count; i++)
        {
            Transform enemy = enemyIcons[i];

            if (isBoosting)
                enemy.localPosition -= new Vector3(0, whaleForwardSpeed, 0) * Time.deltaTime;
            else
            {
                enemy.localPosition -= new Vector3(0, whaleForwardSpeed / 10f, 0) * Time.deltaTime;
            }

            if (Vector3.Distance(whaleIcon.localPosition, enemy.localPosition) > enemyEngageDist)
            {
                enemy.localPosition += (whaleIcon.localPosition - enemy.localPosition).normalized * enemySpeed * Time.deltaTime;
                enemy.LookAt(whaleIcon, transform.forward);
                enemy.localRotation *= Quaternion.Euler(90, 0, 90);

                for (int j = 0; j < whaleBullets.Count; j++)
                {
                    Transform bullet = whaleBullets[j];

                    if (Vector3.Distance(bullet.localPosition, enemy.localPosition) <= 0.2f)
                    {
                        whaleBullets.RemoveAt(j);
                        enemyIcons.RemoveAt(i);
                        Destroy(enemy.gameObject);
                        Destroy(bullet.gameObject);
                        j = whaleBullets.Count;
                        i--;
                    }
                }
            }
            else
            {
                enemyIcons.RemoveAt(i);
                Destroy(enemy.gameObject);
                health = Mathf.Max(0,health-1);
                healthBar.UpdateFillPercent(health/5);
                damageSound.Play();
                StartCoroutine(damageShake.Start());
            }
        }

        for (int i = 0; i < whaleBullets.Count; i++)
        {
            Transform bullet = whaleBullets[i];

            if (isBoosting)
                bullet.localPosition -= new Vector3(0, whaleForwardSpeed, 0) * Time.deltaTime;
            else
            {
                bullet.localPosition -= new Vector3(0, whaleForwardSpeed / 10f, 0) * Time.deltaTime;
            }
            bullet.localPosition += bullet.right * Time.deltaTime * 0.4f;
            bullet.localPosition = new Vector3(bullet.localPosition.x, bullet.localPosition.y, -0.05f);
            if (Vector3.Distance(bullet.localPosition,whaleIcon.localPosition) >= 4)
            {
                whaleBullets.RemoveAt(i);
                Destroy(bullet.gameObject);
                i--;
            }
        }
    }

    public void FireGun()
    {
        Transform prefab = Instantiate(whaleBulletPrefab).transform;
        prefab.SetParent(transform);
        prefab.localPosition = whaleIcon.localPosition;
        prefab.localRotation = whaleGunIcon.localRotation;
        whaleBullets.Add(prefab);

    }
}