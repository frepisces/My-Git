using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AccountMvcApplication;
using AccountMvcApplication.Controllers;
using Moq;
using Common.Helper;
using AccountMvcApplication.Models;
using Common;
using Newtonsoft.Json;

namespace AccountMvcApplication.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IWebApiHelper> _webApiHelper = new Mock<IWebApiHelper>();

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_webApiHelper.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

        }

        [TestMethod]
        public void Manage()
        {
            // Arrange
            HomeController controller = new HomeController(_webApiHelper.Object);
            _webApiHelper.Setup(a => a.getData(It.IsAny<string>(), It.IsAny<string>())).Returns(() => ObjectParserHelper.ObjectToJson(new List<LoginModel>() { new  LoginModel()}));

            // Act
            ViewResult result = controller.Manage() as ViewResult;
            //assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void EditAccount() {

            int myTestValue1 = 1;
            bool myTestValue2 = true;

            string jsonstring = @"{""UserID"":1,""UserName"":""yukon"",""PassWord"":""123456"",""IsConfirmed"":true}";
            var model = new EditAccountModel() { UserId = 1, UserName = "yukon", IsConfirmed = true };

            _webApiHelper.Setup(a => a.getData(It.IsAny<string>(), It.IsAny<string>())).Returns(jsonstring);

            _webApiHelper.Setup(a => a.PostData(It.IsAny<Method>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(jsonstring);
            // Arrange
            HomeController controller = new HomeController(_webApiHelper.Object);

            ViewResult result = controller.EditAccount(myTestValue1, myTestValue2) as ViewResult;

        }

        [TestMethod]
         public void DeleteAccount(){

             int myTestValue = 1;

             var model = new EditAccountModel() { UserId = 1, UserName = "yukon", IsConfirmed = true };
             string jsonResponse = ObjectParserHelper.ObjectToJson(model);

             _webApiHelper.Setup(a => a.PostData(It.IsAny<Method>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(jsonResponse);

             // Arrange
             HomeController controller = new HomeController(_webApiHelper.Object);

             ViewResult result = controller.DeleteAccount(myTestValue) as ViewResult;
        } 

        [TestMethod]
         public void ShowAccountDetail() {

             int myTestValue = 1;

             // Arrange
             HomeController controller = new HomeController(_webApiHelper.Object);

             var model = new EditAccountModel() { UserId = 1, UserName = "yukon", IsConfirmed = true };
             string jsonResponse = ObjectParserHelper.ObjectToJson(model);
             _webApiHelper.Setup(a => a.getData(It.IsAny<string>(), It.IsAny<string>())).Returns(jsonResponse);
             
             // Act
             ViewResult result = controller.ShowAccountDetail(myTestValue) as ViewResult; ;

        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(_webApiHelper.Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
