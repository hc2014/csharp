
using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Services.Description;
 
namespace CS.Test
{
    public class ProxyServiceTest
    {
        // 用Dictionary保存已经反射生成过的WebService和Method，省去每次调用都重新生成实例
        #region 动态调用WebService
 
        /// <summary>
        /// WebService服务列表
        /// </summary>
        private static Dictionary<string, object> ServiceDictionary = new Dictionary<string, object>();
 
        /// <summary>
        /// WebService Method方法列表
        /// </summary>
        private static Dictionary<string, System.Reflection.MethodInfo> MethodDictionary = new Dictionary<string, System.Reflection.MethodInfo>();
 
 
        /// <summary>
        /// 动态调用WebService（开发环境ping不通需要调用的WebService地址时使用）
        /// </summary>
        /// <param name="url">webservice地址</param>
        /// <param name="methodName">需要调用的方法名称</param>
        /// <param name="param">调用上方法时传入的参数</param>
        /// <returns>执行方法后返回的数据</returns>
        public static object InvokeWebService(string url, string methodName, string[] param)
        {
            // 检查是否已经存在WebService服务 和 方法
            if (!ServiceDictionary.ContainsKey(url) || !MethodDictionary.ContainsKey(url + methodName))
            {
                //客户端代理服务命名空间，可以设置成需要的值。
                string space = string.Format("ProxyServiceReference");
 
                //获取WSDL
                WebClient webClient = new WebClient();
                Stream stream = webClient.OpenRead(url + "?WSDL");
                ServiceDescription description = ServiceDescription.Read(stream);//服务的描述信息都可以通过ServiceDescription获取
                string classname = description.Services[0].Name;
 
                ServiceDescriptionImporter descriptionImporter = new ServiceDescriptionImporter();
                descriptionImporter.AddServiceDescription(description, "", "");
                CodeNamespace codeNamespace = new CodeNamespace(space);
 
                //生成客户端代理类代码
                CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
                codeCompileUnit.Namespaces.Add(codeNamespace);
                descriptionImporter.Import(codeNamespace, codeCompileUnit);
                CSharpCodeProvider provider = new CSharpCodeProvider();
 
                //设定编译参数
                CompilerParameters comilerParameters = new CompilerParameters();
                comilerParameters.GenerateExecutable = false;
                comilerParameters.GenerateInMemory = true;
                comilerParameters.ReferencedAssemblies.Add("System.dll");
                comilerParameters.ReferencedAssemblies.Add("System.XML.dll");
                comilerParameters.ReferencedAssemblies.Add("System.Web.Services.dll");
                comilerParameters.ReferencedAssemblies.Add("System.Data.dll");
 
                //编译代理类
                CompilerResults compilerResult = provider.CompileAssemblyFromDom(comilerParameters, codeCompileUnit);
                if (compilerResult.Errors.HasErrors == true)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in compilerResult.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }
 
                //生成代理实例，并调用方法
                System.Reflection.Assembly assembly = compilerResult.CompiledAssembly;
                Type service = assembly.GetType(space + "." + classname, true, true);
                
                if (!ServiceDictionary.ContainsKey(url))
                {
                    object SSOService = Activator.CreateInstance(service);
                    ServiceDictionary.Add(url, SSOService);
                }
 
                System.Reflection.MethodInfo SSOServiceMethod = service.GetMethod(methodName);
                MethodDictionary.Add(url + methodName, SSOServiceMethod);
            }
 
            return MethodDictionary[url + methodName].Invoke(ServiceDictionary[url], param);
            
        }
 
        #endregion
 
    }
