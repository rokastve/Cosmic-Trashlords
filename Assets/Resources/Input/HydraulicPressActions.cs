// GENERATED AUTOMATICALLY FROM 'Assets/Resources/Input/HydraulicPressActions.inputactions'

using System;
using UnityEngine;
using UnityEngine.Experimental.Input;


[Serializable]
public class HydraulicPressActions : InputActionAssetReference
{
    public HydraulicPressActions()
    {
    }
    public HydraulicPressActions(InputActionAsset asset)
        : base(asset)
    {
    }
    private bool m_Initialized;
    private void Initialize()
    {
        // Operable
        m_Operable = asset.GetActionMap("Operable");
        m_Operable_Exit = m_Operable.GetAction("Exit");
        m_Operable_PressProgress = m_Operable.GetAction("PressProgress");
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        m_Operable = null;
        m_Operable_Exit = null;
        m_Operable_PressProgress = null;
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
    // Operable
    private InputActionMap m_Operable;
    private InputAction m_Operable_Exit;
    private InputAction m_Operable_PressProgress;
    public struct OperableActions
    {
        private HydraulicPressActions m_Wrapper;
        public OperableActions(HydraulicPressActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Exit { get { return m_Wrapper.m_Operable_Exit; } }
        public InputAction @PressProgress { get { return m_Wrapper.m_Operable_PressProgress; } }
        public InputActionMap Get() { return m_Wrapper.m_Operable; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(OperableActions set) { return set.Get(); }
    }
    public OperableActions @Operable
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new OperableActions(this);
        }
    }
}
