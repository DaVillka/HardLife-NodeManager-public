﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ST.Library.UI.NodeEditor
{
    /// <summary>
    /// STNode节点特性
    /// 用于描述STNode开发者信息 以及部分行为
    /// </summary>
    public class STNodeAttribute : Attribute
    {
        /// <summary>
        /// 获取STNode节点期望在树形控件的路径
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// 获取STNode节点的作者名称
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// 获取STNode节点的作者邮箱
        /// </summary>
        public string Mail { get; }

        /// <summary>
        /// 获取STNode节点的作者链接
        /// </summary>
        public string Link { get; }

        /// <summary>
        /// 获取STNode节点的描述信息
        /// </summary>
        public string Description { get; }

        private static readonly char[] m_ch_splitter = new char[] { '/', '\\' };
        private static readonly Regex m_reg = new Regex(@"^https?://", RegexOptions.IgnoreCase);
        /// <summary>
        /// 构造一个STNode特性
        /// </summary>
        /// <param name="strPath">期望路径</param>
        public STNodeAttribute(string strPath) : this(strPath, null, null, null, null) { }
        /// <summary>
        /// 构造一个STNode特性
        /// </summary>
        /// <param name="strPath">期望路径</param>
        /// <param name="strDescription">描述信息</param>
        public STNodeAttribute(string strPath, string strDescription) : this(strPath, null, null, null, strDescription) { }
        /// <summary>
        /// 构造一个STNode特性
        /// </summary>
        /// <param name="strPath">期望路径</param>
        /// <param name="strAuthor">STNode作者名称</param>
        /// <param name="strMail">STNode作者邮箱</param>
        /// <param name="strLink">STNode作者链接</param>
        /// <param name="strDescription">STNode节点描述信息</param>
        public STNodeAttribute(string strPath, string strAuthor, string strMail, string strLink, string strDescription)
        {
            if (!string.IsNullOrEmpty(strPath))
            {
                strPath = strPath.Trim().Trim(m_ch_splitter).Trim();
            }

            Path = strPath;

            Author = strAuthor;
            Mail = strMail;
            Description = strDescription;
            if (string.IsNullOrEmpty(strLink) || strLink.Trim() == string.Empty)
            {
                return;
            }

            strLink = strLink.Trim();
            Link = m_reg.IsMatch(strLink) ? strLink : "http://" + strLink;
        }

        private static readonly Dictionary<Type, MethodInfo> m_dic = new Dictionary<Type, MethodInfo>();
        /// <summary>
        /// 获取类型的帮助函数
        /// </summary>
        /// <param name="stNodeType">节点类型</param>
        /// <returns>函数信息</returns>
        public static MethodInfo GetHelpMethod(Type stNodeType)
        {
            if (m_dic.ContainsKey(stNodeType))
            {
                return m_dic[stNodeType];
            }

            MethodInfo mi = stNodeType.GetMethod("ShowHelpInfo");
            if (mi == null)
            {
                return null;
            }

            if (!mi.IsStatic)
            {
                return null;
            }

            ParameterInfo[] ps = mi.GetParameters();
            if (ps.Length != 1)
            {
                return null;
            }

            if (ps[0].ParameterType != typeof(string))
            {
                return null;
            }

            m_dic.Add(stNodeType, mi);
            return mi;
        }
        /// <summary>
        /// 执行对应节点类型的帮助函数
        /// </summary>
        /// <param name="stNodeType">节点类型</param>
        public static void ShowHelp(Type stNodeType)
        {
            MethodInfo mi = STNodeAttribute.GetHelpMethod(stNodeType);
            if (mi == null)
            {
                return;
            }

            _ = mi.Invoke(null, new object[] { stNodeType.Module.FullyQualifiedName });
        }
    }
}
