using KSFramework;
using KSFramework.Pagination;
using KSFramework.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Website.Api.ViewModels.ContactUs;
using Website.Application.ContactUsMessages.Commands.MarkMessageAsChecked;
using Website.Application.ContactUsMessages.Commands.MarkMessageAsUnchecked;
using Website.Application.ContactUsMessages.Commands.RemoveMessage;
using Website.Application.ContactUsMessages.Queries.GetMessageById;
using Website.Application.ContactUsMessages.Queries.GetPagedMessages;
using Website.Common.WebFrameworks.Routing;
using Website.Services.AuthServices;

namespace Website.Api.Controllers;

public class SecureContactUsController : SecureBaseController
{
    private readonly IMediator _mediator;
    public SecureContactUsController(IAuthService authServices, IMediator mediator)
        : base(authServices)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [HttpGet("/getclaims")]
    public async Task<IActionResult> GetClaims()
    {
        return Ok(UserClaims);
    }
    
    // [Authorize(Policy = "ClientIdPolicy")] // This policy has been set in the Extension.cs of Website.Common project, based on this policy
    // Client_Id of the request should be websiteadmin
    // [Authorize]
    [HttpGet(Routes.ContactUs.Get.GetAll)]
    [ProducesResponseType(typeof(ResultMessage<ContactUsListItemViewModel>), 200)]
    public async Task<IActionResult> GetAll(int? pageNumber = 1, int? pageSize = 100, string? searchTerm = "", string? orderByPropertyName = "Id", bool desc = false)
    {
        GetPagedMessagesQuery getPagedItemsQuery = new(pageNumber, pageSize, searchTerm, orderByPropertyName, desc);
        var result = await _mediator.Send(getPagedItemsQuery);
        var showPagination = (pageSize.Value <= result.TotalItems);
        return CustomPagedOk(result.Select(x => new ContactUsListItemViewModel(x)), result.PageIndex, result.TotalPages, result.TotalItems, showPagination);
    }
    
    [Authorize]
    [HttpGet(Routes.ContactUs.Get.GetById)]
    [ProducesResponseType(typeof(OkResponse<ContactUsMessageViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(Guid id)
    {
        GetMessageByIdQuery getByIdQuery = new GetMessageByIdQuery(id);
        var messageDto = await _mediator.Send(getByIdQuery);
        var messageViewModel = new ContactUsMessageViewModel(messageDto);
        
        return CustomOk<ContactUsMessageViewModel>(messageViewModel);
    }

    [HttpPut(Routes.ContactUs.Edit.Activate)]
    [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Activate(Guid id)
    {
        MarkMessageAsCheckedCommand command = new(id);
        var commandResult = await _mediator.Send(command);
        return Ok(commandResult);
    }

    [HttpPut(Routes.ContactUs.Edit.Deactive)]
    [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        MarkMessageAsUncheckedCommand command = new(id);
        var commandResult = await _mediator.Send(command);
        return Ok(commandResult);
    }

    [HttpPut(Routes.ContactUs.Edit.ChangeStatus)]
    [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeStatus(Guid id)
    {
        GetMessageByIdQuery getByIdQuery = new GetMessageByIdQuery(id);
        var messageDto = await _mediator.Send(getByIdQuery);
        if (messageDto.IsChecked)
        {
            MarkMessageAsUncheckedCommand command = new(id);
            var commandResult = await _mediator.Send(command);
            return Ok(commandResult);
        }
        else
        {
            MarkMessageAsCheckedCommand command = new(id);
            var commandResult = await _mediator.Send(command);
            return Ok(commandResult);
        }
    }

    [HttpDelete(Routes.ContactUs.Delete.Remove)]
    [ProducesResponseType(typeof(BaseResponse<Guid>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var removeContactUsCommand = new RemoveMessageCommand(id);
        var removeResult = await _mediator.Send(removeContactUsCommand);
        return Ok(removeResult);
    }
}