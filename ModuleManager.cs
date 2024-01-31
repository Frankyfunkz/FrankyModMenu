using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrankyModMenu
{
    public static class ModuleManager
    {
        public static Action onStartAction;
        public static Action onUpdateAction;
        public static Action onQuitAction;
        public static Action onPatchAction;
        public static Action<int, string> onSceneLoadAction;

        static ModuleManager()
        {
            foreach (Type type in ((IEnumerable<Type>)Assembly.GetExecutingAssembly().GetTypes()).Where<Type>((Func<Type, bool>)(type => !type.IsNested && type.BaseType == typeof(Module))))
                ((Module)Activator.CreateInstance(type)).Bind();
        }

        public static void OnStart()
        {
            Action onStartAction = ModuleManager.onStartAction;
            if (onStartAction == null)
                return;
            onStartAction();
        }

        public static void OnPatch()
        {
            Action onPatchAction = ModuleManager.onPatchAction;
            if (onPatchAction == null)
                return;
            onPatchAction();
        }

        public static void OnSceneLoad(int buildIndex, string sceneName)
        {
            Action<int, string> onSceneLoadAction = ModuleManager.onSceneLoadAction;
            if (onSceneLoadAction == null)
                return;
            onSceneLoadAction(buildIndex, sceneName);
        }

        public static void OnUpdate()
        {
            Action onUpdateAction = ModuleManager.onUpdateAction;
            if (onUpdateAction == null)
                return;
            onUpdateAction();
        }

        public static void OnQuit()
        {
            Action onQuitAction = ModuleManager.onQuitAction;
            if (onQuitAction == null)
                return;
            onQuitAction();
        }
    }
}
