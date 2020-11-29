using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunMode : MonoBehaviour
{
    //recommit
    Transform myGun;
    Transform[] aimPos = new Transform[2];

    public bool PrecisionMode = false;
    public Image aim;

    public float changeDelay = 2f;
    private float count = 0f;
    private bool changeMode = false;

    // Start is called before the first frame update
    void Start()
    {
        myGun = GameObject.Find("SciFiHandGun").transform;

        aimPos[0] = GameObject.Find("AimPos0").transform;
        aimPos[1] = GameObject.Find("AimPos1").transform;

        aim = GameObject.Find("aim").GetComponent<Image>();
        aim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2") && changeMode == false)
        {
            changeMode = true;

            PrecisionMode = !PrecisionMode;

            if (PrecisionMode == false) aim.enabled = false;
        }

        //정밀 모드
        if(PrecisionMode)
        {
            //myGun.position = aimPos[1].position;

            //lerp
            //myGun.position = Vector3.Lerp(myGun.position, aimPos[1].position, Time.deltaTime * 10f);

            //optimization
            if(changeMode) myGun.position = Vector3.Lerp(myGun.position, aimPos[1].position, Time.deltaTime * 10f);
        }

        //기본 모드
        else
        {
            //myGun.position = aimPos[0].position;

            //lerp
            myGun.position = Vector3.Lerp(myGun.position, aimPos[0].position, Time.deltaTime * 10f);

            //optimization
            if(changeMode) myGun.position = Vector3.Lerp(myGun.position, aimPos[0].position, Time.deltaTime * 10f);
        }

        //변경 딜레이
        if (changeMode)
        {
            count += Time.deltaTime;
            
            if(count > changeDelay)
            {
                if(PrecisionMode) aim.enabled = true;

                count = 0;
                changeMode = false;
            }
        }
    }
}
