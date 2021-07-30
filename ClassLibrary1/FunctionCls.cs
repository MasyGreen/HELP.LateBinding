using System.Collections.Generic;
using ConsoleApp;

namespace ClassLibrary1
{
    public class FunctionCls
    {
        public static void CreateList(List<BaseObj> list, string adds)
        {
            for (int i = 0; i < 3; i++)
                list.Add(new BaseObj($"code:{i}", $"name:{i}", "1", adds));
        }
    }
}
