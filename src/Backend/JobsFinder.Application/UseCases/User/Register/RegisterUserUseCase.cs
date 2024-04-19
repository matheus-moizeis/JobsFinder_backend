using AutoMapper;
using JobsFinder.Application.Services.Cryptography;
using JobsFinder.Communication.Requests;
using JobsFinder.Communication.Responses;
using JobsFinder.Domain.Repositories;
using JobsFinder.Domain.Repositories.User;
using JobsFinder.Exceptions;
using JobsFinder.Exceptions.ExceptionsBase;

namespace JobsFinder.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly PasswordEncripter _passwordEncripter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterUserUseCase(
        IUserReadOnlyRepository userReadOnlyRepository, 
        IUserWriteOnlyRepository userWriteOnlyRepository, 
        IMapper mapper, 
        PasswordEncripter passwordEncripter,
        IUnitOfWork unitOfWork
        )
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegistredUserJson> Execute(RequestRegisterUserJson request)
    {

        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);

        user.Password = _passwordEncripter.Encript(request.Password);

        await _userWriteOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegistredUserJson
        {
            Name = request.Name,
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(
                string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
        }

        if (result.IsValid == false)
        {
            var errorMessage = result.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessage);
        }
    }
}
