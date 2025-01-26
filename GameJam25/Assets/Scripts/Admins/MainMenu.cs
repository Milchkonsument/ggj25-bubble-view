using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header ("Camera Animation")]
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _distance;
    private float _movedDistance;

    [Header("UI Elements")]
    [SerializeField]
    private CanvasGroup _menuCanvasGroup;
    [SerializeField]
    private GameObject _menuPanel;
    [SerializeField]
    private GameObject _settingsPanel;
    [SerializeField]
    private Toggle _fullscreenToggle;
    [SerializeField]
    private float _fadeDuration;
    [SerializeField]
    private Button[] _menuButtons;


    [Header("PostProcessing")]
    [SerializeField]
    private Volume _ppVolume;
    [SerializeField]
    private TVController _menuTV;
    private bool _coroutineRunning;

    void Start()
    {
        Debug.Log("Started");
        foreach (Button btn in _menuButtons)
        {
            btn.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_movedDistance < _distance)
        {
            float movement = _moveSpeed * Time.deltaTime;
            _camera.transform.position += _camera.transform.forward * movement;
            _movedDistance += movement;
        }
        else
        {
            if(!_coroutineRunning)
            {
                StartCoroutine(FadeMenu());
                _coroutineRunning = true;
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        _menuPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        _settingsPanel.SetActive(false);
        _menuPanel.SetActive(true);
    }

    public void ToggleFullscreen()
    {
        bool value = _fullscreenToggle.isOn;
        Debug.Log($"Fullscreen is {value}");
        Screen.fullScreen = value;
    }

    private void StartAnimation()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    IEnumerator FadeMenu()
    {
        float timeElapsed = 0f;

        while(timeElapsed < _fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float weight = Mathf.Lerp(0f, 1f, timeElapsed / _fadeDuration);
            _ppVolume.weight = weight;
            yield return null;
        }

        _menuTV.TurnON();
        Debug.Log("ON");
        yield return new WaitForSeconds(3);

        float secondTimeElapsed = 0f;
        while (secondTimeElapsed < _fadeDuration - 1)
        {
            secondTimeElapsed += Time.deltaTime;
            _menuCanvasGroup.alpha = Mathf.Lerp(0f, 1f, secondTimeElapsed / (_fadeDuration - 1));
            yield return null;
        }
        foreach(Button btn in _menuButtons)
        {
            btn.interactable = true;
        }
        _menuCanvasGroup.alpha = 1f;
    }
}
