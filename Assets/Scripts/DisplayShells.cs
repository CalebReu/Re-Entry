using UnityEngine;
using TMPro;
using System;
public class DisplayShells : MonoBehaviour
{
    private int shells;
[SerializeField] private TextMeshProUGUI txt;

    public void SetShells(int s){
        shells =s;
        updateText();
   }
   private void updateText(){
    String s = "";
    for(int i =0; i< shells; i++){
        s = s+"I";
    }
    txt.SetText(s);
   }
   public void Disable(){
    txt.enabled = false;
   }
   public void enable(){
    txt.enabled = true;
   }

}
