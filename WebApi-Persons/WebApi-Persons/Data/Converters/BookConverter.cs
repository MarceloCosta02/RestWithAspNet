﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Persons.Data.Converter;
using WebApi_Persons.Data.VO;
using WebApi_Persons.Model;

namespace WebApi_Persons.Data.Converters
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if (origin == null)
                return new Book();

            return new Book
            {
                Id = origin.Id,
                Title = origin.Title,
                Author = origin.Author,
                Price = origin.Price,
                LaunchDate = origin.LaunchDate,
            };
        }

        public BookVO Parse(Book origin)
        {
            if (origin == null)
                return new BookVO();

            return new BookVO
            {
                Id = origin.Id,
                Title = origin.Title,
                Author = origin.Author,
                Price = origin.Price,
                LaunchDate = origin.LaunchDate,
            };
        }

        public List<Book> ParseList(List<BookVO> origin)
        {
            if (origin == null)
                return new List<Book>();

            return origin.Select(item => Parse(item)).ToList();
        }

        public List<BookVO> ParseList(List<Book> origin)
        {
            if (origin == null)
                return new List<BookVO>();

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
