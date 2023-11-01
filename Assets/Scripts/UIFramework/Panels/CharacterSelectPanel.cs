using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectPanel : BasePanel
{
    private static readonly string abName = "ui";
    private static readonly string resName = "CharacterSelectPanel";

    public CharacterSelectPanel() : base(new UIType(abName, resName)){}

    public override void OnEnter()
    {
        
    }
    
}
