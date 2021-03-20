using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GJgame
{
    public class Movement : MonoBehaviour
    {
        public Rigidbody Hands;

        private InputAction _movementAction;

        private InputAction _grabAction, _detachAction;

        [SerializeField]
        private DefaultMovement Controls;

        private Vector2 _oldInput;

        public Vector2 DirectionInput;

        public float Speed = 1;

        public float RunningSpeed = 3;

        public float RotationSpeed = 30;

        private bool _cartAttached, _canAttachCart;

        private CharacterController _controller;

        private Quaternion _targetRotation;

        private CartMovement _cart;

        public Transform ControlCamera;
        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            Controls = new DefaultMovement();
            Controls.Enable();
            _movementAction = Controls.Movement.MoveDirection;
            Controls.Interactions.Activate.performed += Activate_performed;
            Controls.Interactions.Detach.performed += Detach_performed;
        }

        private void Detach_performed(InputAction.CallbackContext obj)
        {
            if (_cartAttached && _cart)
            {
                _cart.DeatachCart();
                _cartAttached = false;
            }
        }

        private void Activate_performed(InputAction.CallbackContext obj)
        {
            if (!_cartAttached && _canAttachCart)
            {
                _cart.AttachCartToPlayer();
                _cartAttached = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            DirectionInput = _movementAction.ReadValue<Vector2>();
            var forward = Vector3.ProjectOnPlane(ControlCamera.transform.forward, Vector3.up);
            var inputDir = new Vector3(DirectionInput.x, 0, DirectionInput.y);
            forward = Quaternion.LookRotation(forward) * inputDir;
            var curSpeed = _cartAttached ? Speed: RunningSpeed;
            _controller.SimpleMove(forward * inputDir.magnitude * curSpeed + Vector3.down);// * Time.deltaTime);

            if (inputDir.sqrMagnitude > 0)
                _targetRotation = Quaternion.LookRotation(forward);
            else
                _targetRotation = transform.rotation;

            if (_targetRotation != transform.rotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, RotationSpeed * Time.deltaTime);
            }
        }

        public void SetCartAvailable(CartMovement cart, bool isAvailable)
        {
            _cart = cart;
            _canAttachCart = isAvailable;
        }
        public void SetCartState(bool state)
        {
            if (state)
            {
                _controller.center = new Vector3(0, 1.2f, 1);
                _controller.radius = 1f;
            }
            else
            {
                _controller.center = new Vector3(0, 1.2f, 0);
                _controller.radius = 0.5f;
            }
        }
    }
}