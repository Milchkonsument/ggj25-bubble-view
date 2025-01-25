using UnityEngine;
using UnityEngine.Video;

public class TVController : MonoBehaviour
{
    private VideoPlayer player;
    public bool IsOn => _isOn;

    private bool _isOn;
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnON()
    {
        player.Play();
        _isOn = true;
    }

    public void TurnOFF()
    {
        player.Stop();
        //player.frame = 0;
        _isOn = false;
    }
}
