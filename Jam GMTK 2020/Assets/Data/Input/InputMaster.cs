// GENERATED AUTOMATICALLY FROM 'Assets/Data/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""FPSMap"",
            ""id"": ""6570c8ca-4acb-495b-93ff-d2dc594a3ce6"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""7bc921fb-952e-4905-b83c-0a9377ba1989"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2b305dce-f2bb-4278-81e6-a13e84934a22"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // FPSMap
        m_FPSMap = asset.FindActionMap("FPSMap", throwIfNotFound: true);
        m_FPSMap_Interact = m_FPSMap.FindAction("Interact", throwIfNotFound: true);
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

    // FPSMap
    private readonly InputActionMap m_FPSMap;
    private IFPSMapActions m_FPSMapActionsCallbackInterface;
    private readonly InputAction m_FPSMap_Interact;
    public struct FPSMapActions
    {
        private @InputMaster m_Wrapper;
        public FPSMapActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_FPSMap_Interact;
        public InputActionMap Get() { return m_Wrapper.m_FPSMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FPSMapActions set) { return set.Get(); }
        public void SetCallbacks(IFPSMapActions instance)
        {
            if (m_Wrapper.m_FPSMapActionsCallbackInterface != null)
            {
                @Interact.started -= m_Wrapper.m_FPSMapActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_FPSMapActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_FPSMapActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_FPSMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public FPSMapActions @FPSMap => new FPSMapActions(this);
    public interface IFPSMapActions
    {
        void OnInteract(InputAction.CallbackContext context);
    }
}
