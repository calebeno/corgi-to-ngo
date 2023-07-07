using MoreMountains.CorgiEngine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ServerTestUI : MonoBehaviour
{
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _clientButton;
    [SerializeField] private LevelManager _levelManager;

    private void Start()
    {
        _hostButton.onClick.AddListener(HandleHostClick);
        _clientButton.onClick.AddListener(HandleClientClick);

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnDisable()
    {
        _hostButton.onClick.RemoveListener(HandleHostClick);
        _clientButton.onClick.RemoveListener(HandleClientClick);

        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
    }

    private void HandleHostClick()
    {
        Debug.Log("Starting Host");
        NetworkManager.Singleton.StartHost();
        _levelManager.gameObject.SetActive(true);
        Hide();
    }

    private void HandleClientClick()
    {
        Debug.Log("Starting Client");
        NetworkManager.Singleton.StartClient();
    }

    private void OnClientConnected(ulong clientId)
    {
        _levelManager.gameObject.SetActive(true);
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
