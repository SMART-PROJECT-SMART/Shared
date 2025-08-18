namespace Shared.Common
{
    public static class SharedConstants
    {
        public static class Config
        {
            public const string ICD_DIRECTORY = "ICD";
            public const string JSON_SEARCH_PATTERN = "*.json";
        }

        public static class Ports
        {
            public const int BASE_PORT = 8000;
        }

        public static class TelemetryCompression
        {
            public const int TAIL_ID_ITEM_START_BIT_ARRAY_INDEX = 0;
            public const int TAIL_ID_ITEM_BIT_LENGTH = 10;
            public const int BIT_SHIFT_BASE = 1;
        }
    }
}
