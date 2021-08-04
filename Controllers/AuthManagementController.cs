using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarparkAPIApp.Configuration;
using CarparkAPIApp.Models;
using CarparkAPIApp.Models.DTOs.Requests;
using CarparkAPIApp.Models.DTOs.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarparkAPIApp.Controllers
{
    [Route("api/[controller]")] //api/authMangement

    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthManagementController(
            UserManager<UserModel> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                             "Email already in use"
                        },
                        Success = false
                    });
                };

                var newUser = new UserModel()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ContactNumber = user.ContactNumber,
                    UserName = user.Email
                };

                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(newUser);
                    return Ok(new RegistrationResponse()
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                        Success = false
                    });
                }
            };

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                    "Invalid payload"
                },
                Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                            "Invalid login request"
                        },
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if (!isCorrect)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                            "Invalid Username or password"
                        },
                        Success = false
                    });
                }

                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(new RegistrationResponse()
                {
                    Success = true,
                    Token = jwtToken
                });
            }

            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                            "Invalid Payload"
                        },
                Success = false
            });
        }

        [HttpGet]
        [Route("GetDetails")]
        public async Task<IActionResult> GetDetails()
        {

            var email = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var currentUser = await _userManager.FindByEmailAsync(email);
            
            if (currentUser == null)
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errors = new List<string>() {
                            "Something went wrong"
                        },
                    Success = false
                });
            }

            return Ok(currentUser);
        }

        private string GenerateJwtToken(UserModel user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;

        }
    }

}
