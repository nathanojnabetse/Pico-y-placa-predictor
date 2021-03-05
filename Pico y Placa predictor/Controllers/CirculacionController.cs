using System;
using System.Collections.Generic;
using System.Globalization;
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

        /// <summary>
        /// Method to check invalid state from View 
        /// </summary>
        /// <param name="oVehiculo">Object type vehicule from view</param>
        /// <returns>Message of error or success</returns>
        public string Comprobar(Vehiculo oVehiculo)
        {
            bool plateValid = false;
            bool dateValid = false;
            bool hourValid = false;
            
            string rpta = "<ul class='list-group'>"; //List with errors
         
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
                //Data asignation
                plateValid = PlateCheck(oVehiculo.placa);
                dateValid = DateCheck(oVehiculo.fecha);
                hourValid = HourCheck(oVehiculo.hora);
                DateTime dateValue; //Date in type Datetime if date exists

                if (plateValid && dateValid && hourValid)
                {
                    //date exists and is valid
                    if (DateTime.TryParse(oVehiculo.fecha, out dateValue))
                    {
                        oVehiculo.fecha = dateValue.DayOfWeek.ToString(); //Day of the week
                        rpta += MobilityCheck(oVehiculo,dateValue); //Message with info about today´s circulation
                    }
                    else
                    {
                        rpta += "<li class='list-group-item'> El día ingresado es inexistente, prueba otra fecha </li>";
                    }
                }
                else
                {
                    if (!plateValid) //Invalid plate format
                    {
                        rpta += "<li class='list-group-item'> Formato de placa: XXX-0000 </li>";
                    }
                    if (!dateValid) //Invalid Date format
                    {
                        rpta += "<li class='list-group-item'> Formato de fecha: dd/mm/yyy </li>";
                    }
                    if (!hourValid) //Invalid hour format
                    {
                        rpta += "<li class='list-group-item'> Formato de hora: hh: mm </li>";
                    }
                }
            }
            rpta += "</ul>";
            return rpta; //Message with errors in format or information about mobility
        }

        /// <summary>
        /// Methds to check about today's mobility once all of the Vehicule data is verified
        /// </summary>
        /// <param name="oVehiculo">Object type vehicule from view</param>
        /// <param name="dateValue">Date in format dd/MM/yyyy</param>
        /// <returns>Message of mobility permission</returns>
        public string MobilityCheck(Vehiculo oVehiculo,DateTime dateValue)
        {
            string rpta="";
            char lastDigit = oVehiculo.placa[oVehiculo.placa.Length - 1];//Laas digit of the plate

            // Days to compare in English and in Spanish to be visualized
            string day = dateValue.ToString("dddd",new CultureInfo("en-US"));
            string dia = dateValue.ToString("dddd", new CultureInfo("es-ES"));

            //Onject to compare between a time gap
            TimeSpan hour;
            TimeSpan.TryParse(oVehiculo.hora, out hour);


            if (((hour >= new TimeSpan(07, 00, 00) && hour <= new TimeSpan(9, 30, 0))
                || (hour >= new TimeSpan(16, 00, 00) && hour <= new TimeSpan(19, 30, 0)))
                && (day != "Sunday" || day != "Saturday"))
            {
               if ((lastDigit.Equals('1') || lastDigit.Equals('2'))
               && (day == "Monday"))
                {
                    rpta = "<li class='list-group-item'> SU VEHÍCULO NO PUEDE CIRCULAR ESTE DÍA " + dia.ToUpper() + " " + dateValue.ToString("dd/MM/yyyy") + " (07:00-9:30am / 16:00-19:30) </li>";
                }
               else if ((lastDigit.Equals('3') || lastDigit.Equals('4'))
               && (day == "Tuesday"))
                {
                    rpta = "<li class='list-group-item'> SU VEHÍCULO NO PUEDE CIRCULAR ESTE DÍA " + dia.ToUpper() + " " + dateValue.ToString("dd/MM/yyyy") + " (07:00-9:30am / 16:00-19:30) </li>";
                }
                else if ((lastDigit.Equals('5') || lastDigit.Equals('6'))
                && (day == "Thursday"))
                {
                    rpta = "<li class='list-group-item'> SU VEHÍCULO NO PUEDE CIRCULAR ESTE DÍA " + dia.ToUpper() + " " + dateValue.ToString("dd/MM/yyyy") + " (07:00-9:30am / 16:00-19:30) </li>";
                }
                else if ((lastDigit.Equals('7') || lastDigit.Equals('8'))
                && (day == "Wednesday"))
                {
                    rpta = "<li class='list-group-item'> SU VEHÍCULO NO PUEDE CIRCULAR ESTE DÍA " + dia.ToUpper() + " " + dateValue.ToString("dd/MM/yyyy") + " (07:00-9:30am / 16:00-19:30) </li>";
                }
                else if ((lastDigit.Equals('9') || lastDigit.Equals('0'))
                && (day == "Friday"))
                {
                    rpta = "<li class='list-group-item'> SU VEHÍCULO NO PUEDE CIRCULAR ESTE DÍA " + dia.ToUpper() + " " + dateValue.ToString("dd/MM/yyyy") + " (07:00-9:30am / 16:00-19:30) </li>";
                }
                else
                {
                    rpta = "<li class='list-group-item'> TIENE LIBRE MOVILIDAD PARA ESTA FECHA " + dia.ToUpper() + " " + dateValue.ToString("dd/MM/yyyy") + " Y HORA " + hour.ToString(@"hh\:mm") + "</li>";
                }
            }               
            else
            {
                rpta = "<li class='list-group-item'> TIENE LIBRE MOVILIDAD PARA ESTA FECHA " + dia.ToUpper() + " " + dateValue.ToString("dd/MM/yyyy") + " Y HORA " + hour.ToString(@"hh\:mm") + "</li>";
            }

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