using API.APPLICATION.Commands.Customer;
using API.APPLICATION.Parameters.Customer;
using API.APPLICATION.Parameters.User;
using API.APPLICATION.Queries.Customer;
using API.APPLICATION.ViewModels.Customer;
using API.APPLICATION.ViewModels.User;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Attributes;
using BaseCommon.Common.MethodResult;
using BaseCommon.Common.Response;
using BaseCommon.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private const string GetList = nameof(GetList);
        private const string GetById = nameof(GetById);

        private readonly ICustomerServices _customerServices;

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CustomerController(IMapper mapper, IMediator mediator, ICustomerServices customerServices)
        {
            _mapper = mapper;
            _mediator = mediator;
            _customerServices = customerServices;
        }

        /// <summary>
        /// GetListCustomer - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(GetList)]
        [SQLInjectionCheckOperation]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        [AllowAnonymous]
        public async Task<ActionResult> GetDanhSachCustomerAsync(CustomerRequestViewModel request)
        {
            var methodResult = new MethodResult<PagingItems<CustomerResponseViewModel>>();
            var userFilterParam = _mapper.Map<CustomerFilterParam>(request);

            var queryResult = await _customerServices.GetCustomerPagingAsync(userFilterParam).ConfigureAwait(false);
            methodResult.Result = new PagingItems<CustomerResponseViewModel>
            {
                PagingInfo = queryResult.PagingInfo,
                Items = _mapper.Map<IEnumerable<CustomerResponseViewModel>>(queryResult.Items)
            };

            return Ok(methodResult);
        }
        /// <summary>
        /// Get List of GetCustomerById.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(GetById)]
        [ProducesResponseType(typeof(MethodResult<CustomerResponseViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [SQLInjectionCheckOperation]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        [AllowAnonymous]
        public async Task<IActionResult> GetCustomerByIdAsync(CustomerByIdViewModel request)
        {
            var methodResult = new MethodResult<UserResponseByIdModel>();
            var userFilterParam = _mapper.Map<CustomerByIdParam>(request);
            var query = await _customerServices.GetInfoUserByIdAsync(userFilterParam).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UserResponseByIdModel>(query);
            return Ok(methodResult);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<CreateCustomerCommand>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCustomerAsync(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Delete Customer - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(MethodResult<DeleteCustomerCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCustomerAsync(DeleteCustomerCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }

        /// <summary>
        /// Update Customer- (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<UpdateCustomerCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        //[AuthorizeGroupCheckOperation(EAuthorizeType.MusHavePermission)]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateCustomerAsync(UpdateCustomerCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
    }
}