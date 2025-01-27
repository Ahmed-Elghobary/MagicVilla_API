﻿using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_VillaAPI.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private string secretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration, UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public bool IsUniqueUser(string username)
        {
            var user=_db.applicationUsers.FirstOrDefault(x=>x.UserName==username);
            if (user == null)
            {
                return true;
            }
                return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user=_db.applicationUsers.
                FirstOrDefault(u=>u.UserName.ToLower()==loginRequestDTO.UserName.ToLower());

            bool IsValid= await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
            if(user == null || IsValid==false)
            {
                return new LoginResponseDTO()
                {
                    Token="",
                    User=null
                };
            }
            // generate jwt token
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler= new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject= new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,roles.FirstOrDefault())


                }),
                Expires=DateTime.UtcNow.AddDays(1),
                SigningCredentials= new(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDTO>(user),
                Role=roles.FirstOrDefault()
            };
            return loginResponseDTO;
        }

        public async Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            ApplicationUser user = new ()
            {
                UserName= registerationRequestDTO.UserName,
                Name= registerationRequestDTO.Name,
               Email=registerationRequestDTO.UserName,
               NormalizedEmail=registerationRequestDTO.UserName.ToUpper(),
             

            };


            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequestDTO.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("customer"));
                    }
                    await _userManager.AddToRoleAsync(user, "Admin");
                    var UserToReturn=_db.applicationUsers
                        .FirstOrDefault(u=>u.UserName== registerationRequestDTO.UserName);
                    return _mapper.Map<UserDTO>(UserToReturn);
                }
            }
            catch (Exception ex)
            {

            }
            
            return new UserDTO();
        }
    }
}
