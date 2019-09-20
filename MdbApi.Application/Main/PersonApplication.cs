using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MdbApi.Application.Interface;
using MdbApi.Application.Models;
using MdbApi.Data.Interface;
using MdbApi.Domain.Entities;

namespace MdbApi.Application.Main
{
    public class PersonApplication : IPersonApplication
    {
         private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<PersonApplication> _logger;
        public PersonApplication(IPersonRepository personRepository, IMapper mapper, IAppLogger<PersonApplication> logger)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<PersonModelAdd>> Add(PersonModelAdd item)
        {
            var response = new Response<PersonModelAdd>();
            try
            {
                var itemToInst = _mapper.Map<Person>(item);
                var entity = await _personRepository.Add(itemToInst);
                response.Data = _mapper.Map<PersonModelAdd>(entity);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<Response<PersonModel>> Get(string id)
        {
            var response = new Response<PersonModel>();
            try
            {
                var entity = await _personRepository.Get(id);
                var entityList = _mapper.Map<PersonModel>(entity);
                response.Data = entityList;

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "No existe el registro";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message + "--> InnerException.Message-->" + (e.InnerException != null ? e.InnerException.Message : "");
                _logger.LogError(response.Message);
            }
            return response;
        }

        public async Task<Response<IEnumerable<PersonModel>>> GetAll()
        {
            var response = new Response<IEnumerable<PersonModel>>();
            try
            {
                var entity = await _personRepository.GetAll();
                var entityList = _mapper.Map<IEnumerable<PersonModel>>(entity);
                response.Data = entityList;

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message + "--> InnerException.Message-->" + (e.InnerException != null ? e.InnerException.Message : "");
                _logger.LogError(response.Message);
            }
            return response;
        }

        public async Task<Response<bool>> Remove(string id)
        {
            var response = new Response<bool>();
            try
            {
                var entity = await _personRepository.Remove(id);
                response.Data = entity;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Se elimino el registro!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public  Task<Response<bool>> Update(string id, string body)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<bool>> UpdateDocument(PersonModel item)
        {
             var response = new Response<bool>();
            try
            {
                var entityToUpd = _mapper.Map<Person>(item);
                var entity = await _personRepository.UpdateDocument(entityToUpd);
                response.Data = entity;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Se actualizó el registro!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }
    }
}