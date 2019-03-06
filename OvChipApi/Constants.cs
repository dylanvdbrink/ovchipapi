namespace OvChipApi
{
    internal static class Constants
    {
        /// <summary>
        /// The platform used to create this library.
        /// </summary>
        public const string Platform = "android";

        /// <summary>
        /// The user-agent used for every request.
        /// </summary>
        public const string UserAgent = "Dalvik/2.1.0 (Linux; U; Android 6.0.1; Nexus 6 Build/MOB30I)";

        /// <summary>
        /// The version used to create this library.
        /// </summary>
        public const string Version = "2.2.428";

        /// <summary>
        /// The OV-chipkaart OAuth 2.0 client id.
        /// </summary>
        public const string ClientId = "nmOIiEJO5khvtLBK9xad3UkkS8Ua";

        /// <summary>
        /// The OV-chipkaart OAuth 2.0 client secret.
        /// </summary>
        public const string ClientSecret = "FE8ef6bVBiyN0NeyUJ5VOWdelvQa";

        /// <summary>
        /// The OAuth 2.0 endpoint used by the android OV-chipkaart application.
        /// </summary>
        public const string OAuthUrl = "https://login.ov-chipkaart.nl/oauth2/token";
        
        /// <summary>
        /// The API endpoint used by the android OV-chipkaart application.
        /// </summary>
        public const string ApiUrl = "https://api2.ov-chipkaart.nl/femobilegateway/v1";

        /// <summary>
        /// The API endpoint used for retrieving ov cards.
        /// </summary>
        public const string CardListEndpoint = "/cards/list";

        /// <summary>
        /// The API endpoint for retrieving transactions for a card.
        /// </summary>
        public const string TransactionsEndpoint = "/transactions";
    }
}
