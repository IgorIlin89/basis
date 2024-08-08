using ApiUser.Database.Interfaces;
using ApiUser.Domain;
using ApiUser.Domain.Exceptions;


namespace ApiUser.Database;

internal class UserRepository : IUserRepository
{
    private readonly ApiUserContext _context;

    public UserRepository(ApiUserContext context)
    {
        _context = context;
    }

    public User AddUser(User user)
    {
        var existingUser = _context.User.FirstOrDefault(o => o.EMail == user.EMail);

        if (existingUser is not null)
        {
            throw new UserExistsException($"A user with the e-mail {user.EMail} is allready registered");
        }

        var response = _context.User.Add(new User
        {
            EMail = user.EMail,
            GivenName = user.GivenName,
            Surname = user.Surname,
            Age = user.Age,
            Country = user.Country,
            City = user.City,
            Street = user.Street,
            HouseNumber = user.HouseNumber,
            PostalCode = user.PostalCode,
            Password = user.Password
        });

        return response.Entity;
    }

    public User ChangePassword(int id, string password)
    {
        var user = _context.User.FirstOrDefault(o => o.Id == id);

        if (user is null)
        {
            throw new NotFoundException($"Password not changed. User with id '{id}' could not be found");
        }

        user.Password = password;

        return user;
    }

    public void Delete(int id)
    {
        var user = _context.User.FirstOrDefault(o => o.Id == id);

        if (user is not null)
        {
            _context.Remove(user);
        }
    }

    public User? GetUserByEMail(string email)
    {
        var user = _context.User.FirstOrDefault(o => o.EMail == email);

        if (user is null)
        {
            throw new NotFoundException($"User with the email '{email}' does not exist");
        }

        return user;
    }

    public User? GetUserById(int id)
    {
        var user = _context.User.FirstOrDefault(o => o.Id == id);

        if (user is null)
        {
            throw new NotFoundException($"User with the id '{id}' does not exist");
        }

        return user;
    }

    public List<User> GetUserList()
    {
        return _context.User.ToList<User>();
    }

    public User Update(User user)
    {
        var userToEdit = _context.User.FirstOrDefault(o => o.Id == user.Id);

        if (userToEdit is null)
        {
            throw new NotFoundException($"User not updated. Could not find user with id '{user.Id}'");
        }

        userToEdit.EMail = user.EMail;
        userToEdit.GivenName = user.GivenName;
        userToEdit.Surname = user.Surname;
        userToEdit.Age = user.Age;
        userToEdit.Country = user.Country;
        userToEdit.City = user.City;
        userToEdit.Street = user.Street;
        userToEdit.HouseNumber = user.HouseNumber;
        userToEdit.PostalCode = user.PostalCode;
        userToEdit.Password = user.Password;

        return userToEdit;
    }
}
