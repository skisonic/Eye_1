using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    float lifetime;

    public enum gunColor { red, green, blue, yellow };
    public Material[] gunMats;
    public gunColor ec; //enemy color

    public GameObject enemy_death_pf;
    const float LIFE_TIME = 15.0f;
    int hp = 1;
    Color start_color;
    GameObject home;

    // lerp size as dropping?
    // Use this for initialization
    void Start()
    {
        Rigidbody rb;
        home = GameObject.Find("Home");
        lifetime = LIFE_TIME;
        rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material = gunMats[(int)ec];
        hp *= (int)rb.drag / 2;
        if (hp <= 0)
            hp = 1;
        start_color = GetComponent<Renderer>().material.color;

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
        rend = GetComponent<Renderer>();
        rend.material.color = Color.white;
        yield return new WaitForSeconds(0.02f);
        rend.material.color = start_color;
        yield return new WaitForSeconds(0.01f);
        rend.material.color = Color.white;
        yield return new WaitForSeconds(0.02f);
        rend.material.color = start_color;
        yield return null;
    }
}