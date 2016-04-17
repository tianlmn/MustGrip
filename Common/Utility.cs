using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Utility
    {
        public static List<T> ConvertToTrees<T>(List<T> list,string pkey,string ckey,string s)
        {
            List<T> result = new List<T>();
            List<T> visitList = new List<T>();
            try
            {
                Type t = typeof (T);
                PropertyInfo parent = t.GetProperty(pkey);
                PropertyInfo child = t.GetProperty(ckey);
                PropertyInfo self = t.GetProperty(s);
                foreach (T l in list)
                {
                    T find = default(T);
                    bool isfind = false;
                    var pl = parent.GetValue(l);
                    var cl = child.GetValue(l);
                    for (int i = 0; i < visitList.Count; i++)
                    {
                        if (child.GetValue(visitList[i]).ToString() == pl.ToString())
                        {
                            find = visitList[i];
                            isfind = true;
                            break;
                        }
                        var list1 = self.GetValue(visitList[i]) as List<T>;
                        if (list1 != null && list1.Count > 0)
                        {
                            visitList.AddRange(list1);
                        }
                    }
                    if (isfind && find != null)
                    {
                        var list1 = self.GetValue(find) as List<T>;
                        if (list1 != null) list1.Add(l);
                    }
                    else
                    {
                        result.Add(l);
                        visitList.Add(l);
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return result;
        }
    }
}
