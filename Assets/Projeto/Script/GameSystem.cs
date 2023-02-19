using UnityEngine;
using GameInterfaces;
using GameEnuns;

namespace GameSystem
{
    [System.Serializable]
    public class CameraMove: RotCamAng
    {
        [SerializeField] Transform transfPl;
        [SerializeField] Transform transfRefPlayerPai;
        [SerializeField] float timeMoveCam;
        [SerializeField] float yRot;
        [SerializeField] float xRot;
        [SerializeField] float velocityRotCam;
        [SerializeField] float velocityGiro;
        [SerializeField] float velocityGiroVertical;
        [SerializeField] bool isCamController;

        public void AwakeSettings()
        {
            yRot = transfPl.eulerAngles.y;
        }

        public void IsCamController(bool active)
        {
            isCamController = active;
        }

        public float GetYRot()
        {
            return yRot;
        }

        public void CameraGiro()
        {
            if (isCamController)
            {
                yRot += velocityGiro * Input.GetAxis("Mouse X") * Time.deltaTime;
                xRot += -velocityGiroVertical * Input.GetAxis("Mouse Y") * Time.deltaTime;
                xRot = Mathf.Clamp(xRot, -30f,30f);
            }
        }

        public void RefreshCam()
        {
            transfRefPlayerPai.position = Vector3.Lerp(transfRefPlayerPai.position, transfPl.position, timeMoveCam * Time.deltaTime);
            transfRefPlayerPai.localRotation = Quaternion.Euler(xRot, yRot, 0f);

        }
    }

    [System.Serializable]
    public class PlayerBase {
        [SerializeField] RotCamAng rotCamAng;
        [SerializeField] PlayerState plState;
        [SerializeField] Transform transfPl;
        [SerializeField] Animator ani;
        [SerializeField] CharacterController charController;
        [SerializeField] FixedJoystick fixJoy;
        [SerializeField] Vector3 dir;
        [SerializeField] float gravit;
        [SerializeField] float timeInfluenciaLayer;
        [SerializeField] bool pegar;
        [SerializeField] float infuenciaPegar;
        [SerializeField] float velocityMovePlayer;
        [SerializeField] float velocityGiro;

        public void StartSettings(RotCamAng camMove)
        {
            rotCamAng = camMove;
        }

        public void Mov()
        {
            if (fixJoy.Vertical > 0.1f || Input.GetAxis("Vertical") > 0.1f)
            {
                ani.SetBool("moverFrente", true);
                ani.SetBool("moverTraz", false);
            }
            else if (fixJoy.Vertical < -0.1f || Input.GetAxis("Vertical") < -0.1f)
            {
                ani.SetBool("moverFrente", false);
                ani.SetBool("moverTraz", true);
            }
            else
            {
                ani.SetBool("moverFrente", false);
                ani.SetBool("moverTraz", false);
            }
        }

     
        public void PegarSetInfluencia()
        {
            if (pegar) infuenciaPegar = Mathf.Lerp(infuenciaPegar, 1f, timeInfluenciaLayer * Time.deltaTime);
            else infuenciaPegar = Mathf.Lerp(infuenciaPegar, 0f, timeInfluenciaLayer * Time.deltaTime);
            ani.SetLayerWeight(1, infuenciaPegar);
        }

        public void PlayerMove()
        {
            float dirY = 0f;

            if (fixJoy.Vertical > 0.1f || Input.GetAxis("Vertical") > 0.1f) dirY = 1f;
            else if (fixJoy.Vertical < -0.1f || Input.GetAxis("Vertical") < -0.1f) dirY = -1f;
            dir = new Vector3(0f,0f,dirY);

            dir = transfPl.TransformDirection(dir);
            dir *= velocityMovePlayer;
            dir.y -= gravit * Time.deltaTime;
            charController.Move(dir * Time.deltaTime);

           if (dir.z!=0f) transfPl.rotation = Quaternion.Euler(0f, Mathf.LerpAngle(transfPl.eulerAngles.y, rotCamAng.GetYRot(), velocityGiro * Time.deltaTime), 0f);

        }

        public void MovePlayer()
        {
            switch (plState)
            {
                case PlayerState.emPe:
                    PlayerMove();
                break;

            }
        }

    }
}
