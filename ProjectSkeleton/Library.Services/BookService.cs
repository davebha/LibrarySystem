﻿using Library.DataAccess.Interface;
using Library.DomainModels;
using Library.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;


        }
        public async Task<Book>  AddBook(int authorId, string title)
        {
            if (authorId <= 0)
            {
                throw new ArgumentException("Author ID must be a positive integer greater than 0! ");
            }
            
            if(title.Length==0||title=="" || title==" ")
            {
                throw new ArgumentException("Title is mandatory!");
            }

            Book newBook = new Book() { Title = title, AuthorId = authorId, CreatedOn = DateTime.Now };


            return await _bookRepository.Create(newBook);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            throw new NotImplementedException();
        }


    }
}
