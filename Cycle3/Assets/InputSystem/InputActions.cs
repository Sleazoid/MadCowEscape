// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Wacom"",
            ""id"": ""1c7f0b50-9251-4864-b8d4-b6072c55da98"",
            ""actions"": [
                {
                    ""name"": ""Press"",
                    ""type"": ""Button"",
                    ""id"": ""ab4f7ddf-50a6-4108-9eec-6dee451ba468"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Range"",
                    ""type"": ""Button"",
                    ""id"": ""755e66d7-a60a-418f-908c-2d4437d79e50"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""oo"",
                    ""type"": ""Button"",
                    ""id"": ""4bd9390c-adc3-4bc1-94c6-5b2c2b1d6e57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""btn1"",
                    ""type"": ""Button"",
                    ""id"": ""7b8d66ac-73b7-47f4-baeb-f3c33cab7989"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""btn2"",
                    ""type"": ""Button"",
                    ""id"": ""55ea61a7-277c-4802-a6e0-93edf05b7742"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""btn3"",
                    ""type"": ""Button"",
                    ""id"": ""9463439c-9b44-4677-a93d-298c30e87b44"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""btn4"",
                    ""type"": ""Button"",
                    ""id"": ""f7a0b9bc-c47f-45eb-a611-3cae287d169d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""eraser"",
                    ""type"": ""Button"",
                    ""id"": ""c9194b1f-7d42-478b-92a3-462d4abbaace"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""05ca4380-fd93-498f-b84d-0b97e95e8f56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Retry"",
                    ""type"": ""Button"",
                    ""id"": ""b6eac690-e562-429c-9b21-0b0b833feaab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NextLevel"",
                    ""type"": ""Button"",
                    ""id"": ""94fe0218-4e8f-4b8d-a311-6552197eeb5c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""24317d3a-1396-4143-9f49-8a893dac93f3"",
                    ""path"": ""<Pen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67c312f5-1dc9-49c0-9da0-b5f124897a5c"",
                    ""path"": ""<Pen>/inRange"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Range"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c196637-7976-4a53-8109-7b63e9ff82ce"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""oo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1026b75-642e-4b7e-b565-6102006d52d3"",
                    ""path"": ""<Pen>/barrel1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""btn1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fde67d1f-813e-43fe-89f4-568905337efd"",
                    ""path"": ""<Pen>/barrel2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""btn2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83f71b1c-5e85-45da-a79c-e17528c3fb61"",
                    ""path"": ""<Pen>/barrel3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""btn3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d792ef8-415e-493b-8a22-4e9f95754511"",
                    ""path"": ""<Pen>/barrel4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""btn4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ff864f2-efe6-4feb-b375-c62e5c4cd9aa"",
                    ""path"": ""<Pen>/eraser"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""eraser"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d123f5f1-ce4a-474e-a297-bb5f87caccb2"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9edc0686-1b29-478d-a7a2-0410131cc911"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Retry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c40bc34a-7d01-492c-a409-51b6f97aa7c3"",
                    ""path"": ""<Keyboard>/#(P)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextLevel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""id"": ""d8a92bce-9542-45dd-ba6c-4376cda55185"",
            ""actions"": [
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""a4867b5f-2616-4a44-b03b-7ec7bbc790f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftStick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""14228b24-b347-4e5d-8167-72c2284767b4"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""db08d1c0-ecda-4fe4-9af6-b5bd71364576"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d86494b-3b82-4a7d-a1c8-c42b1410ab92"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Wacom
        m_Wacom = asset.FindActionMap("Wacom", throwIfNotFound: true);
        m_Wacom_Press = m_Wacom.FindAction("Press", throwIfNotFound: true);
        m_Wacom_Range = m_Wacom.FindAction("Range", throwIfNotFound: true);
        m_Wacom_oo = m_Wacom.FindAction("oo", throwIfNotFound: true);
        m_Wacom_btn1 = m_Wacom.FindAction("btn1", throwIfNotFound: true);
        m_Wacom_btn2 = m_Wacom.FindAction("btn2", throwIfNotFound: true);
        m_Wacom_btn3 = m_Wacom.FindAction("btn3", throwIfNotFound: true);
        m_Wacom_btn4 = m_Wacom.FindAction("btn4", throwIfNotFound: true);
        m_Wacom_eraser = m_Wacom.FindAction("eraser", throwIfNotFound: true);
        m_Wacom_Quit = m_Wacom.FindAction("Quit", throwIfNotFound: true);
        m_Wacom_Retry = m_Wacom.FindAction("Retry", throwIfNotFound: true);
        m_Wacom_NextLevel = m_Wacom.FindAction("NextLevel", throwIfNotFound: true);
        // Gamepad
        m_Gamepad = asset.FindActionMap("Gamepad", throwIfNotFound: true);
        m_Gamepad_Dash = m_Gamepad.FindAction("Dash", throwIfNotFound: true);
        m_Gamepad_LeftStick = m_Gamepad.FindAction("LeftStick", throwIfNotFound: true);
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

    // Wacom
    private readonly InputActionMap m_Wacom;
    private IWacomActions m_WacomActionsCallbackInterface;
    private readonly InputAction m_Wacom_Press;
    private readonly InputAction m_Wacom_Range;
    private readonly InputAction m_Wacom_oo;
    private readonly InputAction m_Wacom_btn1;
    private readonly InputAction m_Wacom_btn2;
    private readonly InputAction m_Wacom_btn3;
    private readonly InputAction m_Wacom_btn4;
    private readonly InputAction m_Wacom_eraser;
    private readonly InputAction m_Wacom_Quit;
    private readonly InputAction m_Wacom_Retry;
    private readonly InputAction m_Wacom_NextLevel;
    public struct WacomActions
    {
        private @InputActions m_Wrapper;
        public WacomActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Press => m_Wrapper.m_Wacom_Press;
        public InputAction @Range => m_Wrapper.m_Wacom_Range;
        public InputAction @oo => m_Wrapper.m_Wacom_oo;
        public InputAction @btn1 => m_Wrapper.m_Wacom_btn1;
        public InputAction @btn2 => m_Wrapper.m_Wacom_btn2;
        public InputAction @btn3 => m_Wrapper.m_Wacom_btn3;
        public InputAction @btn4 => m_Wrapper.m_Wacom_btn4;
        public InputAction @eraser => m_Wrapper.m_Wacom_eraser;
        public InputAction @Quit => m_Wrapper.m_Wacom_Quit;
        public InputAction @Retry => m_Wrapper.m_Wacom_Retry;
        public InputAction @NextLevel => m_Wrapper.m_Wacom_NextLevel;
        public InputActionMap Get() { return m_Wrapper.m_Wacom; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WacomActions set) { return set.Get(); }
        public void SetCallbacks(IWacomActions instance)
        {
            if (m_Wrapper.m_WacomActionsCallbackInterface != null)
            {
                @Press.started -= m_Wrapper.m_WacomActionsCallbackInterface.OnPress;
                @Press.performed -= m_Wrapper.m_WacomActionsCallbackInterface.OnPress;
                @Press.canceled -= m_Wrapper.m_WacomActionsCallbackInterface.OnPress;
                @Range.started -= m_Wrapper.m_WacomActionsCallbackInterface.OnRange;
                @Range.performed -= m_Wrapper.m_WacomActionsCallbackInterface.OnRange;
                @Range.canceled -= m_Wrapper.m_WacomActionsCallbackInterface.OnRange;
                @oo.started -= m_Wrapper.m_WacomActionsCallbackInterface.OnOo;
                @oo.performed -= m_Wrapper.m_WacomActionsCallbackInterface.OnOo;
                @oo.canceled -= m_Wrapper.m_WacomActionsCallbackInterface.OnOo;
                @btn1.started -= m_Wrapper.m_WacomActionsCallbackInterface.OnBtn1;
                @btn1.performed -= m_Wrapper.m_WacomActionsCallbackInterface.OnBtn1;
                @btn1.canceled -= m_Wrapper.m_WacomActionsCallbackInterface.OnBtn1;
                @btn2.started -= m_Wrapper.m_WacomActionsCallbackInterface.OnBtn2;
                @btn2.performed -= m_Wrapper.m_WacomActionsCallbackInterface.OnBtn2;
                @btn2.canceled -= m_Wrapper.m_WacomActionsCallbackInterface.OnBtn2;
                @btn3.started -= m_Wrapper.m_WacomActionsCallbackInterface.OnBtn3;
                @btn3.performed -= m_Wrapper.m_WacomActionsCallbackInterface.OnBtn3;
                @btn3.canceled -= m_Wrapper.m_WacomActionsCallbackInterface.OnBtn3;
                @btn4.started -= m_Wrapper.m_WacomActionsCallbackInterface.OnBtn4;
                @btn4.performed -= m_Wrapper.m_WacomActionsCallbackInterface.OnBtn4;
                @btn4.canceled -= m_Wrapper.m_WacomActionsCallbackInterface.OnBtn4;
                @eraser.started -= m_Wrapper.m_WacomActionsCallbackInterface.OnEraser;
                @eraser.performed -= m_Wrapper.m_WacomActionsCallbackInterface.OnEraser;
                @eraser.canceled -= m_Wrapper.m_WacomActionsCallbackInterface.OnEraser;
                @Quit.started -= m_Wrapper.m_WacomActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_WacomActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_WacomActionsCallbackInterface.OnQuit;
                @Retry.started -= m_Wrapper.m_WacomActionsCallbackInterface.OnRetry;
                @Retry.performed -= m_Wrapper.m_WacomActionsCallbackInterface.OnRetry;
                @Retry.canceled -= m_Wrapper.m_WacomActionsCallbackInterface.OnRetry;
                @NextLevel.started -= m_Wrapper.m_WacomActionsCallbackInterface.OnNextLevel;
                @NextLevel.performed -= m_Wrapper.m_WacomActionsCallbackInterface.OnNextLevel;
                @NextLevel.canceled -= m_Wrapper.m_WacomActionsCallbackInterface.OnNextLevel;
            }
            m_Wrapper.m_WacomActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Press.started += instance.OnPress;
                @Press.performed += instance.OnPress;
                @Press.canceled += instance.OnPress;
                @Range.started += instance.OnRange;
                @Range.performed += instance.OnRange;
                @Range.canceled += instance.OnRange;
                @oo.started += instance.OnOo;
                @oo.performed += instance.OnOo;
                @oo.canceled += instance.OnOo;
                @btn1.started += instance.OnBtn1;
                @btn1.performed += instance.OnBtn1;
                @btn1.canceled += instance.OnBtn1;
                @btn2.started += instance.OnBtn2;
                @btn2.performed += instance.OnBtn2;
                @btn2.canceled += instance.OnBtn2;
                @btn3.started += instance.OnBtn3;
                @btn3.performed += instance.OnBtn3;
                @btn3.canceled += instance.OnBtn3;
                @btn4.started += instance.OnBtn4;
                @btn4.performed += instance.OnBtn4;
                @btn4.canceled += instance.OnBtn4;
                @eraser.started += instance.OnEraser;
                @eraser.performed += instance.OnEraser;
                @eraser.canceled += instance.OnEraser;
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
                @Retry.started += instance.OnRetry;
                @Retry.performed += instance.OnRetry;
                @Retry.canceled += instance.OnRetry;
                @NextLevel.started += instance.OnNextLevel;
                @NextLevel.performed += instance.OnNextLevel;
                @NextLevel.canceled += instance.OnNextLevel;
            }
        }
    }
    public WacomActions @Wacom => new WacomActions(this);

    // Gamepad
    private readonly InputActionMap m_Gamepad;
    private IGamepadActions m_GamepadActionsCallbackInterface;
    private readonly InputAction m_Gamepad_Dash;
    private readonly InputAction m_Gamepad_LeftStick;
    public struct GamepadActions
    {
        private @InputActions m_Wrapper;
        public GamepadActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Dash => m_Wrapper.m_Gamepad_Dash;
        public InputAction @LeftStick => m_Wrapper.m_Gamepad_LeftStick;
        public InputActionMap Get() { return m_Wrapper.m_Gamepad; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamepadActions set) { return set.Get(); }
        public void SetCallbacks(IGamepadActions instance)
        {
            if (m_Wrapper.m_GamepadActionsCallbackInterface != null)
            {
                @Dash.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnDash;
                @LeftStick.started -= m_Wrapper.m_GamepadActionsCallbackInterface.OnLeftStick;
                @LeftStick.performed -= m_Wrapper.m_GamepadActionsCallbackInterface.OnLeftStick;
                @LeftStick.canceled -= m_Wrapper.m_GamepadActionsCallbackInterface.OnLeftStick;
            }
            m_Wrapper.m_GamepadActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @LeftStick.started += instance.OnLeftStick;
                @LeftStick.performed += instance.OnLeftStick;
                @LeftStick.canceled += instance.OnLeftStick;
            }
        }
    }
    public GamepadActions @Gamepad => new GamepadActions(this);
    public interface IWacomActions
    {
        void OnPress(InputAction.CallbackContext context);
        void OnRange(InputAction.CallbackContext context);
        void OnOo(InputAction.CallbackContext context);
        void OnBtn1(InputAction.CallbackContext context);
        void OnBtn2(InputAction.CallbackContext context);
        void OnBtn3(InputAction.CallbackContext context);
        void OnBtn4(InputAction.CallbackContext context);
        void OnEraser(InputAction.CallbackContext context);
        void OnQuit(InputAction.CallbackContext context);
        void OnRetry(InputAction.CallbackContext context);
        void OnNextLevel(InputAction.CallbackContext context);
    }
    public interface IGamepadActions
    {
        void OnDash(InputAction.CallbackContext context);
        void OnLeftStick(InputAction.CallbackContext context);
    }
}
