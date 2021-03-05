using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Pico_y_Placa_predictor.Models;

namespace Pico_y_Placa_predictor.Controllers
{
    public class CirculacionController : Controller
    {
        // GET: Circulacion
        public ActionResult Index()
        {
            return View("Index");
        }

        public string Comprobar(Vehiculo oVehiculo)
        {
            string rpta = "";
            rpta += "<ul class='list-group'>"; //List with errors
            if (!ModelState.IsValid)
            {
                var query = (from state in ModelState.Values//Values
                             from error in state.Errors//Messages
                             select error.ErrorMessage).ToList();
                
                foreach (var item in query)
                {
                    rpta += "<li class='list-group-item'>" + item + "</li>";
                }
                
            }
            else
            {
                if (!PlateCheck(oVehiculo.placa))
                {
                    rpta += "<li class='list-group-item'> Formato de placa: XXX-0000 </li>";
                }
                if (!DateCheck(oVehiculo.fecha))
                {
                    rpta += "<li class='list-group-item'> Formato de fecha: dd/mm/yyy </li>";
                }
                if (!HourCheck(oVehiculo.hora))
                {
                    rpta += "<li class='list-group-item'> Formato de hora: hh: mm </li>";
                }

            }
            rpta += "</ul>";
            return rpta;
        }

        /// <summary>
        /// Checks for valid format string for vehicle plate        
        /// </summary>
        /// <param name="plate">String of plate to compare</param>
        /// <returns>if the plate is valid or not</returns>
        public bool PlateCheck(string plate)
        {
            // Valid plate format = XXX-0000
            // Admits 3 letters (Except D and F at de beggining) , and at least 3 numbers
            var validPlate = Regex.Match(plate, "^[^DFa-z0-9!\"#$%&'()*+,-./:;<=>?@\\][^_`{|}~][A-Z]{2}-[0-9]{3,4}$");

            return validPlate.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool DateCheck(string date)
        {
            // Valid date format = dd/MM/yyyy            
            var validDate = Regex.Match(date, @"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");

            return validDate.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hour"></param>
        /// <returns></returns>
        public bool HourCheck(string hour)
        {
            // 00:00 
            // 24 Hour format
            var validHour = Regex.Match(hour, "^([01][0-9]|2[0-3]):[0-5][0-9]$");

            return validHour.Success;
        }
    }
}