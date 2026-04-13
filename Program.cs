using System;
using System.Collections.Generic;

// Represents a single book with its basic information
class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int Year { get; set; }

    // Sets up a new book with all the info we need
    public Book(string title, string author, string isbn, int year)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        Year = year;
    }

    // Prints the book's details to the console in a clean format
    public void DisplayDetails()
    {
        Console.WriteLine($"  Title:  {Title}");
        Console.WriteLine($"  Author: {Author}");
        Console.WriteLine($"  ISBN:   {ISBN}");
        Console.WriteLine($"  Year:   {Year}");
    }
}

// Manages the collection of books and the operations we can do on them
class Library
{
    // This list holds all the books that have been added
    private List<Book> books = new List<Book>();

    // Adds a book to the list and confirms it was added
    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"\n\"{book.Title}\" was added to the library.");
    }

    // Shows every book in the library, or lets the user know if it's empty
    public void DisplayAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("\nThe library has no books yet.");
            return;
        }

        Console.WriteLine($"\n--- Library Contents ({books.Count} book(s)) ---");

        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"\nBook {i + 1}:");
            books[i].DisplayDetails();
        }

        Console.WriteLine("\n--- End of List ---");
    }

    // Searches for books by title using a partial, case-insensitive match
    public bool SearchByTitle(string searchTerm)
    {
        string lowerTerm = searchTerm.ToLower();
        List<Book> results = new List<Book>();

        // Go through every book and collect any that match the search term
        foreach (Book book in books)
        {
            if (book.Title.ToLower().Contains(lowerTerm))
            {
                results.Add(book);
            }
        }

        if (results.Count == 0)
        {
            Console.WriteLine($"\nNo books found matching \"{searchTerm}\".");
            return false;
        }

        Console.WriteLine($"\n--- Search Results for \"{searchTerm}\" ({results.Count} match(es)) ---");

        foreach (Book book in results)
        {
            Console.WriteLine();
            book.DisplayDetails();
        }

        return true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();
        bool running = true;

        Console.WriteLine("=== Library Management System ===");

        // Keep showing the menu until the user chooses to exit
        while (running)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("  1. Add a book");
            Console.WriteLine("  2. View all books");
            Console.WriteLine("  3. Search for a book by title");
            Console.WriteLine("  4. Exit");
            Console.Write("\nEnter your choice (1-4): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBookFromInput(library);
                    break;

                case "2":
                    library.DisplayAllBooks();
                    break;

                case "3":
                    Console.Write("\nEnter a title to search for: ");
                    string searchTerm = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(searchTerm))
                    {
                        Console.WriteLine("Search term cannot be empty.");
                    }
                    else
                    {
                        try
                        {
                            library.SearchByTitle(searchTerm);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred during search: {ex.Message}");
                        }
                    }
                    break;

                case "4":
                    running = false;
                    Console.WriteLine("\nGoodbye!");
                    break;

                default:
                    Console.WriteLine("\nInvalid choice. Please enter a number between 1 and 4.");
                    break;
            }
        }
    }

    // Collects and validates all the input needed to create a new book
    static void AddBookFromInput(Library library)
    {
        try
        {
            Console.WriteLine("\n-- Add a New Book --");

            Console.Write("Title: ");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty. Book was not added.");
                return;
            }

            Console.Write("Author: ");
            string author = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Author cannot be empty. Book was not added.");
                return;
            }

            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("ISBN cannot be empty. Book was not added.");
                return;
            }

            // Keep asking until the user enters a valid year
            int year = 0;
            bool validYear = false;

            while (!validYear)
            {
                Console.Write("Publication Year: ");
                string yearInput = Console.ReadLine();

                if (int.TryParse(yearInput, out year) && year > 0)
                {
                    validYear = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid year (a positive whole number).");
                }
            }

            Book newBook = new Book(title, author, isbn, year);
            library.AddBook(newBook);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while adding the book: {ex.Message}");
        }
    }
}