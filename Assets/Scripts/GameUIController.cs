using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_successUI;

    [SerializeField]
    private GameObject m_failureUI;

    private void Awake()
    {
        MessageDispatcher.Instance.AddListener(GameEvents.GAME_SUCCESS, OnGameSuccess);
        MessageDispatcher.Instance.AddListener(GameEvents.GAME_FAILURE, OnGameFailure);
    }

    private void OnDestroy()
    {
        MessageDispatcher.Instance.RemoveListener(GameEvents.GAME_SUCCESS, OnGameSuccess);
        MessageDispatcher.Instance.RemoveListener(GameEvents.GAME_FAILURE, OnGameFailure);
    }

    private void OnGameSuccess(Message message)
    {
        m_successUI.SetActive(true);
    }

    private void OnGameFailure(Message message)
    {
        m_failureUI.SetActive(true);
    }
}