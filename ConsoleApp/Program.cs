using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Dynamic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Start: {DateTime.Now:d}\n");
            List<BaseObj> mylist = new();
            mylist.Add(new("FIRST", "NAME", "NULL"));

            Console.WriteLine($"==================FIRST=================\n");
            foreach (BaseObj obj in mylist)
                Console.WriteLine($"{obj.Dll}: {obj.Code}, {obj.Name}");

            dynamic functionfromcls1 = new FunctionFromCLS1();
            functionfromcls1.FunctionCls(mylist, "dynamic");
            Console.WriteLine($"==================AFTER dynamic=================\n");
            foreach (BaseObj obj in mylist)
                Console.WriteLine($"{obj.Dll}: {obj.Code}, {obj.Name}");

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
                object[] parameters = new object[] { mylist ,""};
                mi.Invoke(ob, parameters);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            #endregion
            Console.WriteLine($"==================AFTER ClassLibrary1=================\n");
            foreach (BaseObj obj in mylist)
                Console.WriteLine($"{obj.Dll}: {obj.Code}, {obj.Name}");

            #region ClassLibrary2
            try { asm = Assembly.LoadFile($"{Directory.GetCurrentDirectory()}\\ClassLibrary2.dll"); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            try
            {
                tp = asm.GetType("ClassLibrary2.FunctionCls");
                object ob = Activator.CreateInstance(tp);
                MethodInfo mi;
                mi = tp.GetMethod("CreateList");
                object[] parameters = new object[] { mylist ,""};
                mi.Invoke(ob, parameters);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            #endregion

            Console.WriteLine($"==================AFTER ClassLibrary2=================\n");
            foreach (BaseObj obj in mylist)
                Console.WriteLine($"{obj.Dll}: {obj.Code}, {obj.Name}");


            Console.WriteLine("");
            Console.WriteLine($"End: {DateTime.Now:d}");


            Console.ReadKey();
        }
    }
   
    public class BaseObj
    {
        public string Code;
        public string Name;
        public string Dll;

        public BaseObj(string code, string name, string dll, string adds = "")
        {
            Code = code;
            Name = name;
            Dll = $"{dll} ({adds})";
        }
    }

    public class FunctionFromCLS1 : DynamicObject
    {
        public override bool TryInvokeMember (
            InvokeMemberBinder binder, 
            object[] args,
            out object result)
        {
            Console.WriteLine (binder.Name + " was called");

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
                mi.Invoke(ob, args);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            #endregion


            result = null;
            return true;
        }
    }
}
