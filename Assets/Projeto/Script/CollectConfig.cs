using UnityEngine;

public class CollectConfig : MonoBehaviour
{
    [SerializeField] bool actionGema;
    [SerializeField] float dist;
    [SerializeField] bool collected;
    [Header("Anime")]
    [SerializeField] bool animeStart;
    [SerializeField] float velocityAni;
    [SerializeField] float velocityAng;
    [SerializeField] float distFinishState;
    [SerializeField] float weight;
    [SerializeField] Transform gemaTransform;
    [SerializeField] Transform checkPoint1;
    [SerializeField] Vector3 posInit;
    [SerializeField] UnityEngine.Animations.Rigging.Rig rig;
    [SerializeField] int idAnime;

    public void StartAnime()
    {
        idAnime = 0;
        posInit = PlayerController.transformHand.localPosition;
        PlayerController.getAction = true;
        animeStart = true;
    }

    void Anime()
    {

        Quaternion currentRot = PlayerController.transformPlayer.rotation;
        PlayerController.transformPlayer.LookAt(transform.position);
        Quaternion rotSearch = PlayerController.transformPlayer.rotation;
        PlayerController.transformPlayer.rotation = currentRot;
        PlayerController.transformPlayer.rotation = Quaternion.Lerp(currentRot, rotSearch, velocityAng * Time.deltaTime);
        PlayerController.transformPlayer.eulerAngles = new Vector3(0f, PlayerController.transformPlayer.eulerAngles.y, 0f);


        float currentDist = 10f;
        switch (idAnime)
        {
            case 0:
                weight = Mathf.Lerp(weight, 1.2f, velocityAni * Time.deltaTime);
                weight = Mathf.Clamp(weight, 0f, 1f);
                rig.weight = weight;
                if (weight == 1f) idAnime = 1;
                break;
            case 1:
                PlayerController.transformHand.position = Vector3.Lerp(PlayerController.transformHand.position, checkPoint1.position, velocityAni * Time.deltaTime);
                currentDist = Vector3.Distance(PlayerController.transformHand.position, checkPoint1.position);
                if (currentDist < distFinishState)
                {
                    if (PlayerController.transformGema != null && PlayerController.transformGema == gemaTransform)
                    {
                        PlayerController.transformGema.parent = transform;
                        PlayerController.transformGema = null;
                    }
                    else
                    {
                        PlayerController.transformGema = gemaTransform;
                        gemaTransform.SetParent(PlayerController.transformHandParent);
                    }


                    //else if (PlayerController.transformGema != null)
                    //{
                    //    PlayerController.transformGema.parent = transform;
                    //    gemaTransform.SetParent(PlayerController.transformHandParent);
                    //}
                    //else
                    //{
                    //    gemaTransform.SetParent(PlayerController.transformHandParent);
                    //}
               //     PlayerController.transformGema = gemaTransform;
                    collected = !collected;
                    idAnime = 2;
                }
                break;
            case 2:
                PlayerController.transformHand.localPosition = Vector3.Lerp(PlayerController.transformHand.localPosition, posInit, velocityAni * Time.deltaTime);
                currentDist = Vector3.Distance(PlayerController.transformHand.localPosition, posInit);
                if (currentDist < distFinishState) idAnime = 3;
                break;
            case 3:
                weight = Mathf.Lerp(weight, -0.2f, velocityAni * Time.deltaTime);
            
                weight = Mathf.Clamp(weight, 0f, 1f);
                rig.weight = weight;
                if (weight == 0f)
                {
                    animeStart = false;
                    PlayerController.getAction = false;
                }
                break;
        }
    }

    void CheckDist()
    {
        float checkDist = Vector3.Distance(transform.position, PlayerController.transformPlayer.position);
        if (dist > checkDist)
        {
            if (!actionGema)
            {
                actionGema = true;
                UIButtonsConfig.acitionGetGema += StartAnime;
            }
        }  
        else
        {
            if (actionGema)
            {
                actionGema = false;
                UIButtonsConfig.acitionGetGema -= StartAnime;
            }
        }
    }

    void Direction()
    {
        transform.LookAt(PlayerController.transformPlayer.position);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }

    void Start()
    {
        rig = GameObject.Find("RigHand").GetComponent<UnityEngine.Animations.Rigging.Rig>();    
    }

    void Update()
    {
        CheckDist();
        Direction();
        if (animeStart) Anime();
    }
}
