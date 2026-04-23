using System;
using UnityEngine;
using UnityEngine.UIElements;

public class keyBindUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _root;
    private VisualElement _keys;
    private VisualElement[] _playrs = new VisualElement[2];
    private Button[] _leftButtons = new Button[7];
    private Button[] _rightButtons = new Button[7];
    private KeyRebinder _keyRebinder;
    private Label _keyBinLabel;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        _keyRebinder = GetComponent<KeyRebinder>();
        _root = _uiDocument.rootVisualElement;
        _keys = _root.Q<VisualElement>("Keys");
        _playrs[0] = _keys.Q<VisualElement>("LeftPlayer");
        _playrs[1] = _keys.Q<VisualElement>("RightPlayer");
        _keyBinLabel = _root.Q<Label>("KeyBinLabel");

        ButtonsSetting(ref _leftButtons, 0);
        ButtonsSetting(ref _rightButtons, 1);

        TextSet(ref _leftButtons, 0);
        TextSet(ref _rightButtons, 1);
        //초기화()
        KeyRebinder(ref _leftButtons, 0);
        KeyRebinder(ref _rightButtons, 1);
        IsBing(false);
    }
    public void IsBing(bool bing)
    {
        _keyBinLabel.style.display = bing ? DisplayStyle.Flex : DisplayStyle.None;
    }

    private void ButtonsSetting(ref Button[] buttons, int valur)
    {
        buttons[0] = _playrs[valur].Q<Button>("Up");
        buttons[1] = _playrs[valur].Q<Button>("Down");
        buttons[2] = _playrs[valur].Q<Button>("Left");
        buttons[3] = _playrs[valur].Q<Button>("Right");
        buttons[4] = _playrs[valur].Q<Button>("Shoot");
        buttons[5] = _playrs[valur].Q<Button>("Barrier");
        buttons[6] = _playrs[valur].Q<Button>("Skill");
    }

    private void TextSet(ref Button[] buttons, int valur)
    {
        {
            buttons[0].text = TextControl(_keyRebinder.LoadKeyName((KeyMapType)valur, KeyActionType.MovementUp));
            buttons[1].text = TextControl(_keyRebinder.LoadKeyName((KeyMapType)valur, KeyActionType.MovementDown));
            buttons[2].text = TextControl(_keyRebinder.LoadKeyName((KeyMapType)valur, KeyActionType.MovementLeft));
            buttons[3].text = TextControl(_keyRebinder.LoadKeyName((KeyMapType)valur, KeyActionType.MovementRight));
            buttons[4].text = TextControl(_keyRebinder.LoadKeyName((KeyMapType)valur, KeyActionType.Shoot));
            buttons[5].text = TextControl(_keyRebinder.LoadKeyName((KeyMapType)valur, KeyActionType.Barrier));
            buttons[6].text = TextControl(_keyRebinder.LoadKeyName((KeyMapType)valur, KeyActionType.Skill));
        }
    }

    private string TextControl(string text)
    {
        if (text == "Up Arrow") return "↑";
        else if (text == "Down Arrow") return "↓";
        else if (text == "Right Arrow") return "→";
        else if (text == "Left Arrow") return "←";

        return text;
    }

    private void KeyRebinder(ref Button[] buttons, int valur)
    {
        var buttons1 = buttons;
        buttons[0].RegisterCallback<ClickEvent>(evt =>
        {
            _keyRebinder.StartRebinding((KeyMapType)valur, KeyActionType.MovementUp, buttons1[0]);

        });
        buttons[1].RegisterCallback<ClickEvent>(evt =>
        {
            _keyRebinder.StartRebinding((KeyMapType)valur, KeyActionType.MovementDown, buttons1[1]);

        });
        buttons[2].RegisterCallback<ClickEvent>(evt =>
        {
            _keyRebinder.StartRebinding((KeyMapType)valur, KeyActionType.MovementLeft, buttons1[2]);

        });
        buttons[3].RegisterCallback<ClickEvent>(evt =>
        {
            _keyRebinder.StartRebinding((KeyMapType)valur, KeyActionType.MovementRight, buttons1[3]);

        });
        buttons[4].RegisterCallback<ClickEvent>(evt =>
        {
            _keyRebinder.StartRebinding((KeyMapType)valur, KeyActionType.Shoot, buttons1[4]);

        });
        buttons[5].RegisterCallback<ClickEvent>(evt =>
        {
            _keyRebinder.StartRebinding((KeyMapType)valur, KeyActionType.Barrier, buttons1[5]);

        });
        buttons[6].RegisterCallback<ClickEvent>(evt =>
        {
            _keyRebinder.StartRebinding((KeyMapType)valur, KeyActionType.Skill, buttons1[6]);

        });
    }
}