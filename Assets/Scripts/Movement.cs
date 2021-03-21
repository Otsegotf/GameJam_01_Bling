using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GJgame
{
    public class Movement : MonoBehaviour
    {
        public Rigidbody Hands;

        public Animator PlayerAnim;

        private InputAction _movementAction;

        private Vector2 _oldInput;

        public Vector2 DirectionInput;

        public float Speed = 1;

        public float RunningSpeed = 3;

        public float RotationSpeed = 30;

        private bool _cartAttached, _canAttachCart;

        private CharacterController _controller;

        private Quaternion _targetRotation;

        public IPickupAble CurrentPickup;

        private IPickupAble _targetPickup;

        public ShopItem CarriedItem;

        public Transform ControlCamera;

        public float MaxPickupDistance = 1f;

        private DefaultMovement _controls;
        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();
            _controls = MainMenuManager.Instance.Controls;
            _movementAction = _controls.Movement.MoveDirection;
            _controls.Interactions.Activate.performed += Activate_performed;
            _controls.Interactions.Detach.performed += Detach_performed;
            SetCartState(false);
        }

        private void OnDestroy()
        {
            if (_controls != null)
            {
                _controls.Interactions.Activate.performed -= Activate_performed;
                _controls.Interactions.Detach.performed -= Detach_performed;
            }
        }

        private void Detach_performed(InputAction.CallbackContext obj)
        {
            if (CurrentPickup != null)
            {
                var comp = (CurrentPickup as Component);
                if (comp == null)
                {
                    CurrentPickup = null;
                    return;
                }
                CurrentPickup.Drop();
            }
        }

        private void Activate_performed(InputAction.CallbackContext obj)
        {
            var objects = Physics.OverlapSphere(Hands.position, 0.3f);
            var collected = new List<IPickupAble>();
            for (int i = 0; i < objects.Length; i++)
            {
                var cart = objects[i].GetComponentInParent<CartMovement>();
                if (cart && CarriedItem == null)
                    _targetPickup = cart;
                var jay = objects[i].GetComponentInParent<JayAI>();
                if(jay && !(_targetPickup is CartMovement))
                {
                    _targetPickup = jay;
                }

            }
            if (_targetPickup != null)
            {
                var comp = (_targetPickup as Component);
                if (comp == null)
                {
                    CurrentPickup = null;
                    return;
                }
                var pickTransform = comp.transform;
                var dist = transform.position - pickTransform.position;
                if (dist.magnitude < MaxPickupDistance)
                    _targetPickup.Pickup();
            }
        } 

        public void SetCurrentPickup(IPickupAble pick)
        {
            CurrentPickup = pick;
        }

        // Update is called once per frame
        void Update()
        {
            DirectionInput = _movementAction.ReadValue<Vector2>();

            PlayerAnim.SetFloat("Movement", Mathf.Clamp01(DirectionInput.magnitude));
            var forward = Vector3.ProjectOnPlane(ControlCamera.transform.forward, Vector3.up);
            var inputDir = new Vector3(DirectionInput.x, 0, DirectionInput.y);
            forward = Quaternion.LookRotation(forward) * inputDir;
            var curSpeed = CurrentPickup != null ? Speed: RunningSpeed;
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

        public void SetTrackedPickupable(IPickupAble pickupable)
        {
            _targetPickup = pickupable;
        }
        public void SetCartState(bool state)
        {
            if (state)
            {
                _controller.center = new Vector3(0, 1.2f, 1);
                _controller.radius = 0.8f;
            }
            else
            {
                _controller.center = new Vector3(0, 1.2f, 0);
                _controller.radius = 0.5f;
            }
        }
    }
}