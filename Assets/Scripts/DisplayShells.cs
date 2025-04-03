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
    private void updateText() {
        String s = "";
        for (int i = 0; i < shells; i++) {
            s = s + "I";
        }
        switch(shells){
            case 5: txt.color = Color.cyan; break;
            case 1: txt.color = Color.red; break;
            default: txt.color = Color.white; break;
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
