using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENTech.Store.Infrastructure.Repositories;
using ENTech.Store.Services.UploadModule.Requests;
using System.IO;
using ENTech.Store.Services.UploadModule.Commands;
using ENTech.Store.Services.ProductModule.Commands;
using ENTech.Store.Services.ProductModule.Requests;
using ENTech.Store.Services.SharedModule.Dtos;
using System.Collections.Generic;

namespace ENTech.Store.Services.Tests
{
    public class MemoryRepository : IRepository
    {

        public Dictionary<string, Dictionary<int, object>> Storage = new Dictionary<string, Dictionary<int, object>>();
   
        public Dictionary<int, object> GetTable<T>()
        {
            var s = Storage;
            var key = typeof(T).ToString();
            if (!s.ContainsKey(key))
                s.Add(key, new Dictionary<int, object>());

            return s[key];
        }

        public void Create<T>(int id, T data)
        {
            GetTable<T>().Add(id, data);
        }

        public void Update<T>(int id, T data)
        {
            GetTable<T>()[id] = data;
        }

        public void Delete<T>(int id)
        {
            GetTable<T>().Remove(id);
        }

        public bool Exists<T>(int id)
        {
            return GetTable<T>().ContainsKey(id);
        }

        public T GetById<T>(int id)
        {
            return (T)GetTable<T>()[id];
        }
 
    }


    [TestClass]
    public class UploadTest
    {
        [TestMethod]
        public void Upload_file_before_save()
        {
            IRepository repository = new MemoryRepository();

            var productCreatedRespose = new ProductCreateCommandBase(repository).Execute(new ProductCreateRequest
            {
                Product = new ProductModule.Dtos.ProductCreateDto
                {
                    Name = "testName"
                }
            });

            var productId = productCreatedRespose.ProductId;

            var uploadCreatedResponse = new UploadCreateCommandBase(repository).Execute(new UploadCreateRequest
            {
                Role = "TestFile",
            });

            new UploadUpdateCommandBase(repository).Execute(new UploadUpdateRequest
            {
                Id = uploadCreatedResponse.Id,
                Stream = new MemoryStream(),
                Extension = ".txt"
            });

            new UploadAttachCommandBase(repository).Execute(new UploadAttachRequest
            {
                AttachingEntityType = "Product",
                AttachingEntityFieldName = "PhotoUrl",
                AttachingEntityId = productId,
                UploadId = uploadCreatedResponse.Id
            });

            var productResponse = new ProductGetByIdCommandBase(repository).Execute(new ProductGetByIdRequest
            {
                Id = productId
            });

            Assert.IsNotNull(((UrlDto)productResponse.Item.Photo).Url);
        }


        [TestMethod]
        public void Upload_file_after_save()
        {
            IRepository repository = new MemoryRepository();

            var productCreatedRespose = new ProductCreateCommandBase(repository).Execute(new ProductCreateRequest
            {
                Product = new ProductModule.Dtos.ProductCreateDto
                {
                    Name = "testName"
                }
            });

            var productId = productCreatedRespose.ProductId;

            var uploadCreatedResponse = new UploadCreateCommandBase(repository).Execute(new UploadCreateRequest
            {
                Role = "TestFile",
            });

            new UploadAttachCommandBase(repository).Execute(new UploadAttachRequest
            {
                AttachingEntityType = "Product",
                AttachingEntityFieldName = "PhotoUrl",
                AttachingEntityId = productId,
                UploadId = uploadCreatedResponse.Id
            });


            new UploadUpdateCommandBase(repository).Execute(new UploadUpdateRequest
            {
                Id = uploadCreatedResponse.Id,
                Stream = new MemoryStream(),
                Extension = ".txt"
            });

            

            var productResponse = new ProductGetByIdCommandBase(repository).Execute(new ProductGetByIdRequest
            {
                Id = productId
            });

            Assert.IsNotNull(((UrlDto)productResponse.Item.Photo).Url);
        }
    }
}
