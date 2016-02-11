using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
		public int m_PlayerNumber = 1;

        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
		private bool m_Action;

		private string m_CtrlHorizontal;
		private string m_CtrlVertical;
		private string m_CtrlAction;
		private string m_CtrlCrouch;
		private string m_CtrlJump;
		private string m_CtrlWalk;
        
        private void Start()
        {
			// Setup controller strings
			m_CtrlHorizontal = "Horizontal" + m_PlayerNumber;
			m_CtrlVertical = "Vertical" + m_PlayerNumber;
			m_CtrlAction = "Action" + m_PlayerNumber;
			m_CtrlCrouch = "Crouch" + m_PlayerNumber;
			m_CtrlJump = "Jump" + m_PlayerNumber;
			m_CtrlWalk = "Walk" + m_PlayerNumber;

            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
				m_Jump = Input.GetButton (m_CtrlJump);
            }

			if (!m_Action) 
			{
				m_Action = Input.GetButton (m_CtrlAction);
			} 
			else
			{
				m_Action = false;
			}

//			if (!m_Throw) 
//			{
//				m_Throw = Input.GetButton (m_CtrlThrow);
//			} 
//			else
//			{
//				m_Throw = false;
//			}
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
			float h = Input.GetAxis(m_CtrlHorizontal);
			float v = Input.GetAxis(m_CtrlVertical);
			bool crouch = Input.GetButton(m_CtrlCrouch);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetButton(m_CtrlWalk)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump, m_Action);
            m_Jump = false;
        }
    }
}
