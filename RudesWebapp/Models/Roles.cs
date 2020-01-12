namespace RudesWebapp.Models
{
    public class Roles
    {
        /**
         * Admin is at the top of the hierarchy
         */
        public const string AdminOnly = Admin;

        /**
         * Above the board is only the admin
         */
        public const string BoardOrAbove = Board + ", " + AdminOnly;

        /**
         * Above a coach are both admin and board
         */
        public const string CoachOrAbove = Coach + ", " + BoardOrAbove;

        /**
         * Above a User are coach, board and admin. Users are at the bottom of the hierarchy
         */
        public const string UserOrAbove = User + ", " + CoachOrAbove;

        public const string User = "User";
        public const string Coach = "Coach";
        public const string Board = "Board";
        public const string Admin = "Admin";
    }
}