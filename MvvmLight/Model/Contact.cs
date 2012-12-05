using System;
using System.Collections.Concurrent;
using System.Linq;
using GalaSoft.MvvmLight;

namespace MvvmLight.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }

    public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ConcurrentDictionary<Guid, TEntity> _concurrentDictionary = new ConcurrentDictionary<Guid, TEntity>();

        /// <summary>
        /// Add new entity to repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Added entity if operation was successful. Null if operation failed.</returns>
        public TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                //we dont want to store nulls in our collection
                throw new ArgumentNullException("entity");
            }

            if (entity.Id == Guid.Empty)
            {
                //we assume no Guid collisions will occur
                entity.Id = Guid.NewGuid();
            }

            if (_concurrentDictionary.ContainsKey(entity.Id))
            {
                return null;
            }

            bool result = _concurrentDictionary.TryAdd(entity.Id, entity);

            if (result == false)
            {
                return null;
            }
            return entity;
        }

        public TEntity Delete(Guid id)
        {
            TEntity removed;
            if (!_concurrentDictionary.ContainsKey(id))
            {
                return null;
            }
            bool result = _concurrentDictionary.TryRemove(id, out removed);
            if (!result)
            {
                return null;
            }
            return removed;
        }

        public TEntity Get(Guid id)
        {
            if (!_concurrentDictionary.ContainsKey(id))
            {
                return null;
            }
            TEntity entity;
            bool result = _concurrentDictionary.TryGetValue(id, out entity);
            if (!result)
            {
                return null;
            }
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (!_concurrentDictionary.ContainsKey(entity.Id))
            {
                return null;
            }
            _concurrentDictionary[entity.Id] = entity;
            return entity;
        }

        public IQueryable<TEntity> Items
        {
            get { return _concurrentDictionary.Values.AsQueryable(); }
        }
    }
    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity Add(TEntity entity);
        TEntity Delete(Guid id);
        TEntity Get(Guid id);
        TEntity Update(TEntity entity);
        IQueryable<TEntity> Items { get; }
    }

    public class Contact : ObservableObject, IEntity
    {
        public Guid Id { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    RaisePropertyChanged(() => Name);
                    //or alternatively
                    //RaisePropertyChanged("Name");
                }
            }
        }

        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string TwitterHandle { get; set; }
    }
}