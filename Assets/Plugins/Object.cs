using UnityEngine;
using System.Collections;

public class Object : MonoBehaviour
{
    public object_type type;
    public Transform object1;
    public AudioClip[] sound;
    public int weapon_num;

    //[HideInInspector]
    public Transform PointSpawn;

    public enum object_type
    {
        armor = 0,
        health = 1,
        weapon = 2,
        parachute = 3,
        bullets = 4
    }


    void Start()
    {
    }//Start

    void Update()
    {
        if (object1)
        {
            object1.transform.eulerAngles += new Vector3(0, 50 * Time.deltaTime, 0);
        }//object
    }//Update

    public void OnTriggerEnter(Collider _collision)
    {
        if (_collision && _collision.GetComponent<Man>() && _collision.GetComponent<Man>().player && _collision.GetComponent<Man>().doing != "sit_vehicle")
        {
            Man temp_script = _collision.GetComponent<Man>();
            if (type == object_type.armor)
            {
                temp_script.GiveArmor(500);
                temp_script.player_sc.SpawnObjectsScript.CountObjInScene -= 1;
                temp_script.player_sc.SpawnObjectsScript.SpawnPoints.Add(PointSpawn);
            }
            else if (type == object_type.health)
            {
                temp_script.AddHealth(10000);
                temp_script.player_sc.SpawnObjectsScript.CountObjInScene -= 1;
                temp_script.player_sc.SpawnObjectsScript.SpawnPoints.Add(PointSpawn);
            }
            else if (type == object_type.weapon)
            {
                _collision.GetComponent<Weapon>().WeaponGive(weapon_num, false, 0, 0);
                _collision.GetComponent<Weapon>().WeaponSelectType(weapon_num);
            }
            else if (type == object_type.parachute) temp_script.GiveParachute();
            int i = (int)type;

            if(type == object_type.bullets)
            {
                _collision.GetComponent<Weapon>().weapon[_collision.GetComponent<Weapon>().weapon_index].clips += 5;
                temp_script.player_sc.SpawnObjectsScript.CountObjInScene -= 1;
                temp_script.player_sc.SpawnObjectsScript.SpawnPoints.Add(PointSpawn);
                i = 2;
            }

            AudioSource.PlayClipAtPoint(sound[i], transform.position, 1);
            Destroy(gameObject);
        }//_collision
    }//OnTriggerEnter
}