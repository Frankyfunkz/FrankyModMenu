using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrankyModMenu
{
    public class Module
    {
        public void Bind()
        {
            ModuleManager.onStartAction += new Action(this.OnStart);
            ModuleManager.onPatchAction += new Action(this.OnPatch);
            ModuleManager.onSceneLoadAction += new Action<int, string>(this.OnSceneLoad);
            ModuleManager.onUpdateAction += new Action(this.OnUpdate);
            ModuleManager.onQuitAction += new Action(this.OnQuit);
        }

        public virtual void OnStart()
        {
        }

        public virtual void OnPatch()
        {
        }

        public virtual void OnSceneLoad(int buildIndex, string sceneName)
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnQuit()
        {
        }
    }
}