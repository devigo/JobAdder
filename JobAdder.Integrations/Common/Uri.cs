using System.Configuration;

namespace JobAdder.Integrations.Common
{
    public static class Uri
    {
        #region URI

        private static string _uri = ConfigurationManager.AppSettings["JobAdderUri"];

        #endregion

        public static string Get()
        {
            // Set the default URI
            if (string.IsNullOrEmpty(_uri))
            {
                _uri = "http://private-76432-jobadder1.apiary-mock.com/";
            }

            return _uri;
        }
    }
}