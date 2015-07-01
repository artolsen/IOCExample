using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IocProject
{
    public class Container
    {
        private class RegisteredObject
        {
            private LifeCycle _lifeCycle;
            public LifeCycle LifeCycle
            {
                get
                {
                    return _lifeCycle;
                }
            }

            private Type _type;
            public Type Type
            {
                get
                {
                    return _type;
                }
            }
            public RegisteredObject(Type type, LifeCycle lifeCycle)
            {
                _type = type;
                _lifeCycle = lifeCycle;
            }
        }
        private readonly Dictionary<Type, RegisteredObject> _registrations = new Dictionary<Type, RegisteredObject>();
        private readonly Dictionary<Type, object> _insances = new Dictionary<Type, object>();


        public void Register<T, TS>(LifeCycle lifeCycle = LifeCycle.Transiant)
        {
            _registrations.Add(typeof(T), new RegisteredObject(typeof(TS), lifeCycle));
        }

        public T Resolve<T>()
        {
            return (T)GetObject(typeof(T));

        }

        private object GetObject(Type parameterType)
        {
            if (!_registrations.ContainsKey(parameterType))
            {
                throw new Exception("Type " + parameterType.Name + " not registered");
            }
            else
            {
                LifeCycle lifeCycle = (from r in _registrations
                                       where r.Key == parameterType
                                       select r.Value.LifeCycle).FirstOrDefault();

                // if it's a singleton life cycle, see if we have one and get that insstance
                if (lifeCycle == LifeCycle.Singleton)
                {
                    if (_insances.ContainsKey(parameterType))
                    {
                        return _insances[parameterType];
                    }
                }
                var concreteType = _registrations[parameterType];

                var constructorParams = GetConstructorParams(concreteType.Type);

                var instance = Activator.CreateInstance(concreteType.Type, constructorParams.ToArray());
                if (lifeCycle == LifeCycle.Singleton)
                {
                    _insances.Add(parameterType, instance);
                }
                return instance;
            }
        }

        private IEnumerable<object> GetConstructorParams(Type concreteType)
        {
            var constructor = concreteType.GetConstructors().Single();

            var cparams = constructor.GetParameters();

            foreach (var parameterInfo in cparams)
            {
                yield return GetObject(parameterInfo.ParameterType);
            }
        }

    }
}
