using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
   [SerializeField] public Text playerName;
   public Text id;
   void Start(){
    Debug.Log(LoginController.player.name);
           playerName.text = "" + LoginController.player.name;
        id.text = "" + LoginController.player.id;

   }
 
   public static int getPlayerId(){
      return LoginController.player.id;
   }
}
