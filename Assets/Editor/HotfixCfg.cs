using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using XLua;

public static class HotfixCfg 
{
    [Hotfix]
    public static List<Type> by_property
    {
        get
        {
            //从程序集中获取全部类信息
            var allTypes = Assembly.Load("Assembly-CSharp").GetTypes();
            var nameSpace = new List<string>();
            //遍历所有类筛选符合规则的命名空间
            foreach (var t in allTypes)
            {
                if (t.Namespace != null && (t.Namespace.StartsWith("XLuaProject", StringComparison.CurrentCulture)))
                {
                    if (!nameSpace.Contains(t.Namespace))
                    {
                        nameSpace.Add(t.Namespace);
                    }
                }
            }
            var retList = new List<Type>();
            var sb = new StringBuilder();
            //遍历所有类筛选所有包含该命名空间的Type对象
            foreach (var t in allTypes)
            {
                if (nameSpace.Contains(t.Namespace))
                {
                    retList.Add(t);
                    sb.AppendLine(t.FullName);
                }
            }
            //输出所有Type信息到项目根目录HotTypes.txt文本中
            File.WriteAllText(Path.Combine(Application.dataPath, "../HotTypes.txt"), sb.ToString());
            return retList;
        }
    }
}