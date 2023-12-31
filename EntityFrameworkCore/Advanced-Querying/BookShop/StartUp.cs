﻿namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            string input = Console.ReadLine();
            //int inYear = int.Parse(Console.ReadLine());


            Console.WriteLine(GetBooksByAuthor(db, input));
        }


        //02. Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            if (!Enum.TryParse<AgeRestriction>(command, true, out var ageRestriction))
            {
                return $"{command} is not valid ";
            }

            var books = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => new
                {
                    b.Title
                })
                .OrderBy(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        //03. Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold &&
                b.Copies < 5000)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.Title));
        }

        //04. Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    bookTitle = b.Title,
                    bookPrice = b.Price
                })
                .OrderByDescending(b => b.bookPrice)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.bookTitle} - ${b.bookPrice}"));
        }

        //05. Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => new
                {
                    bookId = b.BookId,
                    bookTitle = b.Title
                })
                .OrderBy(b => b.bookId)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.bookTitle));
        }

        // 06. Book Titles by Category
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();


            var books = context.Books
                .Where(b => b.BookCategories.Any(bc=>categories.Contains(bc.Category.Name.ToLower())))
                .Select(b => new
                {
                    bookCategories = b.BookCategories,
                    bookTitle = b.Title
                })
                .OrderBy(b => b.bookTitle)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.bookTitle));
        }

        // 07. Released Before Date ---------- ?
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            return "";
        }

        // 08. Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Books
                .Where(b => b.Author.FirstName.EndsWith(input))                
                .Select(b => new
                {
                    AuthorName = b.Author.FirstName + " " + b.Author.LastName,
                })
                .Distinct()
                .ToList();

            return string.Join(Environment.NewLine, authors.Select(b => b.AuthorName));
        }


        // 09. Book Search
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => new
                {
                    BookTitle = b.Title
                })
                .OrderBy(b => b.BookTitle)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => b.BookTitle));
        }

        //10. Book Search by Author
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .Select(b => new
                {
                    b.BookId,
                    BookTitle = b.Title,
                    AuthorName = b.Author.FirstName + " " + b.Author.LastName
                })
                .OrderBy(b => b.BookId)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.BookTitle} ({b.AuthorName})"));
        }


        //11. Count Books --------------?
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            int count = 0;
            return count;
        }


    }



}


