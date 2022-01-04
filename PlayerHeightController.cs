/// <summary>
/// VaM Utilities
/// By Acidbubbles
/// Allows triggers that affect player height
/// Source: https://github.com/acidbubbles/vam-utilities
/// </summary>
public class PlayerHeightController : MVRScript
{
    private JSONStorableFloat _playerHeightJSON;
    private JSONStorableFloat _playerBaseHeightJSON;
    private JSONStorableFloat _playerRelativeHeightJSON;
    private JSONStorableAction _setPlayerBaseHeight;

    public override void Init()
    {
        SuperController.singleton.playerHeightAdjustSlider.onValueChanged.AddListener(OnPlayerHeightChanged);

        _playerHeightJSON = new JSONStorableFloat("Player Height", 0f, -1f, 5f, false)
        {
            setCallbackFunction = val =>
            {
                SuperController.singleton.playerHeightAdjust = val;
            },
            valNoCallback = SuperController.singleton.playerHeightAdjust,
            defaultVal = SuperController.singleton.playerHeightAdjust,
        };
        RegisterFloat(_playerHeightJSON);
        CreateSlider(_playerHeightJSON);

        _playerBaseHeightJSON = new JSONStorableFloat("[Relative] Player Base Height", 0f, -1f, 5f, false)
        {
            setCallbackFunction = val =>
            {
                _playerHeightJSON.val = val + _playerRelativeHeightJSON.val;
            },
            isStorable = false,
            valNoCallback = SuperController.singleton.playerHeightAdjust,
            defaultVal = SuperController.singleton.playerHeightAdjust
        };
        RegisterFloat(_playerBaseHeightJSON);
        CreateSlider(_playerBaseHeightJSON);

        _playerRelativeHeightJSON = new JSONStorableFloat("[Relative] Player Relative Height", 0f, -1f, 5f, false)
        {
            setCallbackFunction = val =>
            {
                _playerHeightJSON.val = _playerBaseHeightJSON.val + val;
            },
            isStorable = false
        };
        RegisterFloat(_playerRelativeHeightJSON);
        CreateSlider(_playerRelativeHeightJSON);

        _setPlayerBaseHeight = new JSONStorableAction("[Relative] Set Player Base Height", () =>
        {
            _playerBaseHeightJSON.valNoCallback = SuperController.singleton.playerHeightAdjust;
            _playerBaseHeightJSON.defaultVal = SuperController.singleton.playerHeightAdjust;
            _playerRelativeHeightJSON.valNoCallback = 0;
        });
        RegisterAction(_setPlayerBaseHeight);
        CreateButton(_setPlayerBaseHeight.name).button.onClick.AddListener(() => _setPlayerBaseHeight.actionCallback.Invoke());
    }

    private void OnPlayerHeightChanged(float val)
    {
        _playerHeightJSON.valNoCallback = val;
        _playerRelativeHeightJSON.valNoCallback = val - _playerBaseHeightJSON.val;
    }

    private void OnDisable()
    {
        SuperController.singleton.playerHeightAdjustSlider.onValueChanged.RemoveListener(OnPlayerHeightChanged);
    }
}