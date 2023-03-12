using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerModel : MonoBehaviour
{
    public SpriteRenderer sprite;
    public TMP_Text nameText;

    public void Initialize()
    {
        sprite.color = Color.red;
        nameText.text = PhotonNetwork.LocalPlayer.NickName;
    }
}
