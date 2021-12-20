using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AgilityWeb.Common.Exceptions;
using AgilityWeb.Domain.Base;
using AgilityWeb.Domain.Base.Validator;
using AgilityWeb.Infra.Base;
using AgilityWeb.Infra.Base.Factory;
using AgilityWeb.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace AgilityWeb.Infra.Services
{
    public class RepositoryService<TEnt, TDto> where TEnt : EntityBase, new()
        where TDto : EntityDtoBase, new()
    {
        private readonly AgilityContext _dataContext;
        private readonly DbSet<TEnt> _dbSet;
        private readonly FactoryConverterModelToDto<TEnt, TDto> _dtoConverter;
        private readonly AbstractValidator<TDto> _domainValidation;
        private RepositoryServiceSaveEvents SaveEvents { get; set; }
        private RepositoryServiceGetEvents GetEvents { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public RepositoryService(AbstractValidator<TDto> validatorDomain, DbSet<TEnt> dbSet,
            FactoryConverterModelToDto<TEnt, TDto> converterModelToDto, AgilityContext dataContext)
        {
            _dbSet = dbSet;
            _dataContext = dataContext;
            _dtoConverter = converterModelToDto;
            _domainValidation = validatorDomain;

            GetEvents = new RepositoryServiceGetEvents();
            SaveEvents = new RepositoryServiceSaveEvents();
        }

        /// <summary>
        /// Return registry for Id
        /// </summary>
        /// <returns></returns>
        public async Task<TDto> GetByIdAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
                return default;

            var query = _dbSet.Where(w => w.Id == id);

            query = GetEvents.BeforeExecuteQuery?.Invoke(query) ?? query;
            var entity = await query!.SingleOrDefaultAsync();

            return _dtoConverter.Parse(entity);
        }


        /// <summary>
        /// Insert registry
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        public async Task<TDto> InsertAsync(TDto obj, bool saveChanges)
        {
            _domainValidation.Validation(obj);

            if (!_domainValidation.IsValid) return null;

            var entity = new TEnt();
            SaveEvents?.BeforeParseDto?.Invoke(entity, obj, false);
            entity = _dtoConverter.Set(entity, obj);

            if (String.IsNullOrEmpty(entity.Id))
                entity.Id = Guid.NewGuid().ToString();

            SaveEvents?.AfterParseDto?.Invoke(entity, obj, false);

            entity = (await _dbSet.AddAsync(entity)).Entity;

            if (saveChanges)
                await _dataContext.SaveChangesAsync();

            return _dtoConverter.Parse(entity);
        }

        /// <summary>
        /// Alter registry
        /// </summary>
        public async Task<TDto> SaveAsync(TDto dto, bool saveChanges)
        {
            _domainValidation.Validation(dto);

            if (!_domainValidation.IsValid)
                return null;

            var query = _dbSet.Where(x => x.Id == dto.Id);

            query = GetEvents.BeforeExecuteQuery?.Invoke(query) ?? query;
            var entity = await query!.SingleOrDefaultAsync() ?? new TEnt();

            var update = !String.IsNullOrEmpty(entity.Id);
            SaveEvents?.BeforeParseDto.Invoke(entity, dto, update);
            _dtoConverter.Set(entity, dto);

            if (!update)
            {
                entity.Id = Guid.NewGuid().ToString();
                SaveEvents?.AfterParseDto?.Invoke(entity, dto, false);

                entity = (await _dbSet.AddAsync(entity)).Entity;
            }
            else
            {
                SaveEvents?.AfterParseDto?.Invoke(entity, dto, true);
            }

            if (saveChanges)
                await _dataContext.SaveChangesAsync();

            return _dtoConverter.Parse(entity);
        }

        /// <summary>
        /// Delete all registrys by id
        /// </summary>
        public async Task DeleteManyAsync(string[] ids, bool saveChanges)
        {
            if (ids == null || ids.Length == 0)
                return;

            var entites = await _dbSet.Where(w => ids.Contains(w.Id))
                .ToListAsync();

            _dataContext.RemoveRange(entites);

            if (saveChanges)
                await _dataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Remove all registrys
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAllAsync(bool saveChanges)
        {
            var entities = await _dbSet
                .ToListAsync();

            _dataContext.RemoveRange(entities);

            if (saveChanges)
                await _dataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Remove registry by id
        /// </summary>
        /// <returns></returns>
        public async Task DeleteAsync(string id, bool saveChanges)
        {
            if (String.IsNullOrEmpty(id))
                return;

            var entity = await _dbSet
                .Where(w => w.Id == id)
                .SingleOrDefaultAsync();

            if (entity == null)
                return;

            _dataContext.Remove(entity);

            if (saveChanges)
                await _dataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Return all registrys 
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<List<TDto>> GetAllAsync(string sort = null)
        {
            var query = _dbSet.AsQueryable();

            query = GetEvents.BeforeExecuteQuery?.Invoke(query) ?? query;

            var entites = await query.ToListAsync();

            return _dtoConverter.Parse(entites);
        }

        /// <summary>
        /// Save all registrys
        /// </summary>
        /// <param name="dtos"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        public async Task SaveManyAsync(List<TDto> dtos, bool saveChanges)
        {
            var insertMany = new List<TDto>();
            var updateMany = new List<TDto>();

            foreach (var dto in dtos)
            {
                _domainValidation.Validation(dto);

                if (String.IsNullOrEmpty(dto.Id))
                {
                    dto.Id = Guid.NewGuid().ToString();
                    insertMany.Add(dto);
                }
                else
                    updateMany.Add(dto);
            }

            foreach (var dto in insertMany)
            {
                var entity = new TEnt();
                SaveEvents?.BeforeParseDto?.Invoke(entity, dto, false);

                _dtoConverter.Set(entity, dto);
                SaveEvents?.AfterParseDto?.Invoke(entity, dto, false);

                await _dbSet.AddAsync(entity);
            }

            foreach (var dto in updateMany)
            {
                var query = _dbSet.Where(w => w.Id == dto.Id);

                query = GetEvents.BeforeExecuteQuery?.Invoke(query) ?? query;
                var entity = await query!.SingleOrDefaultAsync();

                var add = entity == null;
                entity ??= new TEnt();

                SaveEvents?.BeforeParseDto?.Invoke(entity, dto, true);
                _dtoConverter.Set(entity, dto);
                SaveEvents?.AfterParseDto?.Invoke(entity, dto, true);

                if (add)
                    await _dbSet.AddAsync(entity);
            }

            if (saveChanges)
                await _dataContext.SaveChangesAsync();
        }


        /// <summary>
        /// Alter registry
        /// </summary>
        /// <returns></returns>
        public async Task UpdateAsync(TDto dto, bool saveChanges)
        {
            _domainValidation.Validation(dto);

            if (String.IsNullOrEmpty(dto.Id))
                throw new HttpStatusCodeException("Request invalid",HttpStatusCode.BadRequest,"Indentity not informed");

            var query = _dbSet.Where(w => w.Id == dto.Id);

            query = GetEvents.BeforeExecuteQuery?.Invoke(query) ?? query;
            var entity = await query!.SingleOrDefaultAsync();

            if (entity == null)
                throw new HttpStatusCodeException("Not found",HttpStatusCode.NotFound,"Registry not found");

            SaveEvents?.BeforeParseDto?.Invoke(entity, dto, true);
            _dtoConverter.Set(entity, dto);
            SaveEvents?.AfterParseDto?.Invoke(entity, dto, true);

            if (saveChanges)
                await _dataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Save all registrys
        /// </summary>
        /// <param name="dtos"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        public async Task InsertManyAsync(List<TDto> dtos, bool saveChanges)
        {
            foreach (var dto in dtos)
            {
                _domainValidation.Validation(dto);

                if (String.IsNullOrEmpty(dto.Id))
                    dto.Id = Guid.NewGuid().ToString();

                var entity = new TEnt();
                SaveEvents?.BeforeParseDto?.Invoke(entity, dto, false);
                _dtoConverter.Set(entity, dto);
                SaveEvents?.AfterParseDto?.Invoke(entity, dto, false);

                await _dbSet.AddAsync(entity);
            }

            if (saveChanges)
                await _dataContext.SaveChangesAsync();
        }

        public class RepositoryServiceSaveEvents
        {
            public Action<TEnt, TDto, bool> BeforeParseDto { get; set; }
            public Action<TEnt, TDto, bool> AfterParseDto { get; set; }
        }

        public class RepositoryServiceGetEvents
        {
            public Func<IQueryable<TEnt>, IQueryable<TEnt>> BeforeExecuteQuery { get; set; }
        }
    }
}