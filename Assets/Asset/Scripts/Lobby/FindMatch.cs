using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindMatch : MonoBehaviour
{
    [SerializeField]
    private GameObject FingMatchBanner;
    [SerializeField]
    private Button ButtonCancelFind;
    [SerializeField]
    private Text TextFindMatchTime;

    private LobbyController lobby;

    private readonly float timeDelay = 1f;

    private int timeCount;
    private float curTimeDelay;

    private void Awake()
    {
        lobby = FindObjectOfType<LobbyController>();

        ButtonCancelFind.onClick.AddListener(OnClickCancel);
    }

    private void Update()
    {
        if (FingMatchBanner.activeSelf)
        {
            if (curTimeDelay <= 0)
            {
                timeCount += 1;
                curTimeDelay = timeDelay;
                TextFindMatchTime.text = "Find Match   " + timeCount.ToString();
            }
            else
            {
                curTimeDelay -= Time.deltaTime;
            }
        }
    }

    private void OnClickCancel()
    {
        activeFindMatch(false);
        lobby.CancelFindMatch();
    }

    public void activeFindMatch(bool active)
    {
        FingMatchBanner.SetActive(active);
        timeCount = 0;
        TextFindMatchTime.text = "Find Match   " + timeCount.ToString();
    }
}
