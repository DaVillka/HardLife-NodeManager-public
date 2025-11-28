using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAGEMPServerTool
{
    internal abstract class Script
    {
        private static readonly List<Script> scripts = new List<Script>();
        protected Script()
        {
            if (this is IGlobalSubscriber) EventBusManager.Subscribe((IGlobalSubscriber)this);
            scripts.Add(this);
        }
        public static List<Script> GetScripts() { return scripts; }
        public Type[] GetTypes() { return scripts.Select(t => t.GetType()).ToArray(); }
       
        public virtual void Awake(MainForm mainForm = null) { }
        public virtual void Start(MainForm mainForm = null) { }
        public virtual void Finish(MainForm mainForm = null) { }
    }
}
