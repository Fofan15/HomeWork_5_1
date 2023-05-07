using CodeHomeWork_5_1.Services.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHomeWork_5_1
{
    public class App
    {
        private readonly IUserService _userService;
        private readonly IResourceService _resourceService;
        private readonly IRegisterService _registerService;
        private readonly ILogingService _loginService;

        public App(IUserService userService, IResourceService resourceService, IRegisterService registerService, ILogingService loginService)
        {
            _userService = userService;
            _resourceService = resourceService;
            _registerService = registerService;
            _loginService = loginService;
        }

        public async Task Start()
        {
            var userList = await _userService.GetUsersList();
            var singleUser = await _userService.GetUserById(2);
            var userNotFound = await _userService.GetUserById(23);
            var resourceList = await _resourceService.GetResourceList();
            var singleResource = await _resourceService.GetResourceById(2);
            var resourceNotFound = await _resourceService.GetResourceById(23);
            var userCreate = await _userService.CreateUser("morpheus", "leader");
            var userUpdate = await _userService.UpdateUser(2, "morpheus", "zion resident");
            var userUpdatePatch = await _userService.UpdateUserPatch(2, "morpheus", "zion resident");
            var userDelete = await _userService.DeleteUser(2);
            var register = await _registerService.RegisterUser("eve.holt@reqres.in", "pistol");
            var registerError = await _registerService.RegisterUser("sydney@fife", null);
            var login = await _loginService.LoginUser("eve.holt@reqres.in", "cityslicka");
            var loginError = await _loginService.LoginUser("peter@klaven", null);

            Console.WriteLine(JsonConvert.SerializeObject(userList));
            Console.WriteLine(JsonConvert.SerializeObject(singleUser));
            Console.WriteLine(JsonConvert.SerializeObject(userNotFound));
            Console.WriteLine(JsonConvert.SerializeObject(resourceList));
            Console.WriteLine(JsonConvert.SerializeObject(singleResource));
            Console.WriteLine(JsonConvert.SerializeObject(resourceNotFound));
            Console.WriteLine(JsonConvert.SerializeObject(userCreate));
            Console.WriteLine(JsonConvert.SerializeObject(userUpdate));
            Console.WriteLine(JsonConvert.SerializeObject(userUpdatePatch));
            Console.WriteLine(JsonConvert.SerializeObject(userDelete));
            Console.WriteLine(JsonConvert.SerializeObject(register));
            Console.WriteLine(JsonConvert.SerializeObject(registerError));
            Console.WriteLine(JsonConvert.SerializeObject(login));
            Console.WriteLine(JsonConvert.SerializeObject(loginError));
        }
    }
}
