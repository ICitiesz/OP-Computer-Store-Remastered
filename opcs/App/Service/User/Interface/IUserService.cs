using LanguageExt;
using opcs.App.Data.Dto.Request;
using opcs.App.Data.Dto.Response;

namespace opcs.App.Service.User.Interface;

public interface IUserService
{
    Option<AuthenticationResponseDto> RegisterUser(UserRegisterRequestDto requestDto);

    Option<AuthenticationResponseDto> LoginUser(UserLoginRequestDto requestDto);
}