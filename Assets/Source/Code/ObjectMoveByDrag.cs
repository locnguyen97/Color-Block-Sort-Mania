using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectMoveByDrag : MonoBehaviour
{
    [SerializeField] List<GameObject> particleVFXs;
    public int id;

    private Vector3 startPos;
    private Target target;

    private void OnEnable()
    {
        startPos = transform.position;
    }

    public void PickUp()
    {
        //transform.rotation = new Quaternion(0,0,0,0);
    }

    public void CheckOnMouseUp()
    {
        //transform.position = startPos;
        if (target)
        {
            if (target.id == -1)
            {
                transform.position = target.transform.position;
                startPos = transform.position;
            }
            else
            {
                if (target.id == id)
                {
                    GameObject explosion = Instantiate(particleVFXs[Random.Range(0,particleVFXs.Count)], transform.position, transform.rotation);
                    Destroy(explosion, .75f);
                    transform.position = target.transform.position;
                    GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].RemoveObject(target.gameObject);
                    GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].RemoveObject(gameObject);
                    GameManager.Instance.CheckLevelUp();
                }
            }
        }
        else
        {
            transform.position = startPos;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var t = collision.transform.GetComponent<Target>();
        if (t!= null)
        {
            if (!t.hasSlot)
            {
                t.hasSlot = true;
                target = t;
            }
            
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (target != null)
        {
            if (collision.transform ==target.transform )
            {
                target.hasSlot = false;
                target = null;
            }
        }
        
    }
}
