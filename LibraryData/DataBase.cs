using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryData
{
    public class DataBase
    {
        private string _connectionString { get; set; }
        public DataBase(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddBook(Book book)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                context.Books.InsertOnSubmit(book);
                context.SubmitChanges();
            }
        }
        public void AddAuthor(Author author)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                context.Authors.InsertOnSubmit(author);
                context.SubmitChanges();
            }
        }
        public bool CheckAuthor(string firstName, string lastName)
        {
            using(var context = new DataClasses1DataContext(_connectionString))
            {
                return context.Authors.Any(a => a.FirstName == firstName && a.LastName == lastName);
            }
        }
        public Author GetAuthor(string firstName,string lastName)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                return context.Authors.Where(a => a.FirstName == firstName && a.LastName == lastName).First();
            }
        }
        public void AddSubject(Subject subject)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                context.Subjects.InsertOnSubmit(subject);
                context.SubmitChanges();
            }
        }
        public void AddBookSubject(BookSubject bookSubject)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                context.BookSubjects.InsertOnSubmit(bookSubject);
                context.SubmitChanges();
            }
        }
        public void AddBorrow(Borrow borrow)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                context.Borrows.InsertOnSubmit(borrow);
                context.SubmitChanges();
            }
        }
        public IEnumerable<Book> GetBooks()
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Book>(b => b.Author);
                loadOptions.LoadWith<Book>(b => b.BookSubjects);
                loadOptions.LoadWith<BookSubject>(b => b.Subject);
                context.LoadOptions = loadOptions;
                return context.Books.ToList();
            }
        }
        public Book GetBook(int bookId)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Book>(b => b.Author);
                loadOptions.LoadWith<Book>(b => b.BookSubjects);
                loadOptions.LoadWith<BookSubject>(b => b.Subject);
                context.LoadOptions = loadOptions;
                return context.Books.Where(b => b.Id == bookId).First();
            }
        }
        public IEnumerable<Book> GetBooksByAuthor(int authorId)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Book>(b => b.Author);
                loadOptions.LoadWith<Book>(b => b.BookSubjects);
                loadOptions.LoadWith<BookSubject>(b => b.Subject);
                context.LoadOptions = loadOptions;
                return context.Books.Where(b => b.AuthorId == authorId).ToList();
            }
        }
        public IEnumerable<Author> GetAuthors()
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Author>(a => a.Books);
                context.LoadOptions = loadOptions;
                return context.Authors.ToList();
            }
        }
        public IEnumerable<Subject> GetSubjects()
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Subject>(s => s.BookSubjects);
                context.LoadOptions = loadOptions;
                return context.Subjects.ToList();
            }
        }
        public IEnumerable<Borrow> GetBorrows(Member member)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                return context.Borrows.Where(b => b.MemberId == member.Id).ToList();
            }
        }
        public Member AddMember(string firstName, string lastName, string password)
        {
            Member member = new Member();
            string salt = PasswordHelper.GenerateRandomSalt();
            member.PasswordHash = PasswordHelper.HashPassword(password, salt);
            member.Salt = salt;
            member.FirstName = firstName;
            member.LastName = lastName;
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                context.Members.InsertOnSubmit(member);
                context.SubmitChanges();
            }
            return member;

        }
        public Member GetMember(string firstName, string lastName, string password)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                Member member = context.Members.Where(m => m.FirstName == firstName && m.LastName == lastName).First();
                bool success = PasswordHelper.PasswordMatch(password, member.PasswordHash, member.Salt);
                return success ? member : null;
            }
        }
        public bool CheckUser(string firstName, string lastName)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                return !context.Members.Any(m => m.FirstName == firstName && m.LastName == lastName);
            }
        }
        public Admin AddAdmin(string firstName, string lastName, string password)
        {
            Admin admin = new Admin();
            string salt = PasswordHelper.GenerateRandomSalt();
            admin.PasswordHash = PasswordHelper.HashPassword(password, salt);
            admin.Salt = salt;
            admin.FirstName = firstName;
            admin.LastName = lastName;
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                context.Admins.InsertOnSubmit(admin);
                context.SubmitChanges();
            }
            return admin;

        }
        public Admin GetAdmin(string firstName, string lastName, string password)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                Admin admin = context.Admins.Where(a => a.FirstName == firstName && a.LastName == lastName).First();
                bool success = PasswordHelper.PasswordMatch(password, admin.PasswordHash, admin.Salt);
                return success ? admin : null;
            }
        }
    }
}
