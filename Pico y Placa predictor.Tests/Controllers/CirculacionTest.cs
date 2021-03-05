using System;
using System.Globalization;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pico_y_Placa_predictor.Controllers;
using Pico_y_Placa_predictor.Models;

namespace Pico_y_Placa_predictor.Tests.Controllers
{
    [TestClass]
    public class CirculacionTest
    {
        /// <summary>
        /// Test Methos to verify that the correct vie is returned when CirculacionController/Index is called
        /// </summary>
        [TestMethod]
        public void TestIndexView()
        {
            var controller = new CirculacionController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        /// <summary>
        /// Test to verify if a vehicle plate is in the correct format
        /// </summary>
        [TestMethod]
        public void TestPlateCheck_Valid()
        {
            var controller = new CirculacionController();
            var validPlate = controller.PlateCheck("JFK-666");
            Assert.IsTrue(validPlate);
            validPlate = controller.PlateCheck("KJF-8745");
            Assert.IsTrue(validPlate);
            validPlate = controller.PlateCheck("PCE-9574");
            Assert.IsTrue(validPlate);
            validPlate = controller.PlateCheck("LOP-755");
            Assert.IsTrue(validPlate);
            validPlate = controller.PlateCheck("AWE-4510");
            Assert.IsTrue(validPlate);
            validPlate = controller.PlateCheck("MDF-000");
            Assert.IsTrue(validPlate);
            validPlate = controller.PlateCheck("EPL-420");
            Assert.IsTrue(validPlate);
        }

        /// <summary>
        /// Test to verify if a vehicle plate is in the incorrect format
        /// </summary>
        [TestMethod]
        public void TestPlateCheck_NoValid()
        {
            var controller = new CirculacionController();
            var validPlate = controller.PlateCheck("jfk-666");
            Assert.IsFalse(validPlate);
            validPlate = controller.PlateCheck("KJF-87");
            Assert.IsFalse(validPlate);
            validPlate = controller.PlateCheck("*CE-9574");
            Assert.IsFalse(validPlate);
            validPlate = controller.PlateCheck("EOP755");
            Assert.IsFalse(validPlate);
            validPlate = controller.PlateCheck("FWE-4510");
            Assert.IsFalse(validPlate);
            validPlate = controller.PlateCheck("0s-440");
            Assert.IsFalse(validPlate);
            validPlate = controller.PlateCheck("451-AAA");
            Assert.IsFalse(validPlate);
            validPlate = controller.PlateCheck("fpl123");
            Assert.IsFalse(validPlate);
            validPlate = controller.PlateCheck("AAA-42540");
            Assert.IsFalse(validPlate);
        }

        /// <summary>
        /// Test to verify if a date is in the incorrect format
        /// </summary>
        [TestMethod]
        public void TestDateCheck_Valid()
        {
            var controller = new CirculacionController();
            var validDate = controller.DateCheck("14/01/1997");
            Assert.IsTrue(validDate);
            
        }

        /// <summary>
        /// Test to verify if a date is in the incorrect format
        /// </summary>
        [TestMethod]
        public void TestDateCheck_NoValid()
        {
            var controller = new CirculacionController();
            var validDate = controller.DateCheck("14-01-1997");
            Assert.IsFalse(validDate);
            validDate = controller.DateCheck("a");
            Assert.IsFalse(validDate);
            validDate = controller.DateCheck("14/ 01/1997");
            Assert.IsFalse(validDate);
            validDate = controller.DateCheck("2001/01/05");
            Assert.IsFalse(validDate);
            validDate = controller.DateCheck("4/01/1997");
            Assert.IsFalse(validDate);
            validDate = controller.DateCheck("32/05/1997");
            Assert.IsFalse(validDate);
            validDate = controller.DateCheck("05/13/1997");
            Assert.IsFalse(validDate);
        }

        /// <summary>
        /// Test to verify if an hour is in the incorrect format
        /// </summary>
        [TestMethod]
        public void TestHourCheck_Valid()
        {
            var controller = new CirculacionController();
            var validHour = controller.HourCheck("15:59");
            Assert.IsTrue(validHour);

        }

        /// <summary>
        /// Test to verify if an hour is in the incorrect format
        /// </summary>
        [TestMethod]
        public void TestHourCheck_NoValid()
        {
            var controller = new CirculacionController();
            var validHour = controller.HourCheck("9:45");
            Assert.IsFalse(validHour);
            validHour = controller.HourCheck("15:60");
            Assert.IsFalse(validHour);
            validHour = controller.HourCheck("24:00");            
            Assert.IsFalse(validHour);
            validHour = controller.HourCheck("a");
            Assert.IsFalse(validHour);
            validHour = controller.HourCheck("15 05");
            Assert.IsFalse(validHour);
        }

        /// <summary>
        /// Test to verify correct string return
        /// </summary>
        [TestMethod]
        public void TestMobilityCheck_Valid()
        {
            var controller = new CirculacionController();
            Vehiculo oVehiculo = new Vehiculo();
            oVehiculo.placa = "PCE-9574";
            oVehiculo.fecha = "05/02/2021";
            oVehiculo.hora = "07:30";
            oVehiculo.mensaje = "";

            DateTime dateValue;
            DateTime.TryParse(oVehiculo.fecha, out dateValue);
            string dia = dateValue.ToString("dddd", new CultureInfo("es-ES"));

            oVehiculo.mensaje = controller.MobilityCheck(oVehiculo, dateValue);
            
            Assert.AreEqual("<li class='list-group-item'> TIENE LIBRE MOVILIDAD PARA ESTA FECHA " + dia.ToUpper() + " " + oVehiculo.fecha + " Y HORA " + oVehiculo.hora + "</li>", oVehiculo.mensaje);

            oVehiculo.placa = "PCE-9574";
            oVehiculo.fecha = "14/01/1997";
            oVehiculo.hora = "16:30";
            oVehiculo.mensaje = "";
           
            DateTime.TryParse(oVehiculo.fecha, out dateValue);
            dia = dateValue.ToString("dddd", new CultureInfo("es-ES"));

            oVehiculo.mensaje = controller.MobilityCheck(oVehiculo, dateValue);
            
            Assert.AreEqual("<li class='list-group-item'> SU VEHÍCULO NO PUEDE CIRCULAR ESTE DÍA " + dia.ToUpper() + " " + oVehiculo.fecha + " (07:00-9:30am / 16:00-19:30) </li>", oVehiculo.mensaje);
        }

       

    }
}
