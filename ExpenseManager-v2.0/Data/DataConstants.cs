namespace ExpenseManager_v2._0.Data
{
    public static class DataConstants
    {
        public class ApplicationUser
        {
            public const int FullNameMinlength = 5;
            public const int FullNameMaxlength = 40;
            public const int PasswordMinlength = 6;
            public const int PasswordMaxlength = 100;            
            public const string ErrorMessagePassword = "The {0} must be at least {2} and at max {1} characters long.";
            public const string ErrorMessageConfirmPassword = "The password and confirmation password do not match.";
        }

        public class Expense
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;

            public const int NotesMinLength = 10;
            public const int NotesMaxLength = 300;

            public const string ErrorMessageNotes = "The field must be with a minimum length of {2}";
            public const string ErrorMessageAmount = "Amount must be a positive number";
        }

        public class Income
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;

            public const int NotesMinLength = 10;
            public const int NotesMaxLength = 300;

            public const string ErrorMessageNotes = "The field must be with a minimum length of {2}";
            public const string ErrorMessageAmount = "Amount must be a positive number";
        }

        public class Categorys
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }

        public class Credit
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;

            public const int NotesMinLength = 10;
            public const int NotesMaxLength = 300;

            public const string ErrorMessageNotes = "The field must be with a minimum length of {2}";
            public const string ErrorMessageAmount = "Amount must be a positive number";
        }

        public class BorrowedItem
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }

        public class InstallmentLoan
        {
            public const string ErrorMessageAmount = "Amount must be a positive number";
        }

        public class Saving
        {
            public const string ErrorMessageAmount = "Amount must be a positive number";
        }

        public class ContributionToSaving
        {
            public const string ErrorMessageAmount = "Amount must be a positive number";
        }
    }
}
