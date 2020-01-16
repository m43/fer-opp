using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

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

        // list of all roles

        public static readonly List<string> AllRoles = new List<string>(new string[] {User, Coach, Board, Admin});

        public const string User = "User";
        public const string Coach = "Coach";
        public const string Board = "Board";
        public const string Admin = "Admin";

        public static bool IsValidUserRoleName(string roleName)
        {
            return roleName.Equals(User) || roleName.Equals(Coach) || roleName.Equals(Board) || roleName.Equals(Admin);
        }

        public static async Task<bool> CheckRoleExists(RoleManager<IdentityRole> roleManager, string role)
        {
            if (role == null)
            {
                throw new ArgumentException("Given role cannot be null.");
            }

            return await roleManager.RoleExistsAsync(role);
        }
    }
}