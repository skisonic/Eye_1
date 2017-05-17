using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    float lifetime;

    public enum gunColor { red, green, blue, yellow };
    public Material[] gunMats;
    public Material[] spriteMats;
    public gunColor ec; //enemy color

    const float LIFE_TIME = 15.0f;
    int hp = 1;
    Color start_color;

    public GameObject enemy_death_pf;
    public GameObject home;
    public GameObject spawner;

    // lerp size as dropping?
    // Use this for initialization
    void Start()
    {
        Rigidbody rb;
        SpriteRenderer srend;
        lifetime = LIFE_TIME;
        rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material = gunMats[(int)ec];
        gameObject.name += "_" + ec.ToString();
        hp *= (int)rb.drag / 2;
        if (hp <= 0)
            hp = 1;
        start_color = GetComponent<Renderer>().material.color;

        srend = GetComponentInChildren<SpriteRenderer>();
        srend.sprite = spawner.GetComponent<SpawnEnemies>().enemySprites[(int)ec];
        //Debug.Log("drag = " + rb.drag + "hp = " + hp);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if(hp <= 0)
        {
            Die();
        }
        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void addLifetime(float addLife)
    {
        lifetime += addLife;
    }

    public void TakeDamage()
    {
        hp--;
        StopCoroutine(FlashWhite());
        StartCoroutine(FlashWhite());
        //Debug.Log("drag = " + rb.drag + "hp = " + hp);
    }

    void Die()
    {
        home.GetComponent<Home_Damage_Follow>().score++;
        Destroy(gameObject);

        GameObject death = Instantiate(enemy_death_pf, transform.position, Quaternion.identity) as GameObject;
    }


    IEnumerator FlashWhite()
    {
        Renderer rend;
        SpriteRenderer srend;
        rend = GetComponent<Renderer>();
        srend = GetComponentInChildren<SpriteRenderer>();
        if (rend.enabled)
        {
            rend.material.color = Color.white;
            yield return new WaitForSeconds(0.02f);
            rend.material.color = start_color;
            yield return new WaitForSeconds(0.01f);
            rend.material.color = Color.white;
            yield return new WaitForSeconds(0.02f);
            rend.material.color = start_color;
            yield return null;
        }
        if (srend.enabled)
        {
            srend.material = spriteMats[1];
            yield return new WaitForSeconds(0.02f);
            srend.material = spriteMats[0];
            yield return new WaitForSeconds(0.01f);
            srend.material = spriteMats[1];
            yield return new WaitForSeconds(0.02f);
            srend.material = spriteMats[0];
            yield return null;
        }
    }
}