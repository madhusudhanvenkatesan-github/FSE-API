using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FSE.API.DomainModel;
using FSE.API.DomainService;
using FSE.API.Messages;

namespace FSE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> userLogger;
        public UserController(IUserService userService, ILogger<UserController>logger)
        {
            this.userService = userService;
            this.userLogger = logger;
        }
        /// <summary>
        /// Search user based on Parameter. At least one parameter should be non empty
        /// </summary>
        /// <param name="empId">Employee Id</param>
        /// <param name="lName">Last name</param>
        /// <param name="fName">First Name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("SearchUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PMUser>>>SearchUser(string empId, string lName, string fName)
        {
            var searchCriteria = new UserSearchCriteria
            {
                EmployeeID = empId,
                LastName = lName,
                FirstName = fName
            };
            var results = await userService.GetUserByCriteria(searchCriteria);
            if (results.Count == 0)
                return NotFound("No User found");
            else
                return Ok(results);
        }
        /// <summary>
        /// Get a user based on employee id
        /// </summary>
        /// <param name="empId">Employee id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeeById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PMUser>> GetEmployeeById(string empId)
        {
            if (string.IsNullOrWhiteSpace(empId))
                return BadRequest("employee id is empty");
            var results = await userService.GetUserByEmployeeId(empId);
            if (results == default)
                return NotFound("User not found");
            return Ok(results);
        }
        /// <summary>
        /// Gets all the employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllEmployee")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PMUser>>> GetAllEmployee()
        {
            var results = await userService.GetAllUser();
        if (results.Count==0)
                return NotFound("User not found");
            return Ok(results);
        }
        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="userAdd">Details of the user to be added</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>>Post([FromBody]UserAddMsg userAdd)
        {
            if (userAdd == null)
            {
                ModelState.AddModelError("ParameterEmpty", "Input parameter are all empty");
                return BadRequest(ModelState);
            }
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(userAdd);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(userAdd, validationContext, validationResults))
            {
                ModelState.AddModelError("Validation Request", "Validation error");
                return BadRequest(ModelState);
            }
            var usr = await userService.GetUserByEmployeeId(userAdd.EmployeeId);
            if (usr!=default)
            {
                ModelState.AddModelError("EMployeeIdExist", "User already exist");
                return BadRequest(ModelState);
            }

            var result = await userService.Add(userAdd);
            if (result.Item1)
            {
                return Created($"api/Task/{result.Item2}", result.Item1);
            }
            return StatusCode(500, "Unable to create user");
        }
        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="userMod">Details of user that need to be edited</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Put([FromBody]UserModMsg userMod)
        {
            if (userMod == null)
            {
                ModelState.AddModelError("ParameterEmpty", "Input parameter are all empty");
                return BadRequest(ModelState);
            }
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(userMod);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(userMod, validationContext, validationResults))
            {
                return BadRequest(ModelState);
            }
            var result = await userService.Edit(userMod);
            if (result.Item1)
            {
                return Accepted();
            }
            else
                return StatusCode(500, "Unable to edit user");

        }
        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="empId">Employee id of the user that needs to be deleted</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Delete(string empId)
        {
            if (string.IsNullOrWhiteSpace(empId))
                return BadRequest("employee id is empty");
            var result = await userService.Delete(empId);
            if (result)
                return NoContent();
            else
                return StatusCode(500, "User could not be deleted");
        }

    }
    
}