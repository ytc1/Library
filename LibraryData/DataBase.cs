﻿using System;
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
        public void AddMember(Member member)
        {
            using (var context = new DataClasses1DataContext(_connectionString))
            {
                context.Members.InsertOnSubmit(member);
                context.SubmitChanges();
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
                context.LoadOptions = loadOptions;
                return context.Books.ToList();
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
    }
}