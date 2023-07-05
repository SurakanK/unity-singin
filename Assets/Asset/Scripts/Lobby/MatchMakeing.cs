using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchMakeing : MonoBehaviour
{
    [SerializeField]
    private GameObject MatchMakeingConfirm;
    [SerializeField]
    private GameObject PlayersCard;
    [SerializeField]
    private Button ButtonActive;

    private ColyseusManager colyseus;
    private int playerIndex;

    private void Awake()
    {
        colyseus = FindObjectOfType<ColyseusManager>();
        ButtonActive.onClick.AddListener(OnClickActive);
    }

    private void OnClickActive()
    {
        object data = new
        {
            active = true,
        };

        colyseus.RoomLobby.Send(E.EventAction.CONFIRM_MATCH, data);
        ActiveButton(false);
    }

    public void ActiveMatchmakeing(bool active)
    {
        MatchMakeingConfirm.SetActive(active);
        ActiveButton(active);
    }

    public void ActiveButton(bool active)
    {
        ButtonActive.gameObject.SetActive(active);
    }

    public void SetMatchDetail(PlayerData[] platerData)
    {
        ClearMatchDetail();

        foreach (PlayerData player in platerData)
        {
            playerIndex++;

            GameObject card = GameUtils.getChildGameObject(PlayersCard, "Player" + playerIndex.ToString());
            SetCardDetail(card, player.name, true);
        }
    }

    private void ClearMatchDetail()
    {
        playerIndex = 0;
        for (int i = 1; i < PlayersCard.transform.childCount + 1; i++)
        {
            GameObject card = GameUtils.getChildGameObject(PlayersCard, "Player" + i.ToString());
            SetCardDetail(card, "");
        }
    }

    private void SetCardDetail(GameObject card, string name, bool active = false)
    {
        Image imageProfile = card.GetComponent<Image>();
        Text nameText = card.GetComponentInChildren<Text>();

        imageProfile.color = active ? Color.green : Color.white;
        nameText.text = name;
    }
}
