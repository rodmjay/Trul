namespace Trul.Infrastructure.Crosscutting.Security
{
    /// <summary>
    /// Authentication Factory
    /// </summary>
    public static class AuthenticationFactory
    {
        #region Members

        static IAuthenticationFactory _currentAuthenticationFactory = null;

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Set the  authentication factory to use
        /// </summary>
        /// <param name="authenticationFactory"></param>
        public static void SetCurrent(IAuthenticationFactory authenticationFactory)
        {
            _currentAuthenticationFactory = authenticationFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IAuthentication CreateAuthentication()
        {
            return (_currentAuthenticationFactory != null) ? _currentAuthenticationFactory.Create() : null;
        }

        #endregion
    }
}
