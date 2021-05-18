// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Virginie/InputSystem/MobileControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MobileControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MobileControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MobileControls"",
    ""maps"": [
        {
            ""name"": ""Mobile"",
            ""id"": ""f9a40c43-4c11-4a3e-8132-9fb3a2d333a8"",
            ""actions"": [
                {
                    ""name"": ""TouchInput"",
                    ""type"": ""PassThrough"",
                    ""id"": ""19b6e0d3-ba32-4073-b318-fdebc0d273af"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchPress"",
                    ""type"": ""Button"",
                    ""id"": ""c07c8b29-4045-44c4-9959-7dfb7d018a45"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cfbc3786-52ee-432a-92d3-c6130ebd80e5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryContact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d956627f-5fc5-478c-9934-5aff930978f7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PrimaryPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3bacd822-e215-49c0-be7b-9c6e03dd9e13"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryContact"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8f742b91-5661-425d-8d16-3f75cc895d74"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SecondaryPosition"",
                    ""type"": ""Value"",
                    ""id"": ""a5ba1945-af13-4970-964c-d8ab2031074d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""03b5f2bc-c78f-4bc3-8867-b6d634ffd4a1"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5be2ca12-94d4-4c2f-b6a4-23205d2789a6"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4192484e-814e-4717-ad07-807d3500bec6"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fea09c8-e0bd-494a-bbcb-df240fb8da2d"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a30fc475-a77a-4329-bf32-a6a138896759"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72e2c2dc-b964-47ba-ac86-ed60b91f986b"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6467565a-8837-4346-a83e-957edfc0e89a"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Mobile
        m_Mobile = asset.FindActionMap("Mobile", throwIfNotFound: true);
        m_Mobile_TouchInput = m_Mobile.FindAction("TouchInput", throwIfNotFound: true);
        m_Mobile_TouchPress = m_Mobile.FindAction("TouchPress", throwIfNotFound: true);
        m_Mobile_TouchPosition = m_Mobile.FindAction("TouchPosition", throwIfNotFound: true);
        m_Mobile_PrimaryContact = m_Mobile.FindAction("PrimaryContact", throwIfNotFound: true);
        m_Mobile_PrimaryPosition = m_Mobile.FindAction("PrimaryPosition", throwIfNotFound: true);
        m_Mobile_SecondaryContact = m_Mobile.FindAction("SecondaryContact", throwIfNotFound: true);
        m_Mobile_SecondaryPosition = m_Mobile.FindAction("SecondaryPosition", throwIfNotFound: true);
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

    // Mobile
    private readonly InputActionMap m_Mobile;
    private IMobileActions m_MobileActionsCallbackInterface;
    private readonly InputAction m_Mobile_TouchInput;
    private readonly InputAction m_Mobile_TouchPress;
    private readonly InputAction m_Mobile_TouchPosition;
    private readonly InputAction m_Mobile_PrimaryContact;
    private readonly InputAction m_Mobile_PrimaryPosition;
    private readonly InputAction m_Mobile_SecondaryContact;
    private readonly InputAction m_Mobile_SecondaryPosition;
    public struct MobileActions
    {
        private @MobileControls m_Wrapper;
        public MobileActions(@MobileControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchInput => m_Wrapper.m_Mobile_TouchInput;
        public InputAction @TouchPress => m_Wrapper.m_Mobile_TouchPress;
        public InputAction @TouchPosition => m_Wrapper.m_Mobile_TouchPosition;
        public InputAction @PrimaryContact => m_Wrapper.m_Mobile_PrimaryContact;
        public InputAction @PrimaryPosition => m_Wrapper.m_Mobile_PrimaryPosition;
        public InputAction @SecondaryContact => m_Wrapper.m_Mobile_SecondaryContact;
        public InputAction @SecondaryPosition => m_Wrapper.m_Mobile_SecondaryPosition;
        public InputActionMap Get() { return m_Wrapper.m_Mobile; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MobileActions set) { return set.Get(); }
        public void SetCallbacks(IMobileActions instance)
        {
            if (m_Wrapper.m_MobileActionsCallbackInterface != null)
            {
                @TouchInput.started -= m_Wrapper.m_MobileActionsCallbackInterface.OnTouchInput;
                @TouchInput.performed -= m_Wrapper.m_MobileActionsCallbackInterface.OnTouchInput;
                @TouchInput.canceled -= m_Wrapper.m_MobileActionsCallbackInterface.OnTouchInput;
                @TouchPress.started -= m_Wrapper.m_MobileActionsCallbackInterface.OnTouchPress;
                @TouchPress.performed -= m_Wrapper.m_MobileActionsCallbackInterface.OnTouchPress;
                @TouchPress.canceled -= m_Wrapper.m_MobileActionsCallbackInterface.OnTouchPress;
                @TouchPosition.started -= m_Wrapper.m_MobileActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.performed -= m_Wrapper.m_MobileActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.canceled -= m_Wrapper.m_MobileActionsCallbackInterface.OnTouchPosition;
                @PrimaryContact.started -= m_Wrapper.m_MobileActionsCallbackInterface.OnPrimaryContact;
                @PrimaryContact.performed -= m_Wrapper.m_MobileActionsCallbackInterface.OnPrimaryContact;
                @PrimaryContact.canceled -= m_Wrapper.m_MobileActionsCallbackInterface.OnPrimaryContact;
                @PrimaryPosition.started -= m_Wrapper.m_MobileActionsCallbackInterface.OnPrimaryPosition;
                @PrimaryPosition.performed -= m_Wrapper.m_MobileActionsCallbackInterface.OnPrimaryPosition;
                @PrimaryPosition.canceled -= m_Wrapper.m_MobileActionsCallbackInterface.OnPrimaryPosition;
                @SecondaryContact.started -= m_Wrapper.m_MobileActionsCallbackInterface.OnSecondaryContact;
                @SecondaryContact.performed -= m_Wrapper.m_MobileActionsCallbackInterface.OnSecondaryContact;
                @SecondaryContact.canceled -= m_Wrapper.m_MobileActionsCallbackInterface.OnSecondaryContact;
                @SecondaryPosition.started -= m_Wrapper.m_MobileActionsCallbackInterface.OnSecondaryPosition;
                @SecondaryPosition.performed -= m_Wrapper.m_MobileActionsCallbackInterface.OnSecondaryPosition;
                @SecondaryPosition.canceled -= m_Wrapper.m_MobileActionsCallbackInterface.OnSecondaryPosition;
            }
            m_Wrapper.m_MobileActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchInput.started += instance.OnTouchInput;
                @TouchInput.performed += instance.OnTouchInput;
                @TouchInput.canceled += instance.OnTouchInput;
                @TouchPress.started += instance.OnTouchPress;
                @TouchPress.performed += instance.OnTouchPress;
                @TouchPress.canceled += instance.OnTouchPress;
                @TouchPosition.started += instance.OnTouchPosition;
                @TouchPosition.performed += instance.OnTouchPosition;
                @TouchPosition.canceled += instance.OnTouchPosition;
                @PrimaryContact.started += instance.OnPrimaryContact;
                @PrimaryContact.performed += instance.OnPrimaryContact;
                @PrimaryContact.canceled += instance.OnPrimaryContact;
                @PrimaryPosition.started += instance.OnPrimaryPosition;
                @PrimaryPosition.performed += instance.OnPrimaryPosition;
                @PrimaryPosition.canceled += instance.OnPrimaryPosition;
                @SecondaryContact.started += instance.OnSecondaryContact;
                @SecondaryContact.performed += instance.OnSecondaryContact;
                @SecondaryContact.canceled += instance.OnSecondaryContact;
                @SecondaryPosition.started += instance.OnSecondaryPosition;
                @SecondaryPosition.performed += instance.OnSecondaryPosition;
                @SecondaryPosition.canceled += instance.OnSecondaryPosition;
            }
        }
    }
    public MobileActions @Mobile => new MobileActions(this);
    public interface IMobileActions
    {
        void OnTouchInput(InputAction.CallbackContext context);
        void OnTouchPress(InputAction.CallbackContext context);
        void OnTouchPosition(InputAction.CallbackContext context);
        void OnPrimaryContact(InputAction.CallbackContext context);
        void OnPrimaryPosition(InputAction.CallbackContext context);
        void OnSecondaryContact(InputAction.CallbackContext context);
        void OnSecondaryPosition(InputAction.CallbackContext context);
    }
}
