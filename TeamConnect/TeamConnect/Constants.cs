namespace TeamConnect
{
    public static class Constants
    {
        public const string DATABASE_NAME = "teamconnect.db3";
        public const int DEFAULT_ID = 0;
        public const int DEFAULT_WORKING_TIME = 8;

        public static class Navigation
        {
            public const string USER = nameof(USER);
            public const string EMAIL = nameof(EMAIL);
            public const string DETAIL_PAGE = nameof(DETAIL_PAGE);
            public const string MEMBER_NOT_ADDED = nameof(MEMBER_NOT_ADDED);
        }

        public static class GoogleAPI
        {
            public const string API_KEY = "AIzaSyDCu7o1IVRZysBWkCR95qIZH52KL-1okhM";
            public const string BASE_TIMEZONE_API_URI = "https://maps.googleapis.com/maps/api/timezone/json";

            public const string LOCATION = "?location=";
            public const string TIMESTAMP = "&timestamp=";
            public const string KEY = "&key=";
        }
    }
}
