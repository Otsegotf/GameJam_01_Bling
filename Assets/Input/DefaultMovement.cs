// GENERATED AUTOMATICALLY FROM 'Assets/Input/DefaultMovement.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace GJgame
{
    public class @DefaultMovement : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @DefaultMovement()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultMovement"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""9e6df8d9-3658-453d-8493-9686099ccd04"",
            ""actions"": [
                {
                    ""name"": ""MoveDirection"",
                    ""type"": ""Value"",
                    ""id"": ""563f3d16-1c50-4b79-b330-a10c0c355ccb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookAround"",
                    ""type"": ""Value"",
                    ""id"": ""dfb44724-efcc-4727-b610-de2f1c429b57"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""4f48a1e0-1383-4777-aba7-3248a7c9c90f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""be1c162e-b88c-453d-ae8f-8a83ca5c3560"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDirection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c3c437ca-769c-4974-8a89-2e7bc6bf2c58"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""MoveDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""50fd384e-8d47-47e2-9136-e91563a51d9b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""MoveDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""19e726d5-abc1-4f70-85a1-6703f2f9611c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""MoveDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0ead01e4-720e-45e7-aac9-8a29f7e139c8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""MoveDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b43634a3-27b0-45bb-b104-592fee14d8f9"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""MoveDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cdec7b99-40fd-433b-9aa9-dd1dd08d87b4"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""LookAround"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""15efe58a-6bef-4738-981f-bb76c040d373"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""LookAround"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c41f8922-40a0-40c1-85f4-2d5dbca324ab"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09b487d5-c2b3-4d35-8ab0-fcb95cd5a3f6"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interactions"",
            ""id"": ""a96ffa96-e74c-48cb-b682-b8b580c843cf"",
            ""actions"": [
                {
                    ""name"": ""Activate"",
                    ""type"": ""Button"",
                    ""id"": ""531b07e2-e35b-4386-beb8-bdde6b999889"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Detach"",
                    ""type"": ""Button"",
                    ""id"": ""0fc69a46-c900-4042-ad9c-e51902417657"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d583810a-7011-4f0a-9b48-0c5bde3d6da1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Activate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff798777-ba43-4278-89e1-bfafda52c739"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Activate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""059160c7-7036-4be1-a2d4-c58b611f4db5"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Detach"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3cc7aa8-1599-4151-9f47-9eeb085ba668"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Detach"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Default"",
            ""bindingGroup"": ""Default"",
            ""devices"": []
        }
    ]
}");
            // Movement
            m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
            m_Movement_MoveDirection = m_Movement.FindAction("MoveDirection", throwIfNotFound: true);
            m_Movement_LookAround = m_Movement.FindAction("LookAround", throwIfNotFound: true);
            m_Movement_Run = m_Movement.FindAction("Run", throwIfNotFound: true);
            // Interactions
            m_Interactions = asset.FindActionMap("Interactions", throwIfNotFound: true);
            m_Interactions_Activate = m_Interactions.FindAction("Activate", throwIfNotFound: true);
            m_Interactions_Detach = m_Interactions.FindAction("Detach", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Movement
        private readonly InputActionMap m_Movement;
        private IMovementActions m_MovementActionsCallbackInterface;
        private readonly InputAction m_Movement_MoveDirection;
        private readonly InputAction m_Movement_LookAround;
        private readonly InputAction m_Movement_Run;
        public struct MovementActions
        {
            private @DefaultMovement m_Wrapper;
            public MovementActions(@DefaultMovement wrapper) { m_Wrapper = wrapper; }
            public InputAction @MoveDirection => m_Wrapper.m_Movement_MoveDirection;
            public InputAction @LookAround => m_Wrapper.m_Movement_LookAround;
            public InputAction @Run => m_Wrapper.m_Movement_Run;
            public InputActionMap Get() { return m_Wrapper.m_Movement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
            public void SetCallbacks(IMovementActions instance)
            {
                if (m_Wrapper.m_MovementActionsCallbackInterface != null)
                {
                    @MoveDirection.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveDirection;
                    @MoveDirection.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveDirection;
                    @MoveDirection.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveDirection;
                    @LookAround.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnLookAround;
                    @LookAround.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnLookAround;
                    @LookAround.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnLookAround;
                    @Run.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRun;
                    @Run.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRun;
                    @Run.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRun;
                }
                m_Wrapper.m_MovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MoveDirection.started += instance.OnMoveDirection;
                    @MoveDirection.performed += instance.OnMoveDirection;
                    @MoveDirection.canceled += instance.OnMoveDirection;
                    @LookAround.started += instance.OnLookAround;
                    @LookAround.performed += instance.OnLookAround;
                    @LookAround.canceled += instance.OnLookAround;
                    @Run.started += instance.OnRun;
                    @Run.performed += instance.OnRun;
                    @Run.canceled += instance.OnRun;
                }
            }
        }
        public MovementActions @Movement => new MovementActions(this);

        // Interactions
        private readonly InputActionMap m_Interactions;
        private IInteractionsActions m_InteractionsActionsCallbackInterface;
        private readonly InputAction m_Interactions_Activate;
        private readonly InputAction m_Interactions_Detach;
        public struct InteractionsActions
        {
            private @DefaultMovement m_Wrapper;
            public InteractionsActions(@DefaultMovement wrapper) { m_Wrapper = wrapper; }
            public InputAction @Activate => m_Wrapper.m_Interactions_Activate;
            public InputAction @Detach => m_Wrapper.m_Interactions_Detach;
            public InputActionMap Get() { return m_Wrapper.m_Interactions; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(InteractionsActions set) { return set.Get(); }
            public void SetCallbacks(IInteractionsActions instance)
            {
                if (m_Wrapper.m_InteractionsActionsCallbackInterface != null)
                {
                    @Activate.started -= m_Wrapper.m_InteractionsActionsCallbackInterface.OnActivate;
                    @Activate.performed -= m_Wrapper.m_InteractionsActionsCallbackInterface.OnActivate;
                    @Activate.canceled -= m_Wrapper.m_InteractionsActionsCallbackInterface.OnActivate;
                    @Detach.started -= m_Wrapper.m_InteractionsActionsCallbackInterface.OnDetach;
                    @Detach.performed -= m_Wrapper.m_InteractionsActionsCallbackInterface.OnDetach;
                    @Detach.canceled -= m_Wrapper.m_InteractionsActionsCallbackInterface.OnDetach;
                }
                m_Wrapper.m_InteractionsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Activate.started += instance.OnActivate;
                    @Activate.performed += instance.OnActivate;
                    @Activate.canceled += instance.OnActivate;
                    @Detach.started += instance.OnDetach;
                    @Detach.performed += instance.OnDetach;
                    @Detach.canceled += instance.OnDetach;
                }
            }
        }
        public InteractionsActions @Interactions => new InteractionsActions(this);
        private int m_DefaultSchemeIndex = -1;
        public InputControlScheme DefaultScheme
        {
            get
            {
                if (m_DefaultSchemeIndex == -1) m_DefaultSchemeIndex = asset.FindControlSchemeIndex("Default");
                return asset.controlSchemes[m_DefaultSchemeIndex];
            }
        }
        public interface IMovementActions
        {
            void OnMoveDirection(InputAction.CallbackContext context);
            void OnLookAround(InputAction.CallbackContext context);
            void OnRun(InputAction.CallbackContext context);
        }
        public interface IInteractionsActions
        {
            void OnActivate(InputAction.CallbackContext context);
            void OnDetach(InputAction.CallbackContext context);
        }
    }
}
