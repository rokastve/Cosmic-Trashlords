// GENERATED AUTOMATICALLY FROM 'Assets/Resources/Input/CharacterActions.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class CharacterActions : InputActionAssetReference
{
    public CharacterActions()
    {
    }
    public CharacterActions(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // General
        m_General = asset.GetActionMap("General");
        m_General_Movement = m_General.GetAction("Movement");
        m_General_Use = m_General.GetAction("Use");
        m_General_Back = m_General.GetAction("Back");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_General = null;
        m_General_Movement = null;
        m_General_Use = null;
        m_General_Back = null;
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
    private InputAction m_General_Movement;
    private InputAction m_General_Use;
    private InputAction m_General_Back;
    public struct GeneralActions
    {
        private CharacterActions m_Wrapper;
        public GeneralActions(CharacterActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement { get { return m_Wrapper.m_General_Movement; } }
        public InputAction @Use { get { return m_Wrapper.m_General_Use; } }
        public InputAction @Back { get { return m_Wrapper.m_General_Back; } }
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
