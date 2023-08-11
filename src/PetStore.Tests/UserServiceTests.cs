using Allure.Commons;
using Allure.Xunit.Attributes;
using PetStore.Tests.DTOs;
using PetStore.Tests.Helpers;

namespace PetStore.Tests;

[AllureSuite("PetStore UserService Tests")]
[AllureSeverity(SeverityLevel.critical)]
//[AllureLink("PetStore Swagger", "")]
public class UserServiceTests 
{
    [AllureXunit(DisplayName = "Test that /user returns OK when correct user is posted")]
    public async Task CreateUserwithPost()
    {
        var user = TestDataHelpers.CreateNewUser();
        await helpers.SubmitRequest(Method.POST, "/user", user);
        helpers.AssumeResponseIsOk();
    }

    [AllureXunit(DisplayName = "Test that created with method POST /user user may be attrived with GET /user/{username}")]
    public async Task GetUserbyUserName()
    {
        var expectedUser = await helpers.SubmitNewUser();
        await helpers.SubmitRequest(Method.GET, $"/user/{expectedUser.UserName}");
        await helpers.AssumeResponseIsEqualTo(expectedUser);
    }

    [AllureXunit(DisplayName = "Test if user successfully updated by PUT /user/{usename}")]
    public async Task UpdateUser()
    {
        var expectedUser = await helpers.SubmitNewUser();
        TestDataHelpers.EditUserFieds(expectedUser);
        await helpers.SubmitRequest(Method.PUT, $"/user/{expectedUser.UserName}");
        await helpers.AssumeResponseIsEqualTo(expectedUser);
    }

    [AllureXunit(DisplayName = "Test if user successfully deleted by DELETE /user/{usename}")]
    public async Task DeleteUser()
    {
        var expectedUser = await helpers.SubmitNewUser();
        await helpers.SubmitRequest(Method.DELETE, $"/user/{expectedUser.UserName}");
        helpers.AssumeResponseIsOk();

        await helpers.SubmitRequest(Method.GET, $"/user/{expectedUser.UserName}");
        helpers.AssumeResponseIsNotFound();
    }

    [AllureXunit(DisplayName = "Test if users successfully added by /user/createWithArray")]
    public async Task CreateWithArrayCheck()
    {
        List<User> expectedUsers = await helpers.SubmitNewUsers(10);
        
        await helpers.SubmitRequest(Method.POST, "/user/createWithArray", expectedUsers);
        helpers.AssumeResponseIsOk();

        foreach (User expectedUser in expectedUsers)
        {
            await helpers.SubmitRequest(Method.GET, $"/user/{expectedUser.UserName}");
            await helpers.AssumeResponseIsEqualTo(expectedUser);
        }
    }

    [AllureXunit(DisplayName = "Test if users successfully added by /user/createWithList")]
    public async Task CreateWithListCheck()
    {
        List<User> expectedUsers = await helpers.SubmitNewUsers(10);

        await helpers.SubmitRequest(Method.POST, "/user/createWithList", expectedUsers);
        helpers.AssumeResponseIsOk();

        foreach (User expectedUser in expectedUsers)
        {
            await helpers.SubmitRequest(Method.GET, $"/user/{expectedUser.UserName}");
            await helpers.AssumeResponseIsEqualTo(expectedUser);
        }
    }

    [AllureXunit(DisplayName = "Test if user loginis successfukl by known user /user/createWithList")]
    public async Task LoginCheck()
    {
        var user = await helpers.SubmitNewUser();

        await helpers.SubmitRequest(Method.GET, $"/user/login?username={user.UserName}&password={user.Password}");
        await helpers.AssumeLoginResponseIsOk();
    }

    [AllureXunit(DisplayName = "Test if user login is not successfull with wrong credentials /user/createWithList")]
    public async Task LoginFalseCheck()
    {
        var login = Guid.NewGuid();
        var pswd = Guid.NewGuid();

        await helpers.SubmitRequest(Method.GET, $"/user/login?username={login}&password={pswd}");
        helpers.AssumeLoginResponseIsFailed();
    }

    [AllureXunit(DisplayName = "Test if user login is not successfull with wrong credentials /user/createWithList")]
    public async Task LogoutCheck()
    {
        await helpers.SubmitRequest(Method.GET, $"/user/logout");
        helpers.AssumeResponseIsOk();
    }

    private readonly TestApiHelpers helpers = new ();
}