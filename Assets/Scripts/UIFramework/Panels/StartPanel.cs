using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : BasePanel
{
    private static readonly string abName = "ui";
    private static readonly string resName = "StartPanel";

    public StartPanel() : base(new UIType(abName, resName)){}
}
