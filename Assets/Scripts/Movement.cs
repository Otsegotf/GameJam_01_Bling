using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GJgame
{
    public class Movement : MonoBehaviour
    {
        private InputAction _movementAction;

        private InputAction _runAction;

        [SerializeField]
        private DefaultMovement Controls;

        private Vector2 _oldInput;

        public Vector2 DirectionInput;

        public float Speed = 1;

        public float RunningSpeed = 3;

        public float RotationSpeed = 30;

        private bool _running = false;

        private CharacterController _controller;

        private Quaternion _targetRotation;

        [SerializeField]
        private Camera _controlCamera;
        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            Controls = new DefaultMovement();
            Controls.Enable();
            _movementAction = Controls.Movement.MoveDirection;
            Controls.Movement.Run.performed += Run_performed; ;
        }

        private void Run_performed(InputAction.CallbackContext obj)
        {
            _running = obj.ReadValueAsButton();
        }


        // Update is called once per frame
        void Update()
        {
            DirectionInput = _movementAction.ReadValue<Vector2>();
            var forward = Vector3.ProjectOnPlane(_controlCamera.transform.forward, Vector3.up);
            var inputDir = new Vector3(DirectionInput.x, 0, DirectionInput.y);
            forward = Quaternion.LookRotation(forward) * inputDir;
            var curSpeed = _running ? RunningSpeed : Speed;
            _controller.Move(forward * inputDir.magnitude * curSpeed * Time.deltaTime);
            if (inputDir.sqrMagnitude > 0)
                _targetRotation = Quaternion.LookRotation(forward);
            else
                _targetRotation = transform.rotation;

            if (_targetRotation != transform.rotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, RotationSpeed * Time.deltaTime);
            }
        }
    }
}