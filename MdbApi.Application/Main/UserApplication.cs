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
    public class UserApplication : IUserApplication
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<UserApplication> _logger;
        public UserApplication(IUserRepository userRepository, IMapper mapper, IAppLogger<UserApplication> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<UserModel>> Add(UserModel item)
        {
            var response = new Response<UserModel>();
            try
            {
                var userToInst = _mapper.Map<User>(item);
                var user = await _userRepository.Add(userToInst);
                response.Data = _mapper.Map<UserModel>(user);
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

        public async Task<Response<UserModel>> Get(string id)
        {
            var response = new Response<UserModel>();
            try
            {
                var user = await _userRepository.Get(id);
                var userList = _mapper.Map<UserModel>(user);
                response.Data = userList;

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

        public async Task<Response<IEnumerable<UserModel>>> GetAll()
        {
            var response = new Response<IEnumerable<UserModel>>();
            try
            {
                var users = await _userRepository.GetAll();
                var userList = _mapper.Map<IEnumerable<UserModel>>(users);
                response.Data = userList;

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
                var user = await _userRepository.Remove(id);
                response.Data = user;
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

        public Task<Response<bool>> Update(string id, string body)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<bool>> UpdateDocument(UserModel item)
        {
            var response = new Response<bool>();
            try
            {
                var userToUpd = _mapper.Map<User>(item);
                var user = await _userRepository.UpdateDocument(userToUpd);
                response.Data = user;
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