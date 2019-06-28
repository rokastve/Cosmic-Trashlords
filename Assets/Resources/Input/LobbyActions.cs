// GENERATED AUTOMATICALLY FROM 'Assets/Resources/Input/LobbyActions.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class LobbyActions : InputActionAssetReference
{
    public LobbyActions()
    {
    }
    public LobbyActions(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // General
        m_General = asset.GetActionMap("General");
        m_General_Start = m_General.GetAction("Start");
        m_General_Back = m_General.GetAction("Back");
        m_General_Next = m_General.GetAction("Next");
        m_General_Previous = m_General.GetAction("Previous");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_General = null;
        m_General_Start = null;
        m_General_Back = null;
        m_General_Next = null;
        m_General_Previous = null;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // General
    private InputActionMap m_General;
    private InputAction m_General_Start;
    private InputAction m_General_Back;
    private InputAction m_General_Next;
    private InputAction m_General_Previous;
    public struct GeneralActions
    {
        private LobbyActions m_Wrapper;
        public GeneralActions(LobbyActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Start { get { return m_Wrapper.m_General_Start; } }
        public InputAction @Back { get { return m_Wrapper.m_General_Back; } }
        public InputAction @Next { get { return m_Wrapper.m_General_Next; } }
        public InputAction @Previous { get { return m_Wrapper.m_General_Previous; } }
        public InputActionMap Get() { return m_Wrapper.m_General; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(GeneralActions set) { return set.Get(); }
    }
    public GeneralActions @General
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new GeneralActions(this);
        }
    }
}
