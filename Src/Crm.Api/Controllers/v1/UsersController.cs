using Crm.Application.Authorization;
using Crm.Application.Features.Users.CreateUser;
using Crm.Application.Features.Users.GetUserById;
using Crm.Application.Features.Users.GetUsers;
using Crm.Application.Features.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers.v1;

public class UsersController(ISender sender) : BaseController
{
    //[HttpPost("create")]
    //[Authorize(Policy = Permissions.User.Create)]
    //public async Task<ActionResult<CreateUserResponse>> Create(CreateUserCommand command, CancellationToken cancellationToken)
    //{
    //    var result = await sender.Send(command, cancellationToken);

    //        return Created($"/api/v1/users/{result.Id}",ApiResponseFactory.Success( result,StatusCodes.Status201Created));
    //}

    //[HttpGet("get-user{id:long}")]
    //[Authorize(Policy = Permissions.User.View)]
    //public async Task<ActionResult<GetUserByIdResponse>> GetById(long id, CancellationToken cancellationToken)
    //{
    //    var result = await sender.Send(new GetUserByIdQuery(id), cancellationToken);
    //    return Ok(ApiResponseFactory.Success(result));
    //}

    //[HttpGet("get-userd")]
    //[Authorize(Policy = Permissions.User.View)]
    //public async Task<ActionResult<GetUsersResponse>> GetUsers([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
    //{
    //    var result = await sender.Send(query, cancellationToken);
    //    return Ok(ApiResponseFactory.Success(result));
    //}

    //[HttpPut("update")]
    //[Authorize(Policy = Permissions.User.Update)]
    //public async Task<ActionResult<UpdateUserResponse>> Update(UpdateUserCommand command, CancellationToken cancellationToken)
    //{
    //    var result = await sender.Send(command, cancellationToken);
    //    return Ok(ApiResponseFactory.Success(result));
    //}
}

