using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PartyCli.Core.Interfaces;
using PartyCli.Infrastructure.Exeptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PartyCli.Infrastructure.Repositories
{
    public class GenericFileRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly string _fileRepositoryPath;
        private readonly string _fileName = $"{typeof(TEntity).Name}.json";
        private readonly string _filePath;
        private readonly ILogger _logger;

        public GenericFileRepository( ILogger logger, IConfiguration configuration)
        {
            _fileRepositoryPath = configuration["FilesRepositoryPath"] ?? throw new PresentableConfigExeption("FilesRepositoryPath");
            if (!Directory.Exists(_fileRepositoryPath))
                Directory.CreateDirectory(_fileRepositoryPath);

            _logger = logger;
            _filePath = $"{_fileRepositoryPath}/{_fileName}";
        }

        public virtual IEnumerable<TEntity> GetAll()
        {            
            if (File.Exists(_filePath))
            {
                var jsonString = File.ReadAllText(_filePath);

                return TryParseJson(jsonString);
            }

            _logger.Debug($"Could not find file \"{_fileName}\".");

            return new List<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            // Overwriting file content not to get complicated in example solution

            var list = new List<TEntity>();
            list.Add(entity);

            var jsonString = JsonConvert.SerializeObject(list);

            File.WriteAllText(_filePath, jsonString);

            _logger.Debug($"Changes written to file \"{_fileName}\".");
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            // Overwriting file content not to get complicated in example solution

            var jsonString = JsonConvert.SerializeObject(entities);

            File.WriteAllText(_filePath, jsonString);

            _logger.Debug($"Changes written to file \"{_fileName}\".");
        }


        protected IEnumerable<TEntity> TryParseJson(string jsonString)
        {
            try
            {
               return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonString);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                throw new PresentableExeption($"Data file is corupted. Could not parse data to json.");
            }
        }
    }
}
