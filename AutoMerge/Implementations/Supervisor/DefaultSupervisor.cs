using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMerge.Interfaces.Supervisor;

namespace AutoMerge.Implementations.Supervisor
{
    public class DefaultSupervisor : ISupervisor
    {
        private Dictionary<Type, Type> _typesUnderControl { get; set; }

        private static ISupervisor _supervisor = new DefaultSupervisor();
        public DefaultSupervisor()
        {
            _typesUnderControl = new Dictionary<Type, Type>();
            Bind();
        }


        public  ISupervisor GetSupervisor()
        {
            return _supervisor;
        }
        public T GetImplementation<T>() 
        {
            return (T)Activator.CreateInstance(_typesUnderControl[typeof(T)]);
        }


        private void Bind()
        {
            // Add your implementations here
            Register<ISupervisor, DefaultSupervisor>();
            Register<IList<int>, List<int>>();
        }
        private void Register<TI, TC>()
        {
            _typesUnderControl.Add(typeof(TI), typeof(TC));
        }
    }
}
