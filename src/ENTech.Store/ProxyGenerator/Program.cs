using System;
using System.Reflection;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.IO;

namespace ProxyGenerator
{
    enum HttpMethods
    {
        Get, Post, Put, Delete, Patch, Unknown
    }
    class Program
    {
        static Dictionary<Type, string> _typesToRegister = new Dictionary<Type, string>();

        static string RegisterAndGetTypeName(Type t)
        {
            if (!_typesToRegister.ContainsKey(t))
            {
                _typesToRegister.Add(t, t.FullName);

                foreach (var p in t.GetProperties())
                {
                    if (p.PropertyType.IsClass)
                        RegisterAndGetTypeName(p.PropertyType);
                }
            }

            return _typesToRegister[t];
        }


        static HttpMethods GetHttpMethod(MethodInfo m)
        {
            var postAttr = m.GetCustomAttribute<HttpPostAttribute>();
            var getAttr = m.GetCustomAttribute<HttpGetAttribute>();
            var putAttr = m.GetCustomAttribute<HttpPutAttribute>();
            var deleteAttr = m.GetCustomAttribute<HttpDeleteAttribute>();
            var patchAttr = m.GetCustomAttribute<HttpPatchAttribute>();

            if (postAttr != null) return HttpMethods.Post;
            else if (getAttr != null) return HttpMethods.Get;
            else if (putAttr != null) return HttpMethods.Put;
            else if (deleteAttr != null) return HttpMethods.Delete;
            else if (patchAttr != null) return HttpMethods.Patch;
            else return HttpMethods.Unknown;
        }

        static void Main(string[] args)
        {
            var controllerType = typeof(ApiController);
            var a = typeof(ENTech.Store.Api.ForStoreAdmin.Controllers.ProductController).Assembly;
            var controllers = from t in a.ExportedTypes
                              where t.IsSubclassOf(controllerType)
                              let controllerRoute = t.GetCustomAttribute<RoutePrefixAttribute>()
                              let controllerPath = controllerRoute.Prefix
                              select
                                  new
                                  {
                                      Namespace = t.Namespace,
                                      Name = t.Name,
                                      Methods = from m in t.GetMethods()
                                                let r = m.GetCustomAttribute<RouteAttribute>()
                                                let rt = m.GetCustomAttributes<ResponseTypeAttribute>()
                                                where r != null && rt != null// && mp!=null
                                                let responseTypeAttr = m.GetCustomAttribute<ResponseTypeAttribute>()

                                                select new
                                                {
                                                    Name = m.Name,
                                                    Path = controllerPath + "/" + r.Name,
                                                    HttpMethod = GetHttpMethod(m),
                                                    Params = from p in m.GetParameters()
                                                             select new
                                                             {
                                                                 Name = p.Name,
                                                                 TypeName = RegisterAndGetTypeName(p.ParameterType)
                                                             },
                                                    ReturnTypeName = RegisterAndGetTypeName(responseTypeAttr.ResponseType)
                                                }
                                  };

            var ca = controllers.ToArray();
            {
                //write controllers to C#
                var s = "";
                foreach (var c in controllers)
                {
                    s += "namespace " + c.Namespace + " {\n";//ns begins
                    s += "public partial class " + c.Name.Replace("Controller", "Client") + "{ \n";//class begins
                    foreach (var m in c.Methods)
                    {
                        var parameters = String.Join(",", m.Params.Select(p => p.TypeName + " " + p.Name));
                        s += "public " + m.ReturnTypeName + " " + m.Name + "(" + parameters + ") {\n";//method begins
                        s += "//todo: call " + m.HttpMethod + " " + m.Path + "\n";
                        s += "}\n";//method ends
                    }

                    s += "}\n";//class ends
                    s += "}\n";//ns ends
                }

                File.WriteAllText("2.cs", s);
            }

            //write response/reques/ dto classes to C#
            {
                var s = "";
                foreach (var t in _typesToRegister.Keys)
                {
                    s += "namespace " + t.Namespace + " {\n";//ns begins
                    s += "public class " + t.Name + "{ \n";//class begins
                    foreach (var p in t.GetProperties())
                    {
                        s += "public " + p.PropertyType.FullName + " " + p.Name + " { get; set; }\n";
                    }
                    s += "}\n";//class ends
                    s += "}\n";//ns ends
                }

                File.WriteAllText("1.cs", s);
            }

        }
    }
}
