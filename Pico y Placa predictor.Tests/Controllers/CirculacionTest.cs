using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pico_y_Placa_predictor.Controllers;

namespace Pico_y_Placa_predictor.Tests.Controllers
{
    [TestClass]
    public class CirculacionTest
    {
        [TestMethod]
        public void TestIndexView()
        {
            var controller = new CirculacionController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
