﻿using Library.DataAccess.Interface;
using Library.DomainModels;
using Library.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class BookService : IBookService
    {
        
        private readonly IBookRepository _bookRepository;
        private readonly ILibraryLogRepository _libraryLogRepository;

        public BookService(IBookRepository bookRepository,ILibraryLogRepository libraryLogRepository)
        {
            _bookRepository = bookRepository;
            _libraryLogRepository = libraryLogRepository;
            

        }
     
       

        public async Task<Book>  AddBook(string title, int authorId)
        {
            if (authorId <= 0)
            {
                throw new ArgumentException("Author ID must be a positive integer greater than 0! ");
            }
            
             
            if(String.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title is mandatory!");
            }

            Book newBook = new Book() { Title = title, IsAvailable=true,AuthorId = authorId, CreatedOn = DateTime.Now };


            return await _bookRepository.AddBook(newBook);
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAllBooks();
        }
  

        public async Task<Book> GetBookById(int id)
        {
            return await _bookRepository.GetBookById(id);
        }

        public async Task<IEnumerable<Book>> GetBookByName(string title)
        {
            return await _bookRepository.GetBookByName(title);
        }

        //Update changes  to specific field  will be done at service layer


        public async Task<Book> UpdateBookTitle(int id, string newTitle)
        {
            //Do the validation
            
            var book =await  GetBookById(id);
            book.Title = newTitle;

            
            return  await _bookRepository.UpdateBook(book);
           
        }

        public async Task<Book> UpdateIsAvailable(int id,bool newStatus)
        {
            //
            var book = await _bookRepository.GetBookById(id);
            book.IsAvailable = newStatus;

            return await _bookRepository.UpdateBook(book);
                
        }

        public async Task RemoveBook(int id)
        {
            var book = await GetBookById(id);

            await _bookRepository.RemoveBook(book);
        }

        public async Task RemoveAllBooks()
        {
            //IEnumerable<Book> books
            //books
            await _bookRepository.RemoveAllBooks();
        }
    }
}
