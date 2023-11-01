using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : BasePanel
{
    private static readonly string abName = "ui";
    private static readonly string resName = "LoadingPanel";

    public LoadingPanel() : base(new UIType(abName, resName)){}

}
