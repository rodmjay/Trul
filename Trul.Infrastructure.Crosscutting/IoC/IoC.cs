using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Framework;

namespace Trul.Infrastructure.Crosscutting.IoC
{
    public static class IoC
    {
        private static readonly object LockObj = new object();

        private static IContainer container;

        public static IContainer Container {
            get { return container; }

            private set {
                lock(LockObj) {
                    container = value;
                }
            }
        }

        public static T Resolve<T>() {
            return container.Resolve<T>();
        }

        public static object Resolve(Type type) {
            return container.Resolve(type);
        }

        public static void SetContainer(IContainer container) {
            Container = container;
        }
    }
}
