﻿using MyComicList.Application.Commands.Comics;
using MyComicList.Application.DataTransfer.Users;
using MyComicList.Application.Requests;
using MyComicList.Application.Responses;

namespace MyComicList.Application.Commands.Users
{
    public interface IGetUsers : ICommand<UserRequest, PagedResponse<UserGetDTO>>
    {
        
    }
}