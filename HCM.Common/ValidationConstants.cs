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
            public const int UsernameMaxLength = 30;
            public const int UsernameMinLength = 3;
            public const int EmailMaxLength = 256;
            public const int PasswordMaxLength = 100;
            public const int PasswordMinLength = 6;

            public const string UsernameRequiredMessage = "Username is required";
            public const string EmailRequiredMessage = "Email is required";
            public const string EmailInvalidMessage = "Invalid email address";
            public const string PasswordRequiredMessage = "Password is required";
            public const string PasswordLengthMessage = "Password must be between 6 and 100 characters";
            public const string ConfirmPasswordRequiredMessage = "Confirm password is required";
            public const string ConfirmPasswordMatchMessage = "Passwords do not match";
        }

        public static class Role
        {
            public const int NameMaxLength = 256;
        }
    }
}

