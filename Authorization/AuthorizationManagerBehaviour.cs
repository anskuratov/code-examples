using Assets.Sources.Settings;
using Assets.Sources.Utils;
using SorResources.Models;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AuthorizationManagerBehaviour : MonoBehaviour
{
    private LoaderBehaviour _loader;
    private WebManagerBehaviour _webManager;
    private ApiRoutes _apiRoutes;
    private GameDataPoolBehaviour _gameDataPool;
    private GameSettings _gameSettings;

    [SerializeField]
    private Text _loadingText;

    [Inject]
    private void ZenjectInit(LoaderBehaviour loader,
        WebManagerBehaviour webManager,
        ApiRoutes apiRoutes,
        GameDataPoolBehaviour gameDataPool,
        GameSettings gameSettings)
    {
        _loader = loader;
        _webManager = webManager;
        _apiRoutes = apiRoutes;
        _gameDataPool = gameDataPool;
        _gameSettings = gameSettings;
    }

    private void Start()
    {
        CheckVersion();
    }

    private void CheckVersion()
    {
        _webManager.Send(_apiRoutes.Main.Version(_gameSettings.Version), 
            CheckVersionHandler);
    }

    private void CheckVersionHandler(ResponseCode responseCode)
    {
        switch(responseCode)
        {
            case ResponseCode.Success:
                Debug.Log("Current version");
                Authenticate();
                break;

            case ResponseCode.OldVersion:
                Debug.LogError("OldVersion");
                _loadingText.text = "This version of the game is not supported.\n Please update the game.";
                break;

            default:
                throw new System.Exception($"Response code version control: {(int)responseCode}");
        }
            
    }

    private void Authenticate()
    {
        var token = PlayerPrefs.GetString(SORPlayerPreferences.Token);

        if (token == null || token.Equals(string.Empty))
            LoadLoginScene();
        else
            _webManager.Send(_apiRoutes.Auth.IsValid(), AuthRequestHandler);
    }

    private void LoadLoginScene() =>
        _loader.LoadScene(SORScene.level_main_menu, true);

    private void LoadGameScene() =>
        _loader.LoadScene(SORScene.level_oilfield, true);

    private void AuthRequestHandler(ResponseCode responseCode)
    {
        switch (responseCode)
        {
            case ResponseCode.Success:
                _webManager.SendWithValue<StartData>(_apiRoutes.Main.OnStart(), OnStartRequestHandler);
                break;

            case ResponseCode.NonAuthorizedError:
                LoadLoginScene();
                break;

            default:
                Debug.Log("Unknown error");
                break;
        }
    }

    private void OnStartRequestHandler(StartData startData, ResponseCode responseCode)
    {
        switch (responseCode)
        {
            case ResponseCode.Success:
				_gameDataPool.InitializeData(startData);
                LoadGameScene();
                break;

            default:
                Debug.Log("Unknown error");
                break;
        }
    }
}
