using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ConsoleApp;

namespace ClassLibrary2
{
    public class FunctionCls
    {
        public static void CreateList(List<BaseObj> list, string adds)
        {
            for (int i = 0; i < 3; i++)
                list.Add(new BaseObj($"code:{i}", $"name:{i}", "2",adds));

            #region ClassLibrary1
            Assembly asm = null;
            try { asm = Assembly.LoadFile($"{Directory.GetCurrentDirectory()}\\ClassLibrary1.dll"); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            Type tp;
            try
            {
                tp = asm.GetType("ClassLibrary1.FunctionCls");
                object ob = Activator.CreateInstance(tp);
                MethodInfo mi;
                mi = tp.GetMethod("CreateList");
                object[] parameters = new object[] { list, "from cls 2" };
                mi.Invoke(ob, parameters);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            #endregion
        }
    }
}
