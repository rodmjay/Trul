using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Framework.Rules;

namespace Trul.Infrastructure.Crosscutting.Rules
{
    public class ClientValidatorVisitorFactory
    {
        #region Members

        static IClientValidatorVisitorFactory _currentFactory = null;

        #endregion

        #region Public Methods

        public static void SetCurrent(IClientValidatorVisitorFactory factory)
        {
            _currentFactory = factory;
        }

        public static IClientValidatorVisitor Create()
        {
            return (_currentFactory != null) ? _currentFactory.Create() : null;
        }

        #endregion
    }
}
