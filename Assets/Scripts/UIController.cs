using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameState _gameState;
    [SerializeField] private Slider _playerHealth;
    [SerializeField] private Slider _planetHealth;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private TMP_Text _finalScoreText;

    private void OnEnable()
    {
        _gameState.OnIncreaseScore.AddListener(UpdateScoreUI);
        _gameState.OnGameOver.AddListener(ShowGameOverScreen);
        
        _gameState.OnIncreaseScore.AddListener(UpdateScoreUI);
        _gameState.OnGameOver.AddListener(ShowGameOverScreen);
    }
    private void OnDisable()
    {
        _gameState.OnIncreaseScore.RemoveListener(UpdateScoreUI);
        _gameState.OnGameOver.RemoveListener(ShowGameOverScreen);

        _gameState.OnIncreaseScore.RemoveListener(UpdateScoreUI);
        _gameState.OnGameOver.RemoveListener(ShowGameOverScreen);
    }
    
    public void UpdateScoreUI(int newScore)
    {
        _scoreText.text = $"Score {newScore}";
    }

    public void UpdatePlayerHealthUI(int newHealth)
    {
        Debug.Log($"Updating Player Health UI: {newHealth}, Before Update Slider Value: {_playerHealth.value}");
        _playerHealth.value = newHealth;
        Canvas.ForceUpdateCanvases(); // Force UI refresh
        Debug.Log($"After Update Slider Value: {_playerHealth.value}");
    }
    
    public void UpdatePlanetHealthUI(int newHealth)
    {
        _planetHealth.value = newHealth;
    }
    
    public void ShowGameOverScreen()
    {
        _gameOverScreen.SetActive(true);
        _finalScoreText.text = $"Score: {_gameState.Score}";
    }

    public void BindPlayerHealth(Health playerHealth)
    {
        playerHealth.OnReceiveHealth.AddListener(UpdatePlayerHealthUI);
        playerHealth.OnReceiveDamage.AddListener(UpdatePlayerHealthUI);
    }

    private void Start()
    {
        var playerHealth = FindObjectOfType<Health>();
        if (playerHealth != null)
        {
            BindPlayerHealth(playerHealth);
            _playerHealth.maxValue = playerHealth.MaxHealth;
            _playerHealth.value = playerHealth.CurrentHealth;
        }
    }
}
