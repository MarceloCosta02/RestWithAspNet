﻿using System.Collections.Generic;
using WebApi_Persons.Model;
using System.Threading;
using WebApi_Persons.Model.Context;
using System;
using System.Linq;
using WebApi_Persons.Repository;
using WebApi_Persons.Repository.Generic;
using WebApi_Persons.Data.Converters;
using WebApi_Persons.Data.VO;
using Tapioca.HATEOAS.Utils;

namespace WebApi_Persons.Business.Implementattions
{
    public class PersonBusiness : IPersonBusiness
    {

        private IPersonRepository _repository;

        private readonly PersonConverter _converter;

        public PersonBusiness(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.ParseList(_repository.FindByName(firstName, lastName));
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }

        public PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            page = page > 0 ? page - 1 : 0;

            string query = @"select * from Persons p where 1 = 1 ";

            // Se o nome não estiver nulo nem vazio, contatenar com essa query com o nome
            if (!string.IsNullOrEmpty(name)) query = query + $" and p.firstName like '%{name}%'";

            // Faz a ordenação
            query = query + $" order by p.firstName {sortDirection} limit {pageSize} offset {page}";

            string countQuery = @"select count(*) from Persons p where 1 = 1 ";

            // Se o nome não estiver nulo nem vazio, contatenar com essa query com o nome
            if (!string.IsNullOrEmpty(name)) countQuery = countQuery + $" and p.firstName like '%{name}%'";

            var persons = _repository.FindWithPagedSearch(query);

            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDTO<PersonVO>
            {
                CurrentPage = page + 1,
                List = _converter.ParseList(persons),
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalResults
            };
        }
    }
}
