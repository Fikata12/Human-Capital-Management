namespace HCM.Common
{
    public static class ValidationConstants
    {
        public static class Person
        {
            public const int FirstNameMaxLength = 30;
            public const int LastNameMaxLength = 30;
            public const int EmailMaxLength = 256;
            public const int JobTitleMaxLength = 50;
        }

        public static class Department
        {
            public const int NameMaxLength = 50;
        }

        public static class User
        {
            public const int UsernameMaxLength = 256;
            public const int EmailMaxLength = 256;
        }

        public static class Role
        {
            public const int NameMaxLength = 256;
        }
    }
}

