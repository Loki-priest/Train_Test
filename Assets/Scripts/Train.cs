using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField]
    private GameObject[] wheels;

    [SerializeField]
    private GameObject body;

    public float speed;
    float newSpeed;

    [SerializeField]
    private float maxSpeed = 30;

    public WayPoint nextWaypoint;

    void Start()
    {
        newSpeed = speed;
        GuiMgr.Instance.speed.text = speed.ToString("0.0") + " km/h";
    }

    void OnTriggerEnter(Collider other)
    {
        GameMgr.Instance.isOnStation = true;
        GameMgr.Instance.isRewardGot = false;
        GuiMgr.Instance.stop.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        GameMgr.Instance.isOnStation = false;
        GameMgr.Instance.isRewardGot = true;
        GuiMgr.Instance.get.SetActive(false);
        GuiMgr.Instance.stop.SetActive(false);
    }

    void ChangeSpeed()
    {
        speed += (newSpeed-speed)*Time.deltaTime;
        GuiMgr.Instance.speed.text = speed.ToString("0.0") + " km/h";
        if(speed<1f && GameMgr.Instance.isOnStation && !GameMgr.Instance.isRewardGot)
        {
            GuiMgr.Instance.get.SetActive(true);
            GuiMgr.Instance.stop.SetActive(false);
        }
    }

    public void ApplySpeed(float value)
    {
        newSpeed = maxSpeed * value;
    }

    void RotateWheels()
    {
        foreach (GameObject a in wheels)
        {
            a.transform.Rotate(0, 0, -speed*Time.timeScale);
        }
    }

    void Moving()
    {
        //transform.LookAt(nextPoint.transform);
        var targetRotation = Quaternion.LookRotation(/*nextPoint*/nextWaypoint.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3 * Time.deltaTime); //5
        
        transform.position += transform.forward * speed * Time.deltaTime;

        //колеса
        RotateWheels();
        //покачивание
        body.transform.localRotation = Quaternion.Slerp(body.transform.localRotation,Quaternion.Euler(0, 0, Random.Range(-2, 2)), speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, /*nextPoint*/nextWaypoint.transform.position) <= 2.0f)
        {
            nextWaypoint = nextWaypoint.nextWaypoint;
        }

    }

    void Update()
    {
        ChangeSpeed();
        Moving();
    }

}
