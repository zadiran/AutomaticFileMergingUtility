using System;
using System.Collections.Generic;

namespace DI
{
    public class Supervisor
    {

        #region infrastructure

        protected Supervisor()
        {
            _typesUnderControl = new Dictionary<Type, Type>();
            Bind();
        }

        private sealed class SupervisorInstanceCreator
        {
            private static readonly Supervisor _supervisor = new Supervisor();

            public static Supervisor GetInstance() { return _supervisor; }
        }

        public static Supervisor GetSupervisor
        {
            get { return SupervisorInstanceCreator.GetInstance(); }
        }

        #endregion

        #region bindings

        private Dictionary<Type, Type> _typesUnderControl { get; set; }

        public T GetImplementation<T>()
        {
            return (T)Activator.CreateInstance(_typesUnderControl[typeof(T)]);
        }

        private void Bind()
        {
            // Add your bindings here
            Register<IList<int>, List<int>>();
          //  Register<IProcessing, DefaultProcessing>();
          //  Register<IMerge, DefaultMerge>();
        }

        private void Register<TI, TC>()
        {
            _typesUnderControl.Add(typeof(TI), typeof(TC));
        }

        #endregion   
    }
}
